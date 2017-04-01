using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Util;

namespace MultiScaleTrajectories.SingleTrajectory.View
{
    partial class STMoveBankDialog : Form
    {
        public Trajectory2D ChosenTrajectory;
        public string ChosenTrajectoryName;
        private readonly Dictionary<string, Trajectory2D> trajectories;

        public STMoveBankDialog(string fileName)
        {
            InitializeComponent();

            trajectories = MoveBank.ReadTrajectories(fileName);

            foreach (var name in trajectories.Keys)
            {
                trajectoryTable.Rows.Add(name, trajectories[name].Count);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, System.EventArgs e)
        {
            if (trajectoryTable.SelectedRows.Count == 0)
                return;

            var name = (string) trajectoryTable.SelectedRows[0].Cells[0].Value;
            var trajectory = trajectories[name];

            trajectoryGMap.DrawSingleTrajectory(trajectory);
            trajectoryGMap.LookAtTrajectory(trajectory);

            ChosenTrajectory = trajectory;
            ChosenTrajectoryName = name;
        }
    }
}
