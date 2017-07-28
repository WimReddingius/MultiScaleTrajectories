using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.Properties;
using MultiScaleTrajectories.Simplification.MultiScale;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale;

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
                () => new SSShortcutFindingController(),
                () => new MSShortcutFindingController(),
                () => new SPFController()
            };

            var algoForm = new AlgorithmForm(controllers) { Text = Resources.Program_Name };

            Application.Run(algoForm);
        }
    }
}
