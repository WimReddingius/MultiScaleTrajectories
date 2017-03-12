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
            FormsUtil.FillContainer(baseSplitContainer.Panel1, algoView.VisualizationContainer);
            FormsUtil.FillContainer(configurationSplitContainer.Panel2, algoView);
        }

    }
}
