using MultiScaleTrajectories.algorithm.ST;
using MultiScaleTrajectories.view;
using OpenTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiScaleTrajectories
{
    public partial class MainForm : Form
    {

        Visualizer visualizer;

        public MainForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();
            visualizer = new Visualizer();
            splitContainer1.Panel1.Controls.Add(visualizer);
            visualizer.Dock = System.Windows.Forms.DockStyle.Fill;            
            visualizer.Location = new System.Drawing.Point(0, 0);
            visualizer.Size = new System.Drawing.Size(410, 407);
            splitContainer1.Panel1.ResumeLayout(false);

            registerAlgorithms();
        }

        private void registerAlgorithms()
        {
            algorithmComboBox.Items.Add(new STShortcutShortestPath());
            algorithmComboBox.SelectedItem = algorithmComboBox.Items[0];
        }

        private void addLevelButton_Click(object sender, EventArgs e)
        {
            int rowCount = levelTable.RowCount;
            int closeness = 10;

            if (rowCount > 0)
                closeness = (int) levelTable.Rows[levelTable.RowCount - 1].Cells["Closeness"].Value;

            levelTable.Rows.Add(rowCount + 1, closeness);
            visualizer.InputEpsilons.Add(closeness);
        }

        private void removeLevelButton_Click(object sender, EventArgs e)
        {
            int rowCount = levelTable.RowCount;

            if (rowCount > 0) {
                levelTable.Rows.RemoveAt(rowCount - 1);
                visualizer.InputEpsilons.RemoveAt(rowCount - 1);
            }
            
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            visualizer.visualizeSolution();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            visualizer.SwitchMode(VisualizationMode.EDIT);
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (algorithmComboBox.SelectedItem is STAlgorithm)
            {
                STAlgorithm algo = (STAlgorithm)algorithmComboBox.SelectedItem;
                visualizer.CurrentAlgorithm = algo;
                algorithmComboBox.Text = algo.ToString();
            }
        }

        private void levelTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            visualizer.InputEpsilons[row] = (double) levelTable.Rows[row].Cells["Closeness"].Value;
        }

        private void levelTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int index = (int)e.Row.Cells["Level"].Value;
            visualizer.InputEpsilons.RemoveAt(index);
        }
    }
}
