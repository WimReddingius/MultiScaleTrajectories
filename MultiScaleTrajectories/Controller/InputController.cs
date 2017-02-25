using System.ComponentModel;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface InputController
    {

        Control GetOptionsControl();

        void ClearInput();

        string SerializeInput();

        void LoadSerializedInput(string inputString);

    }
}
