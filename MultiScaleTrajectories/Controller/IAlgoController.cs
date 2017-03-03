using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgoController
    {

        Control ConfigControl { get; }

        string Name { get;  }

    }
}
