using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.Controller.SingleTrajectory;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace MultiScaleTrajectories
{
    partial class MainForm : Form
    {

        AlgoTypeController AlgorithmType;

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
            algorithmTypeComboBox.Items.Add(new STController());
            algorithmTypeComboBox.SelectedItem = algorithmTypeComboBox.Items[0];
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            object algo = algorithmComboBox.SelectedItem;
            AlgorithmType.SetAlgorithm(algo);
            algorithmComboBox.SelectedItem = algo;
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlgorithmType = (AlgoTypeController) algorithmTypeComboBox.SelectedItem;

            //register algorithms
            algorithmComboBox.Items.Clear();
            algorithmComboBox.Items.AddRange(AlgorithmType.Algorithms.ToArray());
            algorithmComboBox.SelectedItem = algorithmComboBox.Items[0];

            //register view modes
            viewTypeComboBox.Items.Clear();
            viewTypeComboBox.Items.AddRange(AlgorithmType.ViewControllers.ToArray());
            viewTypeComboBox.SelectedItem = viewTypeComboBox.Items[0];
                
            //set controls corresponding to algorithm type
            FillContainer(inputPanel, AlgorithmType.InputController.GetOptionsControl());
        }

        private void viewTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlgorithmType.CurrentViewType = (ViewTypeController)viewTypeComboBox.SelectedItem;

            //set controls: options and view
            FillContainer(viewTabPage, AlgorithmType.CurrentViewType.GetOptionsControl());
            FillContainer(splitContainer1.Panel1, AlgorithmType.CurrentViewType.GetViewControl());
        }

        private void FillContainer(Control container, Control control)
        {
            container.Controls.Clear();
            container.Controls.Add(control);
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
                string algoTypeName = (string)jObject["AlgorithmType"];
                Type algoTypeType = Type.GetType(algoTypeName, true);

                foreach (object algorithmType in algorithmTypeComboBox.Items)
                {
                    if (algorithmType.GetType() == algoTypeType)
                    {
                        algorithmTypeComboBox.SelectedItem = algorithmType;
                    }
                }

                jObject.Remove("AlgorithmType");

                AlgorithmType.InputController.LoadSerializedInput(jObject.ToString());
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
                    string input = AlgorithmType.InputController.SerializeInput();

                    JObject jObject = JObject.Parse(input);
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
            AlgorithmType.InputController.ClearInput();
            //TODO: update visualization via events
            Properties.Settings.Default["InputFile"] = "";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void algorithmLabel_Click(object sender, EventArgs e)
        {

        }

    }
}
