using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
   interface IOutputController
    {
        Control OptionsControl { get; }

        Control ViewControl { get; }

        string ToString();
    }
}
