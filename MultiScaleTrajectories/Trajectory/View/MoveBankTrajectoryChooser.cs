using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.AlgoUtil.Geometry;

namespace MultiScaleTrajectories.Trajectory.View
{
    partial class MoveBankTrajectoryChooser : Form
    {
        public Trajectory2D ChosenTrajectory;
        public string ChosenTrajectoryName;
        private readonly Dictionary<string, Trajectory2D> trajectories;

        public MoveBankTrajectoryChooser(string fileName)
        {
            InitializeComponent();

            toPointChooser.Maximum = decimal.MaxValue;
            skipFrequencyChooser.Maximum = decimal.MaxValue;
            skipAmountChooser.Maximum = decimal.MaxValue;

            trajectories = MoveBank.ReadTrajectories(fileName);

            foreach (var name in trajectories.Keys)
            {
                trajectoryTable.Rows.Add(name, trajectories[name].Count);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (trajectoryTable.SelectedRows.Count == 0)
                return;

            var fileTrajectoryName = (string)trajectoryTable.SelectedRows[0].Cells[0].Value;
            var fileTrajectory = trajectories[fileTrajectoryName];

            fromPointChooser.ValueChanged -= fromPointChooser_ValueChanged;
            fromPointChooser.Value = 0;
            fromPointChooser.ValueChanged += fromPointChooser_ValueChanged;

            toPointChooser.ValueChanged -= toPointChooser_ValueChanged;
            toPointChooser.Value = fileTrajectory.Count - 1;
            toPointChooser.ValueChanged += toPointChooser_ValueChanged;

            CalculateTrajectory();
        }

        private void toPointChooser_ValueChanged(object sender, EventArgs e)
        {
            CalculateTrajectory(false);
        }

        private void fromPointChooser_ValueChanged(object sender, EventArgs e)
        {
            CalculateTrajectory(false);
        }

        private void skipAmountChooser_ValueChanged(object sender, EventArgs e)
        {
            CalculateTrajectory(false);
        }

        private void skipFrequencyChooser_ValueChanged(object sender, EventArgs e)
        {
            CalculateTrajectory(false);
        }

        private void CalculateTrajectory(bool lookAtTrajectory = true)
        {
            if (trajectoryTable.SelectedRows.Count == 0)
                return;

            var fileTrajectoryName = (string)trajectoryTable.SelectedRows[0].Cells[0].Value;
            var fileTrajectory = trajectories[fileTrajectoryName];
            var newTrajectory = GetSubTrajectory(fileTrajectory, (int)fromPointChooser.Value, (int)toPointChooser.Value, (int)skipAmountChooser.Value, (int)skipFrequencyChooser.Value);

            trajectoryGMap.DrawSingleTrajectory(newTrajectory);

            if (lookAtTrajectory)
                trajectoryGMap.LookAtTrajectory(newTrajectory);

            ChosenTrajectory = newTrajectory;
            ChosenTrajectoryName = fileTrajectoryName
                + " (" + newTrajectory.Count + " / " + (int)skipAmountChooser.Value + "-" + (int)skipFrequencyChooser.Value + ")";

            pointCountLabel.Text = "Original: " + fileTrajectory.Count + ". Pruned: " + newTrajectory.Count;
        }

        private Trajectory2D GetSubTrajectory(Trajectory2D trajectory, int start, int end, int skipAmount, int skipFrequency)
        {
            var newTrajectory = new Trajectory2D();

            var skipCount = 0;
            for (var i = start; i <= end; i++)
            {
                newTrajectory.AppendPoint(trajectory[i].X, trajectory[i].Y);
                skipCount++;

                if (skipCount == skipFrequency)
                {    
                    i += skipAmount;
                    skipCount = 0;
                }
            }
            return newTrajectory;
        }
    }
}
