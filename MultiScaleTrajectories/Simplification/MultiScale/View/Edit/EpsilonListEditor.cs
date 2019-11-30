using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class EpsilonListEditor : UserControl
    {
        private MSInput input;

        public EpsilonListEditor()
        {
            InitializeComponent();

            Closeness.ValueType = typeof(double);

            var errorSamplers = new BindingList<IErrorSampler>
            {
                new ZoomAwareErrorSampler(),
                new ShortcutErrorSampler()
            };

            foreach (var sampler in errorSamplers)
            {
                sampler.NewSamples += NewSamples;
            }

            errorSamplerComboBox.DataSource = errorSamplers;
            errorSamplerComboBox.Format += (o, e) => e.Value = ((IErrorSampler)e.Value).TypeName;
            errorSamplerComboBox_SelectedIndexChanged(null, null);
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

        public void LoadInput(MSInput inp)
        {
            input = inp;
            levelTable.Rows.Clear();

            for (int level = 1; level <= input.NumLevels; level++)
            {
                levelTable.Rows.Add(level, input.GetEpsilon(level));
            }

            foreach (var sampler in errorSamplerComboBox.Items) {
                ((IErrorSampler)sampler).LoadInput(inp);
            }
        }
        
        private void ClearLevels()
        {
            for (var level = input.NumLevels; level >= 1; level--)
            {
                RemoveLevel(level);
            }
        }

        private void NewSamples(List<double> samples)
        {
            ClearLevels();
            for (var level = 1; level <= samples.Count; level++)
            {
                InsertLevel(level, samples[level - 1]);
            }
        }

        private void errorSamplerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sampler = ((IErrorSampler)errorSamplerComboBox.SelectedItem);
            errorSamplerSplitPanel.Panel2.Fill(sampler.Control);
        }
    }
}
