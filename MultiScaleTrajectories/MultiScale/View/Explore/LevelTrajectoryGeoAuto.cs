using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm.Run;
using GMap.NET;
using GMap.NET.WindowsForms;
using MultiScaleTrajectories.MultiScale.Algorithm;

namespace MultiScaleTrajectories.MultiScale.View.Explore
{
    partial class LevelTrajectoryGeoAuto : UserControl
    {
        private AlgorithmRun<MSInput, MSOutput> run;
        private int currentLevel;

        private MSOutput output => run?.Output;
        private GMapControl MapControl => trajectoryGMap.MapControl;
        private double epsilonSpan => (double) detailNumericUpDown.Value;


        public LevelTrajectoryGeoAuto()
        {
            InitializeComponent();

            MapControl.Paint += DrawInfo;
            MapControl.OnMapZoomChanged += FitLevelToDesiredDetail;
        }

        private void FitLevelToDesiredDetail()
        {
            var minSpan = Math.Min(MapControl.ViewArea.WidthLng, MapControl.ViewArea.HeightLat);
            currentLevel = run.Input.NumLevels;
                
            while (true)
            {
                var levelEpsilonSpan = minSpan / run.Input.GetEpsilon(currentLevel);
                if (levelEpsilonSpan > epsilonSpan || currentLevel == 1)
                    break;

                currentLevel--;
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

            var trajectory = output.GetTrajectoryAtLevel(1);
            trajectoryGMap.LookAtTrajectory(trajectory);

            FitLevelToDesiredDetail();
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

        private void detailNumericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            FitLevelToDesiredDetail();
        }
    }
}
