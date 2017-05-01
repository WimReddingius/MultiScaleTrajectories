using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using GMap.NET.WindowsForms;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
{
    partial class LevelTrajectoryGeo : UserControl
    {
        private AlgorithmRun<MSInput, MSOutput> run;
        private MSOutput output => run?.Output;
        private int currentLevel;

        private GMapControl MapControl => trajectoryGMap.MapControl;

        public LevelTrajectoryGeo()
        {
            InitializeComponent();

            MapControl.PreviewKeyDown += HandlePreviewKeyDown;
            MapControl.KeyDown += HandleArrowKeys;
            MapControl.Paint += DrawInfo;
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

            this.run = run;
            currentLevel = 1;

            var trajectory = output.GetTrajectoryAtLevel(currentLevel);

            trajectoryGMap.DrawSingleTrajectory(trajectory);
            trajectoryGMap.LookAtTrajectory(trajectory);
        }

        private void DrawInfo(object sender, PaintEventArgs e)
        {
            if (run == null)
                return;

            var g = e.Graphics;
            var brush = new SolidBrush(Color.Blue);
            var font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.DrawString(run.Name, font, brush, 5, 5);
            g.DrawString("Level " + currentLevel, font, brush, 5, 25);
        }

    }
}
