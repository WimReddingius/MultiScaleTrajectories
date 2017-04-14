using System;
using System.Windows.Forms;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding;
using MultiScaleTrajectories.MultiScale;
using MultiScaleTrajectories.PathFinding.SingleSource;
using MultiScaleTrajectories.Properties;

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
            algoForm.AlgoControllerTypes.Add(typeof(ShortcutFindingController));
            algoForm.AlgoControllerTypes.Add(typeof(EpsilonFindingController));
            algoForm.AlgoControllerTypes.Add(typeof(SingleSourceShortestPathController));
            Application.Run(algoForm);
        }
    }
}
