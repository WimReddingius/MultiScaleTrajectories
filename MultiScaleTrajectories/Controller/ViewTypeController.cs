using System.ComponentModel;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
   interface ViewTypeController
    {

        Control GetOptionsControl();

        Control GetViewControl();

        string ToString();

    }
}
