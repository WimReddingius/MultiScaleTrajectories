using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.View;
using MultiScaleTrajectories.View.SingleTrajectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MultiScaleTrajectories
{
    partial class MainForm : Form
    {

        IAlgorithmType AlgorithmType;

        public MainForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();

            RegisterAlgorithmTypes();
            OpenSettings();
        }

        private void OpenSettings()
        {
            string inputFile = (string)Properties.Settings.Default["InputFile"];
            if (!string.IsNullOrEmpty(inputFile))
            {
                OpenInputFile(inputFile);
            }
        }

        private void RegisterAlgorithmTypes()
        {
            algorithmTypeComboBox.Items.Clear();
            algorithmTypeComboBox.Items.Add(new SingleTrajectory());
            algorithmTypeComboBox.SelectedItem = algorithmTypeComboBox.Items[0];
            AlgorithmType = (IAlgorithmType) algorithmTypeComboBox.SelectedItem;
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            AlgorithmType.VisualizeOutput();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            AlgorithmType.VisualizeInput();
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            object algo = algorithmComboBox.SelectedItem;
            AlgorithmType.SetAlgorithm(algo);
            algorithmComboBox.SelectedItem = algo;
            algorithmComboBox.Text = algo.ToString();
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlgorithmType = (IAlgorithmType) algorithmTypeComboBox.SelectedItem;
            SetAlgorithmType(AlgorithmType);

            algorithmTypeComboBox.SelectedItem = AlgorithmType;
            algorithmTypeComboBox.Text = AlgorithmType.ToString();
        }

        private void SetAlgorithmType(IAlgorithmType algoType)
        {
            //register algorithms
            algorithmComboBox.Items.Clear();
            List<object> algorithms = AlgorithmType.GetAlgorithms();

            foreach (object algorithm in algorithms)
            {
                algorithmComboBox.Items.Add(algorithm);
            }

            //by default simply select first algorithm
            algorithmComboBox.SelectedItem = algorithmComboBox.Items[0];

            //set controls corresponding to algorithm type
            SetInputControl(AlgorithmType.GetInputControl());
            SetGLControl(AlgorithmType.GetVisualizationControl());
        }

        private void SetInputControl(UserControl userControl)
        {
            inputPanelContainer.Controls.Clear();
            inputPanelContainer.Controls.Add(userControl);
            userControl.Dock = System.Windows.Forms.DockStyle.Fill;
            userControl.Location = new System.Drawing.Point(0, 0);
        }

        private void SetGLControl(GLControl control)
        {
            control.MakeCurrent();
            splitContainer1.Panel1.Controls.Clear();
            splitContainer1.Panel1.Controls.Add(control);
            control.Dock = System.Windows.Forms.DockStyle.Fill;
            control.Location = new System.Drawing.Point(0, 0);
        }

        private void openInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = openInputDialog.FileName;
                OpenInputFile(fileName);
                Properties.Settings.Default["InputFile"] = fileName;
            }
        }

        private void OpenInputFile(string fileName)
        {
            try
            {
                string input = File.ReadAllText(fileName);

                JObject jObject = JObject.Parse(input);
                string algoName = (string)jObject["Algorithm"];
                string algoTypeName = (string)jObject["AlgorithmType"];

                Type algoTypeType = Type.GetType(algoTypeName, true);
                foreach (object algorithmType in algorithmTypeComboBox.Items)
                {
                    if (algorithmType.GetType() == algoTypeType)
                    {
                        algorithmTypeComboBox.SelectedItem = algorithmType;
                    }
                }

                Type algoType = Type.GetType(algoName, true);
                foreach (object algorithm in algorithmComboBox.Items)
                {
                    if (algorithm.GetType() == algoType)
                    {
                        algorithmComboBox.SelectedItem = algorithm;
                    }
                }

                jObject.Remove("Algorithm");
                jObject.Remove("AlgorithmType");

                AlgorithmType.DeSerializeInput(jObject.ToString());
            }
            catch (IOException)
            {
            }
        }

        private void saveInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = saveInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveInputDialog.FileName;
                try
                {
                    string input = AlgorithmType.SerializeInput();

                    JObject jObject = JObject.Parse(input);
                    jObject.Add("Algorithm", algorithmComboBox.SelectedItem.GetType().AssemblyQualifiedName);
                    jObject.Add("AlgorithmType", algorithmTypeComboBox.SelectedItem.GetType().AssemblyQualifiedName);

                    File.WriteAllText(fileName, jObject.ToString());

                    Properties.Settings.Default["InputFile"] = fileName;
                }
                catch (IOException)
                {
                }
            }
        }

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            AlgorithmType.ClearInput();
            AlgorithmType.VisualizeInput();
            Properties.Settings.Default["InputFile"] = "";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

    }
}
