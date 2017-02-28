using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgoView
    {

        Control ConfigurationControl { get; }

        Control ViewControl { get; }

        string ToString();

    }
}
