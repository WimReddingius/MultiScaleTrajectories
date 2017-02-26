using System;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using Newtonsoft.Json;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Controller;

namespace MultiScaleTrajectories.Controller.SingleTrajectory
{
    partial class STInputController : UserControl, IInputController
    {

        AlgorithmRunner<STInput, STOutput> AlgorithmRunner;

        public STInputController(AlgorithmRunner<STInput, STOutput> runner)
        {
            InitializeComponent();

            levelTable.Columns["Closeness"].ValueType = typeof(double);

            AlgorithmRunner = runner;
        }

        private void levelTable_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int level = (int)e.Row.Cells["Level"].Value;
            AlgorithmRunner.Input.RemoveLevel(level);
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
            AlgorithmRunner.Input.SetEpsilon(level, (double)levelTable.Rows[e.RowIndex].Cells["Closeness"].Value);
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
                AlgorithmRunner.Input.RemoveLevel(level);
            }
        }

        private void AppendLevel(double epsilon)
        {
            InsertLevel(levelTable.RowCount + 1, epsilon);
        }

        private void InsertLevel(int level, double epsilon)
        {
            levelTable.Rows.Insert(level - 1, level, epsilon);
            AlgorithmRunner.Input.InsertLevel(level, epsilon);
        }

        public void LoadInput(STInput Input)
        {
            AlgorithmRunner.Input.Load(Input.Trajectory, Input.Epsilons);

            levelTable.Rows.Clear();

            for (int level = 1; level <= Input.NumLevels; level++)
            {
                levelTable.Rows.Add(level, Input.GetEpsilon(level));
            }
        }

        public void ClearInput()
        {
            AlgorithmRunner.Input.Clear();
            levelTable.Rows.Clear();
        }

        public void LoadSerializedInput(string inputString)
        {
            LoadInput(JsonConvert.DeserializeObject<STInput>(inputString));
        }

        public string SerializeInput()
        {
            return JsonConvert.SerializeObject(AlgorithmRunner.Input);
        }

        public Control GetOptionsControl()
        {
            return this;
        }

    }
}
