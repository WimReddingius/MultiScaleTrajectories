using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IInputController
    {

        Control GetOptionsControl();

        void ClearInput();

        string SerializeInput();

        void LoadSerializedInput(string inputString);

    }
}
