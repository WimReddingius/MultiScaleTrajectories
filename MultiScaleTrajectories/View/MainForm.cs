using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.Controller.SingleTrajectory;

namespace MultiScaleTrajectories.View
{
    partial class MainForm : Form
    {

        public Control LargeContainer => baseSplitContainer.Panel1;


        public MainForm()
        {
            InitializeComponent();

            RegisterAlgoTypeControllers();
        }

        private void RegisterAlgoTypeControllers()
        {
            algorithmTypeComboBox.Items.Clear();

            List<IAlgorithmController> controllers = new List<IAlgorithmController>
            {
                new STController(LargeContainer)
            };

            algorithmTypeComboBox.DataSource = controllers;
            algorithmTypeComboBox.DisplayMember = "Name";
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoController = (IAlgorithmController) algorithmTypeComboBox.SelectedItem;
            FormsUtil.FillContainer(configurationSplitContainer.Panel2, algoController.Control);
        }

    }
}
