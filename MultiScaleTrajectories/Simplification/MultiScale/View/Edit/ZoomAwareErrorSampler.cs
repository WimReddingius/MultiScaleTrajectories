using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class ZoomAwareErrorSampler : UserControl, IErrorSampler
    {
        public string TypeName => "Zoom Aware";
        public UserControl Control => this;
        public event Action<List<double>> NewSamples;

        private MSInput input;

        public ZoomAwareErrorSampler()
        {
            InitializeComponent();

            numLevelsNumericUpDown.Maximum = int.MaxValue;
        }

        public void LoadInput(MSInput inp)
        {
            input = inp;
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            var bb = input.Trajectory.BuildBoundingBox();
            var bbDiagonal = Math.Sqrt(Math.Pow(bb.Width, 2) + Math.Pow(bb.Height, 2));
            var detailFactor = ((double)detailPercentileNumericUpDown.Value) * bbDiagonal;
            var zoomFactor = (double)zoomFactorNumericUpDown.Value;

            var errors = new List<double>();
            for (var level = 1; level <= (int)numLevelsNumericUpDown.Value; level++)
            {
                errors.Add(detailFactor / Math.Pow(zoomFactor, input.NumLevels - level));
            }

            NewSamples.Invoke(errors);
        }
    }
}
