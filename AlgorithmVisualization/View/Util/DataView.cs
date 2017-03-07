using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    #if DEBUG

        //For showing in Visual Studio Designer
        public class DataView<T> : UserControl, IDataLoader<T>
        {
            public virtual void LoadData(T data)
            {

            }
        }

    #else

        abstract class DataView<T> : UserControl, IDataLoader<T> 
        {
            public abstract void LoadData(T data);
        }

    #endif
}
