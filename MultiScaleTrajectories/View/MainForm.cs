using System;
using System.IO;
using System.Windows.Forms;
using MultiScaleTrajectories.Controller;
using MultiScaleTrajectories.Controller.SingleTrajectory;
using Newtonsoft.Json.Linq;

namespace MultiScaleTrajectories.View
{
    partial class MainForm : Form
    {

        AlgoTypeController AlgoController;
        private ViewMode CurrentViewMode;

        public MainForm()
        {
            InitializeComponent();

            RegisterAlgorithmTypes();
            OpenSettings();

            CurrentViewMode = ViewMode.Idle;
            SwitchViewMode(ViewMode.Input);
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
            AlgoController.CurrentAlgorithm = algo;
            algorithmComboBox.SelectedItem = algo;
        }

        private void algorithmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlgoController = (AlgoTypeController) algorithmTypeComboBox.SelectedItem;

            //register algorithms
            algorithmComboBox.Items.Clear();
            algorithmComboBox.Items.AddRange(AlgoController.Algorithms.ToArray());
            algorithmComboBox.SelectedItem = algorithmComboBox.Items[0];

            //register view modes
            outputControllerComboBox.Items.Clear();
            outputControllerComboBox.Items.AddRange(AlgoController.OutputControllers.ToArray());
            outputControllerComboBox.SelectedItem = outputControllerComboBox.Items[0];
                
            //set controls corresponding to algorithm type
            FillContainer(inputOptionsPanel, AlgoController.InputController.OptionsControl);
        }

        private void outputControllerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            AlgoController.CurrentOutputController = (IOutputController)outputControllerComboBox.SelectedItem;

            //set controls: options and view
            FillContainer(outputViewOptionsPanel, AlgoController.CurrentOutputController.OptionsControl);
            UpdateViewPanel();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0: //Input
                    SwitchViewMode(ViewMode.Input);
                    break;
                case 1: //Output
                    AlgoController.StartRun();
                    SwitchViewMode(ViewMode.Output);
                    break;
            }
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
                System.Type algoTypeType = System.Type.GetType(algoTypeName, true);

                foreach (object algorithmType in algorithmTypeComboBox.Items)
                {
                    if (algorithmType.GetType() == algoTypeType)
                    {
                        algorithmTypeComboBox.SelectedItem = algorithmType;
                    }
                }

                jObject.Remove("AlgorithmType");

                AlgoController.InputController.LoadSerializedInput(jObject.ToString());
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
                    string input = AlgoController.InputController.SerializeInput();

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
            AlgoController.InputController.LoadFreshInput();
            Properties.Settings.Default["InputFile"] = "";
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void SwitchViewMode(ViewMode newMode)
        {
            if (newMode != CurrentViewMode)
            {
                CurrentViewMode = newMode;
                UpdateViewPanel();
            }
        }

        private void UpdateViewPanel()
        {
            Control newControl = null;
            switch (CurrentViewMode)
            {
                case ViewMode.Input:
                    newControl = AlgoController.InputController.ViewControl;
                    break;
                case ViewMode.Output:
                    newControl = AlgoController.CurrentOutputController.ViewControl;
                    break;
            }

            FillContainer(splitContainer.Panel1, newControl);
            newControl?.Focus();
        }

        private void FillContainer(Control container, Control control)
        {
            if (control != null)
            {
                container.Controls.Clear();

                control.CreateControl();
                container.Controls.Add(control);

                control.Dock = DockStyle.Fill;
                control.Location = new System.Drawing.Point(0, 0);
            }
        }

        private enum ViewMode
        {
            Idle,
            Input,
            Output
        }

    }
}
