using System;
using System.Windows.Forms;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.Properties;
using MultiScaleTrajectories.SingleTrajectory.Controller;

namespace MultiScaleTrajectories
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var algoForm = new AlgorithmForm { Text = Resources.Program_Name };
            algoForm.AlgoControllers.Add(new STController());

            Application.Run(algoForm);

        }
    }
}
