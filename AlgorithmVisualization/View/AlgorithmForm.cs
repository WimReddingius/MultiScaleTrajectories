using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm : Form
    {
        public readonly BindingList<IAlgorithmController> AlgoControllers;

        public AlgorithmForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();
            AlgorithmControllerSettingsManager.Init();

            AlgoControllers = new BindingList<IAlgorithmController>();
            AlgoControllers.ListChanged += (o, e) =>
            {
                algorithmTypeComboBox.DataSource = AlgoControllers;
                algorithmTypeComboBox.DisplayMember = "Name";
            };
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoView = ((IAlgorithmController) algorithmTypeComboBox.SelectedItem).AlgorithmView;

            baseSplitContainer.Panel1.Fill(algoView.VisualizationContainer);
            configurationSplitContainer.Panel2.Fill(algoView);
            algoView.Reset();
        }

        private void AlgorithmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AlgorithmControllerSettingsManager.Save();
        }
    }
}
