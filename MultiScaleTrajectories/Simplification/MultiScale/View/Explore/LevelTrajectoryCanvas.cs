﻿using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.View.GLVisualization.GLUtil;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Trajectory.View;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
{
    class LevelTrajectoryCanvas : TrajectoryCanvas
    {
        private AlgorithmRun<MSInput, MSOutput> run;
        private MSOutput output;

        private int currentLevel;


        public LevelTrajectoryCanvas()
        {
            KeyDown += HandleArrowKeys;
            Visible = false;
        }

        protected override void RenderWorld()
        {
            Trajectory2D trajectory = output.GetTrajectoryAtLevel(currentLevel);
            DrawTrajectoryEdges(trajectory, 2.5f, Color.Red);
        }

        protected override void RenderHud()
        {
            int padding = 5;
            var color = Color.Black;
            GLUtil2D.RenderText(padding, padding, run.Name, color);
            GLUtil2D.RenderText(padding, 20 + padding, "Level " + currentLevel, color);
        }

        private void HandleArrowKeys(object sender, KeyEventArgs e)
        {
            var levelDown = e.KeyCode == Keys.Down;
            var levelUp = e.KeyCode == Keys.Up;

            if (levelDown && currentLevel > 1)
            {  // here up
                currentLevel--;
            }

            if (levelUp && currentLevel < output.NumLevels)
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

        public void BeforeOutputAvailable(AlgorithmRun<MSInput, MSOutput> run)
        {
            Visible = false;
        }

        public void AfterOutputAvailable(AlgorithmRun<MSInput, MSOutput> run)
        {
            this.run = run;
            output = run.Output;

            Visible = true;
            currentLevel = 1;
            LookAtTrajectory(output.GetTrajectoryAtLevel(currentLevel));
        }

    }
}
