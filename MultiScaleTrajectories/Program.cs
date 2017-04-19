using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Controller;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Controller;
using MultiScaleTrajectories.MultiScale.Controller;
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

            var controllers = new List<Func<AlgorithmControllerBase>>
            {
                () => new MSController(),
                () => new ShortcutFindingController(),
                () => new EpsilonFindingController(),
                //() => new SSSPController()
            };

            var algoForm = new AlgorithmForm(controllers) { Text = Resources.Program_Name };

            Application.Run(algoForm);
        }
    }
}
