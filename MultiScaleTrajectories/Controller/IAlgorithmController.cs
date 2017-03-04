using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgorithmController
    {

        Control Control { get; }

        string Name { get;  }

    }
}
