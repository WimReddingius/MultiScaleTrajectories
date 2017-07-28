using AlgorithmVisualization.Algorithm;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm
{
    abstract class SPFAlgorithm : Algorithm<SPFInput, SPFOutput> 
    {

        protected SPFAlgorithm(string name, Control options = null) : base(name)
        {
            OptionsControl = options;
        }
    }
}
