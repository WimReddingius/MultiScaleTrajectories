using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.Util.Factory;
using AlgorithmVisualization.Util.Naming;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm : Form
    {
        private NamingList<AlgorithmControllerBase> algoControllers;
        private AlgorithmControllerBase selectedAlgoController => (AlgorithmControllerBase)controllerComboBox.SelectedItem;
        private IFactory<AlgorithmControllerBase> selectedAlgoControllerFactory => (IFactory<AlgorithmControllerBase>)controllerFactoryComboBox.SelectedItem;


        public AlgorithmForm(IList<Func<AlgorithmControllerBase>> controllers)
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();

            var factories = new NamingList<NameableFactory<AlgorithmControllerBase>>();
            var defaultControllers = new List<AlgorithmControllerBase>();

            foreach (var func in controllers)
            {
                var factory = new NameableFactory<AlgorithmControllerBase>(func, func().Name);
                factories.Add(factory);
                defaultControllers.Add(factory.Create());
            }

            controllerFactoryComboBox.DataSource = factories;
            controllerFactoryComboBox.Format += (s, ev) => { ev.Value = ((INameable)ev.Value).Name; };

            SetControllers(defaultControllers);
        }

        private void SetControllers(IList<AlgorithmControllerBase> controllers)
        {
            algoControllers = new NamingList<AlgorithmControllerBase>();

            algoControllers.ListChanged += (o, e) =>
            {
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    var controller = algoControllers[e.NewIndex];
                    controller.NameChanged += (ob, ev) => RedrawProblemComboBox();
                }
                removeControllerButton.Enabled = algoControllers.Count > 0;
            };

            foreach (var controller in controllers)
            {
               algoControllers.Add(controller);
            }

            controllerComboBox.DataSource = algoControllers;
            controllerComboBox.Format += (s, ev) => { ev.Value = ((INameable)ev.Value).Name; };
            removeControllerButton.Enabled = algoControllers.Count > 0;
        }

        private void addControllerButton_Click(object sender, EventArgs e)
        {
            var controller = selectedAlgoControllerFactory.Create();
            algoControllers.Add(controller);
            controllerComboBox.SelectedItem = controller;
            
        }

        private void removeControllerButton_Click(object sender, EventArgs e)
        {
            algoControllers.Remove(selectedAlgoController);
        }

        private void algorithmProblemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoView = selectedAlgoController.AlgorithmView;

            baseSplitContainer.Panel1.Fill(algoView.VisualizationContainer);
            configurationSplitContainer.Panel2.Fill(algoView);
        }

        private void openControllerButton_Click(object sender, EventArgs e)
        {
            var result = openControllerDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var fileName = openControllerDialog.FileName;
                try
                {
                    var controller = AlgorithmControllerConverter.LoadFromFile(fileName);

                    controller.Name = Path.GetFileNameWithoutExtension(fileName);
                    algoControllers.Add(controller);
                    controllerComboBox.SelectedItem = controller;
                }
                catch (Exception err)
                {
                    FormsUtil.ShowErrorMessage(err.ToString());
                }
            }
        }

        private void saveControllerButton_Click(object sender, EventArgs e)
        {
            var result = saveControllerDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string fileName = saveControllerDialog.FileName;
                try
                {
                    AlgorithmControllerConverter.SaveToFile(selectedAlgoController, fileName);
                    selectedAlgoController.Name = Path.GetFileNameWithoutExtension(fileName);
                }
                catch (Exception err)
                {
                    FormsUtil.ShowErrorMessage(err.ToString());
                }
            }
        }

        private void AlgorithmForm_Load(object sender, EventArgs e)
        {
            var controllersFromDisk = AlgorithmControllerConverter.LoadDefaultList();

            if (controllersFromDisk != null)
                SetControllers(controllersFromDisk);
        }

        private void AlgorithmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AlgorithmControllerConverter.SaveAsDefaultList(algoControllers);
        }

        private void RedrawProblemComboBox()
        {
            controllerComboBox.SelectedIndexChanged -= algorithmProblemComboBox_SelectedIndexChanged;

            controllerComboBox.DataSource = null;
            controllerComboBox.DataSource = algoControllers;

            controllerComboBox.SelectedIndexChanged += algorithmProblemComboBox_SelectedIndexChanged;
        }

    }
}
