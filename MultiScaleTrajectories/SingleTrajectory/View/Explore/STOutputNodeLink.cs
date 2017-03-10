using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Experiment;
using AlgorithmVisualization.Controller.Explore;
using AlgorithmVisualization.View.Visualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore
{
    class STOutputNodeLink : GLTrajectoryVisualization2D, IRunLoader<STInput, STOutput>
    {
        private AlgorithmRun<STInput, STOutput> run;
        private STOutput Output;

        private STInput previousInput;
        private int CurrentLevel;

        public STOutputNodeLink()
        {
            DeregisterEvents();
        }

        protected override void RenderWorld()
        {
            Trajectory2D trajectory = Output.GetTrajectoryAtLevel(CurrentLevel);
            GLUtilTrajectory2D.DrawEdges(trajectory, 2.5f, Color.Red);
        }

        protected override void RenderHud()
        {
            int padding = 5;
            Color color = Color.Black;
            GLUtil2D.RenderText(padding, padding, run.Name, color);
            GLUtil2D.RenderText(padding, 20 + padding, "Level " + CurrentLevel, color);
        }

        private void HandleArrowKeys(object sender, KeyEventArgs e)
        {
            bool levelDown = e.KeyCode == Keys.Up;
            bool levelUp = e.KeyCode == Keys.Down;

            if (levelDown && CurrentLevel > 1)
            {  // here up
                CurrentLevel--;
            }

            if (levelUp && CurrentLevel < Output.NumLevels)
            {  // here down
                CurrentLevel++;
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

        public void LoadRuns(AlgorithmRun<STInput, STOutput>[] runs)
        {
            DeregisterEvents();

            run = runs[0];
            Output = run.Output;

            run.OnFinish(HandleRunFinished);
        }

        private void DeregisterEvents()
        {
            KeyDown -= HandleArrowKeys;
            Paint -= Render;
        }

        private void HandleRunFinished()
        {
            //preserve currently visualized level if the input hasn't changed
            if (previousInput != run.Input)
                CurrentLevel = 1;

            previousInput = run.Input;

            LookAtTrajectory(Output.GetTrajectoryAtLevel(1));

            //purely a safeguard to prevent double event subscription
            DeregisterEvents();

            KeyDown += HandleArrowKeys;
            Paint += Render;

            Refresh();
        }

    }
}
