using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgoTypeView
    {

        Control ConfigurationControl { get; }

        Control ViewControl { get; }

        string ToString();

    }
}
