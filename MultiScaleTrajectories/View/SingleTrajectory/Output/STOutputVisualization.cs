using System;
using System.Drawing;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Controller.Util;
using MultiScaleTrajectories.View.Visualization;
using MultiScaleTrajectories.View.Visualization.GL;

namespace MultiScaleTrajectories.View.SingleTrajectory.Output
{
    class STOutputVisualization : GLVisualization2D, IDataLoader<STOutput>
    {
        private STOutput Output;
        private int CurrentLevel;

        protected override void RenderWorld()
        {
            if (Output.IsComplete)
            {
                Util2D.DrawEdges(GetRenderedTrajectory(), 2.5f, Color.Red);
                Util2D.DrawPoints(GetRenderedTrajectory(), 3.5f, Color.Red);
            }

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

        public void LoadData(STOutput output)
        {
            Output = output;

            CurrentLevel = -1;
            KeyDown -= HandleArrowKeys;

            Output.Completed += () =>
            {
                if (InvokeRequired)
                {
                    Action del = () =>
                    {
                        CurrentLevel = Output.NumLevels;
                        KeyDown += HandleArrowKeys;
                        Refresh();
                    };
                    Invoke(del);
                }
            };
        }

    }
}
