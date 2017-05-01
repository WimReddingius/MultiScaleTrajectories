using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using AlgorithmVisualization.View;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource;
using MultiScaleTrajectories.Properties;
using MultiScaleTrajectories.Simplification.MultiScale;
using MultiScaleTrajectories.Simplification.ShortcutFinding.ArbitraryScale;
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
                () => new ASShortcutFindingController(),
                () => new SSShortcutFindingController(),
                () => new MSShortcutFindingController()
                //() => new SSSPController()
            };

            var algoForm = new AlgorithmForm(controllers) { Text = Resources.Program_Name };

            Application.Run(algoForm);
        }
    }
}
