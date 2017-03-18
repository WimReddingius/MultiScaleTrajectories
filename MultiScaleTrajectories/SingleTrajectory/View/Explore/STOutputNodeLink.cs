using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore
{
    class STOutputNodeLink : GLTrajectoryVisualization2D, IRunExplorer<STInput, STOutput>
    {
        public string DisplayName => "Node-Link Visualization";
        public int MinConsolidation => 1;
        public int MaxConsolidation => 1;
        public int Priority => 1;

        private AlgorithmRun<STInput, STOutput> Run;
        private STOutput Output;

        private int currentLevel;
        private bool active => Visible;


        public STOutputNodeLink()
        {
            Visible = false;
        }

        protected override void RenderWorld()
        {
            Trajectory2D trajectory = Output.GetTrajectoryAtLevel(currentLevel);
            GLUtilTrajectory2D.DrawEdges(trajectory, 2.5f, Color.Red);
        }

        protected override void RenderHud()
        {
            int padding = 5;
            Color color = Color.Black;
            GLUtil2D.RenderText(padding, padding, Run.Name, color);
            GLUtil2D.RenderText(padding, 20 + padding, "Level " + currentLevel, color);
        }

        private void HandleArrowKeys(object sender, KeyEventArgs e)
        {
            bool levelDown = e.KeyCode == Keys.Up;
            bool levelUp = e.KeyCode == Keys.Down;

            if (levelDown && currentLevel > 1)
            {  // here up
                currentLevel--;
            }

            if (levelUp && currentLevel < Output.NumLevels)
            {  // here down
                currentLevel++;
            }
            Refresh();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        public void RunSelectionChanged(AlgorithmRun<STInput, STOutput>[] runs)
        {
            Run = runs[0];
            Output = runs[0].Output;
        }

        public void RunStateChanged(AlgorithmRun<STInput, STOutput> run, RunState state)
        {
            if (state == RunState.Idle)
            {
                KeyDown -= HandleArrowKeys;
                Visible = false;
            }

            if (state >= RunState.OutputAvailable)
            {
                if (active) return;

                Visible = true;

                currentLevel = 1;
                LookAtTrajectory(run.Output.GetTrajectoryAtLevel(1));

                KeyDown -= HandleArrowKeys;
                KeyDown += HandleArrowKeys;
            }
        }

    }
}
