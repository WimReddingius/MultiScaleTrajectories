using System;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    partial class STInputOptions : UserControl, IInputLoader<STInput>
    {
        private STInput Input;

        public STInputOptions()
        {
            InitializeComponent();

            levelTable.Columns["Closeness"].ValueType = typeof(double);
        }

        private void levelTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int level = (int)e.Row.Cells["Level"].Value;
            Input.RemoveLevel(level);
        }

        private void addLevelButton_Click(object sender, EventArgs e)
        {
            var insertAt = levelTable.Rows.Count;

            if (levelTable.SelectedCells.Count > 0)
                insertAt = levelTable.SelectedCells[0].RowIndex + 1;

            double epsilon = double.PositiveInfinity;
            if (levelTable.RowCount > insertAt)
                epsilon = (double)levelTable.Rows[insertAt].Cells["Closeness"].Value;

            InsertLevel(insertAt + 1, epsilon);
        }

        private void removeLevelButton_Click(object sender, EventArgs e)
        {
            if (levelTable.RowCount > 1)
            {
                RemoveLevel(levelTable.RowCount);
            }
        }

        private void levelTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int level = e.RowIndex + 1;
            Input.SetEpsilon(level, (double)levelTable.Rows[e.RowIndex].Cells["Closeness"].Value);
        }

        private void RemoveLevel(int level)
        {
            if (level > 0)
            {
                levelTable.Rows.RemoveAt(level - 1);
                Input.RemoveLevel(level);
            }
        }

        private void PrependLevel(double epsilon)
        {
            InsertLevel(1, epsilon);
        }

        private void InsertLevel(int level, double epsilon)
        {
            levelTable.Rows.Insert(level - 1, level, epsilon);
            Input.InsertLevel(level, epsilon);

            for (int i = level; i < levelTable.RowCount; i++)
            {
                levelTable.Rows[i].Cells["Level"].Value = (int)levelTable.Rows[i].Cells["Level"].Value + 1;
            }
        }

        public void LoadInput(STInput input)
        {
            Input = input;
            levelTable.Rows.Clear();

            for (int level = 1; level <= Input.NumLevels; level++)
            {
                levelTable.Rows.Add(level, Input.GetEpsilon(level));
            }
        }

    }
}
