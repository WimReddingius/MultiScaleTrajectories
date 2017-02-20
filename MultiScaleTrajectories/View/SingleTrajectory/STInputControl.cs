using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;

namespace MultiScaleTrajectories.View.SingleTrajectory
{
    partial class STInputControl : UserControl
    {

        STInput Input;

        public STInputControl(STInput input)
        {
            InitializeComponent();

            levelTable.Columns["Closeness"].ValueType = typeof(double);

            this.Input = input;
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

            AppendLevel(epsilon);
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

        private void AppendLevel(double epsilon)
        {
            InsertLevel(levelTable.RowCount + 1, epsilon);
        }

        private void InsertLevel(int level, double epsilon)
        {
            levelTable.Rows.Insert(level - 1, level, epsilon);
            Input.InsertLevel(level, epsilon);
        }

        public void LoadInput(STInput Input)
        {
            this.Input = Input;

            levelTable.Rows.Clear();

            for (int level = 1; level <= Input.NumLevels; level++)
            {
                levelTable.Rows.Add(level, Input.GetEpsilon(level));
            }
        }

    }
}
