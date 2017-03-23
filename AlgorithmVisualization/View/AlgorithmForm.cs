using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm : Form
    {
        public readonly BindingList<Type> ProblemControllerTypes;
        private readonly BindingList<IAlgorithmController> problemControllers;


        public AlgorithmForm()
        {
            InitializeComponent();

            OpenTK.Toolkit.Init();
            AlgorithmControllerConverter.Init();

            ProblemControllerTypes = new BindingList<Type>();
            problemControllers = new BindingList<IAlgorithmController>();

            ProblemControllerTypes.ListChanged += (o, e) =>
            {
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    problemControllers.Add(AlgorithmControllerConverter.GetController(ProblemControllerTypes[e.NewIndex]));
                }
                if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    problemControllers.Remove(AlgorithmControllerConverter.GetController(ProblemControllerTypes[e.NewIndex]));
                }

                algorithmProblemComboBox.DataSource = problemControllers;
                algorithmProblemComboBox.DisplayMember = "Name";
            };
        }

        private void algorithmProblemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var algoView = ((IAlgorithmController) algorithmProblemComboBox.SelectedItem).AlgorithmView;

            baseSplitContainer.Panel1.Fill(algoView.VisualizationContainer);
            configurationSplitContainer.Panel2.Fill(algoView);
            algoView.Reset();
        }

        private void AlgorithmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            AlgorithmControllerConverter.Save();
        }
    }
}
