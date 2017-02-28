using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.Controller.SingleTrajectory;
using MultiScaleTrajectories.Controller.SingleTrajectory.Output;
using MultiScaleTrajectories.View.SingleTrajectory;
using Newtonsoft.Json.Linq;

namespace MultiScaleTrajectories.View
{
    partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            RegisterAlgoTypeControllers();
        }

        private void RegisterAlgoTypeControllers()
        {
            algorithmTypeComboBox.Items.Clear();

            algorithmTypeComboBox.Items.Add(new STController());
            algorithmTypeComboBox.SelectedItem = algorithmTypeComboBox.Items[0];
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var controller = (IAlgoView) algorithmTypeComboBox.SelectedItem;

            //set controls corresponding to algorithm type
            FormsUtil.FillContainer(configurationSplitContainer.Panel2, controller.ConfigurationControl);
            FormsUtil.FillContainer(baseSplitContainer.Panel1, controller.ViewControl);
        }

    }
}
