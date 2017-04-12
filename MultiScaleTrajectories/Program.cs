using System;
using System.Windows.Forms;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.Properties;
using MultiScaleTrajectories.MultiScale.Controller;

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
            algoForm.AlgoControllerTypes.Add(typeof(MSController));
            Application.Run(algoForm);
        }
    }
}
