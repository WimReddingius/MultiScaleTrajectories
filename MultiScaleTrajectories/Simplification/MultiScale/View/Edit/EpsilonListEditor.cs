using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.CompactError;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class EpsilonListEditor : UserControl
    {
        private List<double> maxErrors;
        private MSInput input;

        public EpsilonListEditor()
        {
            InitializeComponent();

            Closeness.ValueType = typeof(double);
        }

        private void levelTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            var level = (int)e.Row.Cells["Level"].Value;
            input.RemoveLevel(level);
            RevalidateLevelColumn();
        }

        private void addLevelButton_Click(object sender, EventArgs e)
        {
            var insertAt = levelTable.Rows.Count;

            if (levelTable.SelectedCells.Count > 0)
                insertAt = levelTable.SelectedCells[0].RowIndex + 1;

            var epsilon = double.PositiveInfinity;
            if (levelTable.RowCount > insertAt)
                epsilon = (double)levelTable.Rows[insertAt].Cells["Closeness"].Value;

            InsertLevel(insertAt + 1, epsilon);
        }

        private void removeLevelButton_Click(object sender, EventArgs e)
        {
            if (levelTable.RowCount > 1)
            {
                var removeAt = levelTable.Rows.Count;
                if (levelTable.SelectedCells.Count > 0)
                    removeAt = levelTable.SelectedCells[0].RowIndex + 1;

                RemoveLevel(removeAt);
            }
        }

        private void levelTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int level = e.RowIndex + 1;
            input.SetEpsilon(level, (double)levelTable.Rows[e.RowIndex].Cells["Closeness"].Value);
        }

        private void RemoveLevel(int level)
        {
            if (level > 0)
            {
                var rowIndex = level - 1;
                levelTable.Rows.RemoveAt(rowIndex);
                input.RemoveLevel(level);
                RevalidateLevelColumn();
            }
        }

        private void RevalidateLevelColumn()
        {
            for (var row = 0; row < levelTable.Rows.Count; row++)
            {
                levelTable.Rows[row].Cells["Level"].Value = row + 1;
            }
        }

        private void InsertLevel(int level, double epsilon)
        {
            levelTable.Rows.Insert(level - 1, level, epsilon);
            input.InsertLevel(level, epsilon);

            for (int i = level; i < levelTable.RowCount; i++)
            {
                levelTable.Rows[i].Cells["Level"].Value = (int)levelTable.Rows[i].Cells["Level"].Value + 1;
            }
        }

        private async void computeErrorDistribution_Click(object sender, EventArgs e)
        {
            var trajectory = input.Trajectory;

            var builder = new MSSCompleteCompactError
            {
                ShortcutSetFactory = new ShortcutIntervalSetFactory()
            };

            var inp = new MSSInput(trajectory, new List<double> {0.0});
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

                var errors = (MSCompactErrorShortcutSet) builder.FindShortcuts(new MSSConvexHull.ShortcutChecker(inp, outp), true);
                maxErrors = errors.MaxErrors.Values.ToList();

                this.InvokeIfRequired(() => shortcutFindingProgressLabel.Text = "Sorting...");

                maxErrors.Sort();
            });

            shortcutFindingProgressLabel.Text = "Finished";

            string illegalChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex illegalCharsRegex = new Regex(string.Format("[{0}]", Regex.Escape(illegalChars)));
            string santitizedInputName = illegalCharsRegex.Replace(input.Name, "");

            string fileName = "Error list - " + santitizedInputName + ".json";
            JsonSerializerSettings serializationSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            using (StreamWriter file = File.CreateText(fileName))
            using (JsonTextWriter textWriter = new JsonTextWriter(file))
            {
                var serializer = JsonSerializer.Create(serializationSettings);
                serializer.Serialize(textWriter, maxErrors);
            }

            revalidateDistributionButton.Enabled = true;
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

            for (var level = input.NumLevels; level >= 1; level--)
            {
                RemoveLevel(level);
            }

            var numEpsilons = maxErrors.Count;
            var start = (double)(lowerPercentageChooser.Value / 100 * (numEpsilons - 1));
            var end = (double)(upperPercentageChooser.Value / 100 * (numEpsilons - 1));
            var numLevels = (int)numLevelsChooser.Value;

            var range = end - start;
            var step = numLevels == 1 ? 0 : range / (numLevels - 1);

            var index = start;
            for (var level = 1; level <= numLevels; level++)
            {
                var intIndex = (int)index;
                var epsilon = maxErrors[intIndex];

                if (index > intIndex && (intIndex + 1 <= maxErrors.Count - 1))
                {
                    var nextEpsilon = maxErrors[intIndex + 1];
                    var remainder = index - intIndex;
                    epsilon += (epsilon - nextEpsilon) * remainder;
                }

                InsertLevel(level, epsilon);
                index += step;
            }
        }

        public void LoadInput(MSInput inp)
        {
            input = inp;
            levelTable.Rows.Clear();

            for (int level = 1; level <= input.NumLevels; level++)
            {
                levelTable.Rows.Add(level, input.GetEpsilon(level));
            }

            maxErrors = null;
            revalidateDistributionButton.Enabled = false;
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

                revalidateDistributionButton.Enabled = true;
                BuildErrorDistribution();
            }
            catch (Exception err)
            {
                FormsUtil.ShowErrorMessage(err.ToString());
            }
        }
    }
}
