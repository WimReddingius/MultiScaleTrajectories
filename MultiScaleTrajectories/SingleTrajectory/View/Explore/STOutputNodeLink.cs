using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore
{
    class STOutputNodeLink : GLTrajectoryVisualization2D
    {
        private AlgorithmRun<STInput, STOutput> Run;
        private STOutput Output;

        private int currentLevel;


        public STOutputNodeLink()
        {
            KeyDown += HandleArrowKeys;
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

        public void BeforeOutputAvailable(AlgorithmRun<STInput, STOutput> run)
        {
            Visible = false;
        }

        public void AfterOutputAvailable(AlgorithmRun<STInput, STOutput> run)
        {
            Run = run;
            Output = run.Output;

            Visible = true;
            currentLevel = 1;
            LookAtTrajectory(Output.GetTrajectoryAtLevel(1));
        }

    }
}
