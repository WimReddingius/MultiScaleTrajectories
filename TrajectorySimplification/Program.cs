using System;
using System.Windows.Forms;
using AlgorithmVisualization.View;
using TrajectorySimplification.Properties;
using TrajectorySimplification.Single.Controller;

namespace TrajectorySimplification
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

            var algoForm = new AlgorithmForm()
            {
                Text = Resources.Program_Name
            };
            algoForm.AddControllers(new STController());

            Application.Run(algoForm);

        }
    }
}
