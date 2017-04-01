using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo
{
    partial class LevelTrajectoryGMap : UserControl
    {
        private STOutput output;
        private int currentLevel;

        public LevelTrajectoryGMap()
        {
            InitializeComponent();

            gMap.MapControl.PreviewKeyDown += HandlePreviewKeyDown;
            gMap.MapControl.KeyDown += HandleArrowKeys;
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
            gMap.ShowSingleTrajectory(trajectory);
        }

        public void BeforeOutputAvailable(AlgorithmRun<STInput, STOutput> run)
        {
           Visible = false;
        }

        public void AfterOutputAvailable(AlgorithmRun<STInput, STOutput> run)
        {
            Visible = true;

            output = run.Output;
            currentLevel = 1;

            var trajectory = output.GetTrajectoryAtLevel(currentLevel);
            gMap.LookAtTrajectory(trajectory);
            gMap.ShowSingleTrajectory(trajectory);
        }

    }
}
