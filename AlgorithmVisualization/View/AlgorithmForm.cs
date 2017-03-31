using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm : Form
    {
        public readonly BindingList<Type> AlgoControllerTypes;
        private readonly BindingList<IAlgorithmController> algoControllers;


        public AlgorithmForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();
            AlgorithmControllerConverter.Init();

            AlgoControllerTypes = new BindingList<Type>();
            algoControllers = new BindingList<IAlgorithmController>();

            AlgoControllerTypes.ListChanged += (o, e) =>
            {
                switch(e.ListChangedType) {
                    case ListChangedType.ItemAdded:
                        algoControllers.Add(AlgorithmControllerConverter.GetController(AlgoControllerTypes[e.NewIndex]));
                        break;
                    case ListChangedType.ItemDeleted:
                        algoControllers.Remove(AlgorithmControllerConverter.GetController(AlgoControllerTypes[e.NewIndex]));
                        break;
                }

                algorithmProblemComboBox.DataSource = algoControllers;
                algorithmProblemComboBox.DisplayMember = "Name";
            };
        }

        private void algorithmProblemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoView = ((IAlgorithmController) algorithmProblemComboBox.SelectedItem).AlgorithmView;

            baseSplitContainer.Panel1.Fill(algoView.VisualizationContainer);
            configurationSplitContainer.Panel2.Fill(algoView);
        }

        private void AlgorithmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AlgorithmControllerConverter.Save();
        }

    }
}
