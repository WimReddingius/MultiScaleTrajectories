using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm : Form
    {
    
        public AlgorithmForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();
        }

        public void AddControllers(params IAlgorithmController[] controllers)
        {
            foreach (var algorithmController in controllers)
            {
                algorithmController.AlgorithmView.Initialize(baseSplitContainer.Panel1);
            }

            algorithmTypeComboBox.DataSource = controllers;
            algorithmTypeComboBox.DisplayMember = "Name";
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoController = (IAlgorithmController) algorithmTypeComboBox.SelectedItem;
            FormsUtil.FillContainer(configurationSplitContainer.Panel2, algoController.AlgorithmView);
        }

    }
}
