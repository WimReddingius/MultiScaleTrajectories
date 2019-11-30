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

            toPointChooser.Maximum = fileTrajectory.Count;
            downSampleToChooser.Maximum = fileTrajectory.Count;

            fromPointChooser.ValueChanged -= fromPointChooser_ValueChanged;
            fromPointChooser.Value = 1;
            fromPointChooser.ValueChanged += fromPointChooser_ValueChanged;

            toPointChooser.ValueChanged -= toPointChooser_ValueChanged;
            toPointChooser.Value = fileTrajectory.Count;
            toPointChooser.ValueChanged += toPointChooser_ValueChanged;

            downSampleToChooser.ValueChanged -= downSampleToChooser_ValueChanged;
            downSampleToChooser.Value = fileTrajectory.Count;
            downSampleToChooser.ValueChanged += downSampleToChooser_ValueChanged;

            CalculateTrajectory();
        }

        private void toPointChooser_ValueChanged(object sender, EventArgs e)
        {
            fromPointChooser.Maximum = (int)toPointChooser.Value;
            downSampleToChooser.Maximum = (int)toPointChooser.Value - (int)fromPointChooser.Value + 1;
            
            CalculateTrajectory(false);
        }

        private void fromPointChooser_ValueChanged(object sender, EventArgs e)
        {
            toPointChooser.Minimum = (int)fromPointChooser.Value;
            downSampleToChooser.Maximum = (int)toPointChooser.Value - (int)fromPointChooser.Value + 1;
            CalculateTrajectory(false);
        }

        private void downSampleToChooser_ValueChanged(object sender, EventArgs e)
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
            var newTrajectory = GetSubTrajectory(fileTrajectory, (int)fromPointChooser.Value, (int)toPointChooser.Value, (int)downSampleToChooser.Value);

            trajectoryGMap.DrawSingleTrajectory(newTrajectory);

            if (lookAtTrajectory)
                trajectoryGMap.LookAtTrajectory(newTrajectory);

            ChosenTrajectory = newTrajectory;
            ChosenTrajectoryName = fileTrajectoryName + "Starting from point " + (int)fromPointChooser.Value + " to point " + (int)toPointChooser.Value + "downsampled to " + (int)downSampleToChooser.Value;
        }

        private Trajectory2D GetSubTrajectory(Trajectory2D trajectory, int start, int end, int downSampleAmount)
        {
            var newTrajectory = new Trajectory2D();
            var originalCount = end - start;

            for (var i = 0; i < downSampleAmount; i++)
            {
                var index = (int)Math.Floor(((double)(start - 1 + (i * originalCount))) / downSampleAmount);
                var newPoint = trajectory[index];
                newTrajectory.AppendPoint(newPoint.X, newPoint.Y);
            }
            return newTrajectory;
        }
    }
}
