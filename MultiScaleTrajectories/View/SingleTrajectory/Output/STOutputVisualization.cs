using System;
using System.Drawing;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.View.Visualization;
using MultiScaleTrajectories.View.Visualization.GL;

namespace MultiScaleTrajectories.View.SingleTrajectory.Output
{
    class STOutputVisualization : GLVisualization2D, IDataLoader<AlgorithmRun<STInput, STOutput>[]>
    {
        private STOutput Output;
        private int CurrentLevel;

        public STOutputVisualization()
        {
            HandleOutputIncomplete();
        }

        protected override void RenderWorld()
        {
            Util2D.DrawEdges(GetRenderedTrajectory(), 2.5f, Color.Red);
            Util2D.DrawPoints(GetRenderedTrajectory(), 3.5f, Color.Red);
            
            int padding = 5;
            string text = "Level " + CurrentLevel;
            Color color = Color.Black;
            Util2D.RenderText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, text, color);
        }

        protected Trajectory2D GetRenderedTrajectory()
        {
            return Output.GetTrajectoryAtLevel(CurrentLevel);
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

        public void LoadData(AlgorithmRun<STInput, STOutput>[] runs)
        {
            Output = runs[0].Output;

            if (Output.IsComplete)
                HandleOutputComplete();
            else
                Output.Completed += HandleOutputComplete;
        }

        private void HandleOutputIncomplete()
        {
            CurrentLevel = -1;
            KeyDown -= HandleArrowKeys;
            Paint -= Render;
        }

        private void HandleOutputComplete()
        {
            CurrentLevel = Output.NumLevels;
            KeyDown += HandleArrowKeys;
            Paint += Render;

            if (InvokeRequired)
            {
                Action del = Refresh;
                Invoke(del);
            }
        }

    }
}
