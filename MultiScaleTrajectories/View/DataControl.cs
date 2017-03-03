using System.Windows.Forms;
using MultiScaleTrajectories.Controller;

namespace MultiScaleTrajectories.View
{
    #if DEBUG

        //For showing in Visual Studio Designer
        class DataControl<T> : UserControl, IDataLoader<T>
        {
            public virtual void LoadData(T data)
            {

            }
        }

    #else

        abstract class DataControl<T> : UserControl, IDataLoader<T> 
        {
            public abstract void LoadData(T data);
        }

    #endif
}
