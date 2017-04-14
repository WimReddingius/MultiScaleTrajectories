﻿using System;
using System.Globalization;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.View.Edit
{
    partial class SFInputEditor : UserControl, IInputEditor<ShortcutFinderInput>
    {
        private readonly InputEditor<SingleTrajectoryInput> trajectoryEditor;
        private ShortcutFinderInput input;

        public SFInputEditor(object editor)
        {
            InitializeComponent();

            trajectoryEditor = InputEditor<SingleTrajectoryInput>.CreateSimple(editor);

            this.Fill(trajectoryEditor, false);
            epsilonContainer.BringToFront();

            Name = trajectoryEditor.Name;
        }

        public void LoadInput(ShortcutFinderInput input)
        {
            this.input = input;
            epsilonTextBox.Text = input.Epsilon.ToString(CultureInfo.InvariantCulture);
            trajectoryEditor.LoadInput(input);
        }

        private void epsilonTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (epsilonTextBox.Text != "")
            {
                try
                {
                    var epsilon = double.Parse(epsilonTextBox.Text, CultureInfo.InvariantCulture);
                    input.Epsilon = epsilon;
                }
                catch (Exception ex)
                {
                    FormsUtil.ShowErrorMessage(ex.ToString());
                    epsilonTextBox.Text = "";
                }
            }
        }
    }
}
