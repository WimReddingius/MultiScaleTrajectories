using System.Windows.Forms;
using MultiScaleTrajectories.Controller.Util;

namespace MultiScaleTrajectories.Controller
{
   abstract class DataViewController<T> : CompoundDataLoader<T>
    {

        public abstract Control OptionsControl { get; }

        public abstract Control ViewControl { get; }

    }
}
