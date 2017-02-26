using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
   interface IViewTypeController
    {

        Control GetOptionsControl();

        Control GetViewControl();

        string ToString();

    }
}
