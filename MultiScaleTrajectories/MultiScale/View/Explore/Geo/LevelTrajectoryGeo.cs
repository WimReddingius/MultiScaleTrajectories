using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using MultiScaleTrajectories.MultiScale.Algorithm;

namespace MultiScaleTrajectories.MultiScale.View.Explore.Geo
{
    partial class LevelTrajectoryGeo : UserControl
    {
        private MSOutput output;
        private int currentLevel;

        public LevelTrajectoryGeo()
        {
            InitializeComponent();

            trajectoryGMap.MapControl.PreviewKeyDown += HandlePreviewKeyDown;
            trajectoryGMap.MapControl.KeyDown += HandleArrowKeys;
        }

        private void HandlePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void HandleArrowKeys(object sender, KeyEventArgs e)
        {
            var levelDown = e.KeyCode == Keys.Up;
            var levelUp = e.KeyCode == Keys.Down;

            if (levelDown && currentLevel > 1)
            {  // here up
                currentLevel--;
            }

            if (levelUp && currentLevel < output.NumLevels)
            {  // here down
                currentLevel++;
            }

            var trajectory = output.GetTrajectoryAtLevel(currentLevel);
            trajectoryGMap.DrawSingleTrajectory(trajectory);
        }

        public void BeforeOutputAvailable(AlgorithmRun<MSInput, MSOutput> run)
        {
           Visible = false;
        }

        public void AfterOutputAvailable(AlgorithmRun<MSInput, MSOutput> run)
        {
            Visible = true;

            output = run.Output;
            currentLevel = 1;

            var trajectory = output.GetTrajectoryAtLevel(currentLevel);

            trajectoryGMap.DrawSingleTrajectory(trajectory);
            trajectoryGMap.LookAtTrajectory(trajectory);
        }

    }
}
