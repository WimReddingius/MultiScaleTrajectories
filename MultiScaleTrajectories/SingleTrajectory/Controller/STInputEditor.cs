using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.Util;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    sealed class STInputEditor : InputEditor<STInput>
    {
        public STInputEditor()
        {
            CanImport = true;
            Visualization = new STInputNodeLink();
            Options = new STInputOptions();
        }

        public override STInput Import(string fileName)
        {
            return new STInput(MoveBank.ReadSingleTrajectory(fileName));
        }
                
    }
}
