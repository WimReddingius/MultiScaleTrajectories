using System;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Exploration.Visualization.GLUtil;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.SingleTrajectory.View.Output
{
    class STOutputNodeLink : GLTrajectoryVisualization, IRunLoader<STInput, STOutput>
    {
        private STOutput Output;
        private int CurrentLevel;

        public STOutputNodeLink()
        {
            HandleOutputIncomplete();
        }

        protected override void RenderWorld()
        {
            GLUtilTrajectory2D.DrawEdges(GetRenderedTrajectory(), 2.5f, Color.Red);
            //GLUtilTrajectory2D.DrawPoints(GetRenderedTrajectory(), 3.5f, (p) => Color.Red);
        }

        protected override void RenderHud()
        {
            int padding = 5;
            string text = "Level " + CurrentLevel;
            Color color = Color.Black;
            GLUtil2D.RenderText((-ClientRectangle.Width / 2) + padding, (-ClientRectangle.Height / 2) + padding, text, color);
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

        public void LoadRuns(AlgorithmRun<STInput, STOutput>[] runs)
        {
            HandleOutputIncomplete();
            Output = runs[0].Output;

            if (runs[0].IsFinished)
                HandleOutputComplete();
            else
                runs[0].Finished += HandleOutputComplete;
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
