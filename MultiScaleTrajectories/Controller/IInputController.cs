using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IInputController
    {
        Control OptionsControl { get; }

        Control ViewControl { get; }

        void LoadFreshInput();

        string SerializeInput();

        void LoadSerializedInput(string inputString);
    }
}
