using System;
using System.IO;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Controller;
using Newtonsoft.Json.Linq;

namespace MultiScaleTrajectories.View
{
    partial class AlgoTabControl<TIn, TOut> : UserControl where TIn : Input, new() where TOut : Output, new()
    {

        private readonly IAlgoController<TIn, TOut> Controller;
        private readonly Control ViewContainer;

        public AlgoTabControl(Control viewContainer, IAlgoController<TIn, TOut> controller)
        {
            InitializeComponent();

            Controller = controller;
            ViewContainer = viewContainer;

            //new run
            Controller.Run = new AlgorithmRunnable<TIn, TOut>();
            Controller.InputController.LoadData(Controller.Run.Input);

            //setup input controller
            FormsUtil.FillContainer(inputOptionsPanel, Controller.InputController.OptionsControl);

            //register algorithms
            algorithmComboBox.Items.Clear();
            algorithmComboBox.Items.AddRange(Controller.Algorithms.ToArray());
            algorithmComboBox.SelectedItem = algorithmComboBox.Items[0];

            //register output controllers
            outputControllerComboBox.Items.Clear();
            outputControllerComboBox.Items.AddRange(Controller.OutputControllers.ToArray());
            outputControllerComboBox.SelectedItem = outputControllerComboBox.Items[0];

            ViewData(Controller.InputController);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage page = tabControl.SelectedTab;

            if (page == runTabPage || page == inputTabPage)
                ViewData(Controller.InputController);
            else if (page == outputTabPage)
            {
                if (Controller.Run.Output != null)
                    ViewData(GetCurrentOutputController());
            }
        }

        private void algorithmComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            IAlgorithm<TIn, TOut> algo = (IAlgorithm<TIn, TOut>) algorithmComboBox.SelectedItem;
            Controller.Run.Algorithm = algo;
        }

        private void outputControllerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataViewController<TOut> outputController = GetCurrentOutputController();

            outputController.LoadData(Controller.Run.Output);

            //set controls: options and view
            FormsUtil.FillContainer(outputViewOptionsPanel, outputController.OptionsControl);
            FormsUtil.FillContainer(ViewContainer, outputController.ViewControl);
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
                Controller.Run.Input.LoadSerialized(input);
            }
            catch (IOException)
            {
            }
        }

        //TODO: make more robust when used for wrong algo type
        //TODO: settings loading
        private void saveInputButton_Click(object sender, EventArgs e)
        {
            DialogResult result = saveInputDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveInputDialog.FileName;
                try
                {
                    string input = Controller.Run.Input.Serialize();

                    JObject jObject = JObject.Parse(input);
                    jObject.Add("AlgorithmType", Controller.GetType().AssemblyQualifiedName);

                    File.WriteAllText(fileName, input);

                    Properties.Settings.Default["InputFile"] = fileName;
                }
                catch (IOException)
                {
                }
            }
        }

        private void clearInputButton_Click(object sender, EventArgs e)
        {
            Controller.Run.Input.Clear();
            Properties.Settings.Default["InputFile"] = "";
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            Controller.Run.Run();
        }

        private DataViewController<TOut> GetCurrentOutputController()
        {
            return (DataViewController<TOut>) outputControllerComboBox.SelectedItem;
        }

        private void ViewData<T>(DataViewController<T> dataViewController)
        {
            FormsUtil.FillContainer(ViewContainer, dataViewController.ViewControl);
            dataViewController.ViewControl?.Focus();
        }

    }
}
