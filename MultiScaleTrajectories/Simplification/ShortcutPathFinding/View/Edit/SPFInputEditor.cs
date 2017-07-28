using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Simplification.ShortcutFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.View.Edit
{
    partial class SPFInputEditor : UserControl, IInputEditor<SPFInput>
    {
        private readonly InputEditor<SingleTrajectoryInput> trajectoryEditor;
        private SPFInput input;

        public SPFInputEditor(IInputEditor<SingleTrajectoryInput> editor)
        {
            InitializeComponent();

            errorUpDown.Maximum = decimal.MaxValue;
            sourceIndexUpDown.Maximum = decimal.MaxValue;
            targetIndexUpdown.Maximum = decimal.MaxValue;

            trajectoryEditor = new SimpleInputEditor<SingleTrajectoryInput>(editor);

            splitContainer1.Panel2.Fill(trajectoryEditor);
            Name = trajectoryEditor.Name;
        }

        public void LoadInput(SPFInput input)
        {
            this.input = input;
            trajectoryEditor.LoadInput(input);

            if (input.Trajectory.Count == 0)
            {
                sourceIndexUpDown.Value = 0;
                targetIndexUpdown.Value = 0;
                return;
            }

            if (input.Source != null && input.Targets.Count > 0)
            {
                if (input.Source == input.Trajectory.First() && input.Targets.First() == input.Trajectory.Last())
                {
                    startToEndCheckbox.CheckState = CheckState.Checked;
                    sourceIndexUpDown.Value = 0;
                    targetIndexUpdown.Value = input.Trajectory.Last().Index;
                }
                else
                {
                    sourceIndexUpDown.Value = input.Source.Index;
                    targetIndexUpdown.Value = input.Targets.First().Index;
                }
            }
            else
            {
                startToEndCheckbox.CheckState = CheckState.Checked;
                sourceIndexUpDown.Value = 0;
                targetIndexUpdown.Value = input.Trajectory.Last().Index;
            }

            if (input.ShortcutSet != null)
            {
                var regions = (ShortcutIntervalSet) input.ShortcutSet;
                shortcutFindingProgressLabel.Text = "Shortcuts: " + regions.Count + " Intervals: " + regions.IntervalCount;
            }
        }

        private async void computeShortcutsButton_Click(object sender, System.EventArgs e)
        {
            var epsilon = (double) errorUpDown.Value;

            var inp = new MSSInput
            {
                Trajectory = input.Trajectory,
                Epsilons = new List<double> {epsilon}
            };

            var outp = new MSSOutput(inp);

            var builder = new MSCompleteSimple
            {
                ShortcutSetFactory = new ShortcutIntervalSetFactory()
            };

            var checker = new MSSChinChan.ShortcutChecker(inp, outp);

            outp.Logged += str =>
            {
                this.InvokeIfRequired(() =>
                {
                    shortcutFindingProgressLabel.Text = str;
                });
            };

            await Task.Run(() =>
            {
                outp.LogLine("Started...");
                var shortcutSet = (MSSimpleShortcutSet) builder.FindShortcuts(checker, true);
                input.ShortcutSet = shortcutSet.ExtractShortcuts(1);
            });

            var regions = (ShortcutIntervalSet) input.ShortcutSet;
            shortcutFindingProgressLabel.Text = "Shortcuts: " + regions.Count + " Intervals: " + regions.IntervalCount;
        }

        private void startToEndCheckbox_Click(object sender, System.EventArgs e)
        {
            customRangeCheckbox.CheckState = startToEndCheckbox.Checked ? CheckState.Unchecked : CheckState.Checked;

            if (startToEndCheckbox.Checked)
            {
                sourceIndexUpDown.Value = 0;
                targetIndexUpdown.Value = input.Trajectory.Last().Index;
            }

            //UpdateSourceAndTarget();
        }

        private void customRangeCheckbox_Click(object sender, System.EventArgs e)
        {
            startToEndCheckbox.CheckState = customRangeCheckbox.Checked ? CheckState.Unchecked : CheckState.Checked;

            if (startToEndCheckbox.Checked)
            {
                sourceIndexUpDown.Value = 0;
                targetIndexUpdown.Value = input.Trajectory.Last().Index;
            }

            //UpdateSourceAndTarget();
        }

        private void sourceIndexUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateSourceAndTarget();
        }

        private void targetIndexUpdown_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateSourceAndTarget();
        }

        private void UpdateSourceAndTarget()
        {
            if (input.Trajectory.Count == 0)
                return;

            if (startToEndCheckbox.Checked)
            {
                input.Source = input.Trajectory.First();
                input.Targets.Clear();
                input.Targets.Add(input.Trajectory.Last());
            }
            else
            {
                var sourceIndex = (int)sourceIndexUpDown.Value;
                var targetIndex = (int)targetIndexUpdown.Value;

                input.Source = input.Trajectory[sourceIndex];
                input.Targets.Clear();
                input.Targets.Add(input.Trajectory[targetIndex]);
            }
        }
    }
}
