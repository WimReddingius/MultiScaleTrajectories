using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.CompactError;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms;
using AlgorithmVisualization.View.Util;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    internal partial class ShortcutErrorSampler : UserControl, IErrorSampler
    {
        public UserControl Control => this;
        public string TypeName => "Shortcut Error Percentile";
        public event Action<List<double>> NewSamples;

        private List<double> maxErrors;
        private MSInput input;

        public ShortcutErrorSampler()
        {
            maxErrors = null;
            InitializeComponent();

            numLevelsNumericUpDown.Maximum = int.MaxValue;
        }

        private async void computeErrorDistribution_Click(object sender, EventArgs e)
        {
            var trajectory = input.Trajectory;

            var builder = new MSSCompleteCompactError
            {
                ShortcutSetFactory = new ShortcutIntervalSetFactory()
            };

            var inp = new MSSInput(trajectory, new List<double> { 0.0 });
            var outp = new MSSOutput(inp);

            outp.Logged += str =>
            {
                this.InvokeIfRequired(() =>
                {
                    str = shortcutFindingProgressLabel.Text = str;
                });
            };

            await Task.Run(() =>
            {
                this.InvokeIfRequired(() => shortcutFindingProgressLabel.Text = "Started...");

                var errors = (MSCompactErrorShortcutSet)builder.FindShortcuts(new MSSConvexHull.ShortcutChecker(inp, outp), true);
                maxErrors = errors.MaxErrors.Values.ToList();

                this.InvokeIfRequired(() => shortcutFindingProgressLabel.Text = "Sorting...");

                maxErrors.Sort();
            });

            shortcutFindingProgressLabel.Text = "Finished";

            string illegalChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex illegalCharsRegex = new Regex(string.Format("[{0}]", Regex.Escape(illegalChars)));
            string santitizedInputName = illegalCharsRegex.Replace(input.Name, "");

            string fileName = "Error list - " + santitizedInputName + ".json";
            using (StreamWriter file = File.CreateText(fileName))
            using (JsonTextWriter textWriter = new JsonTextWriter(file))
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings());
                serializer.Serialize(textWriter, maxErrors);
            }

            BuildErrorDistribution();
        }

        private void revalidateDistributionButton_Click(object sender, EventArgs e)
        {
            BuildErrorDistribution();
        }

        private void BuildErrorDistribution()
        {
            if (maxErrors == null)
                return;

            var numEpsilons = maxErrors.Count;
            var start = (double)(lowerPercentageChooser.Value / 100 * (numEpsilons - 1));
            var end = (double)(upperPercentageChooser.Value / 100 * (numEpsilons - 1));
            var numLevels = (int)numLevelsNumericUpDown.Value;

            var range = end - start;
            var step = numLevels == 1 ? 0 : range / (numLevels - 1);
            var errors = new List<double>();

            var index = start;
            for (var level = 1; level <= numLevels; level++)
            {
                var intIndex = (int)index;
                var epsilon = maxErrors[intIndex];

                if (index > intIndex && (intIndex + 1 <= maxErrors.Count - 1))
                {
                    var nextEpsilon = maxErrors[intIndex + 1];
                    var remainder = index - intIndex;
                    epsilon += (nextEpsilon - epsilon) * remainder;
                }

                errors.Add(epsilon);
                index += step;
            }

            NewSamples.Invoke(errors);
        }

        private void openErrorsButton_Click(object sender, EventArgs e)
        {
            var result = openErrorsFileDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            var fileName = openErrorsFileDialog.FileName;
            try
            {
                using (StreamReader file = File.OpenText(fileName))
                using (JsonTextReader textReader = new JsonTextReader(file))
                {
                    var serializer = new JsonSerializer();
                    maxErrors = serializer.Deserialize<List<double>>(textReader);
                }

                BuildErrorDistribution();
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }

        public void LoadInput(MSInput inp)
        {
            input = inp;
            maxErrors = null;
        }
    }
}
