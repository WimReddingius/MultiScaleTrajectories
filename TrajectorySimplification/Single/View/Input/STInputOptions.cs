using System;
using System.Windows.Forms;
using AlgorithmVisualization.View;
using AlgorithmVisualization.View.Data;
using TrajectorySimplification.Single.Algorithm;

namespace MultiScaleTrajectories.View.SingleTrajectory.Input
{
    partial class STInputOptions : DataView<STInput>
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
            int rowCount = levelTable.RowCount;
            double epsilon = 10.0;

            if (rowCount > 0)
                epsilon = (double)levelTable.Rows[rowCount - 1].Cells["Closeness"].Value;

            PrependLevel(epsilon);
        }

        private void removeLevelButton_Click(object sender, EventArgs e)
        {
            RemoveLastLevel();
        }

        private void levelTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int level = e.RowIndex + 1;
            Input.SetEpsilon(level, (double)levelTable.Rows[e.RowIndex].Cells["Closeness"].Value);
        }

        private void RemoveLastLevel()
        {
            RemoveLevel(levelTable.RowCount);
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

        public override void LoadData(STInput input)
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
