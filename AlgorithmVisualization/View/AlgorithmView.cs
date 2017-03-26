using System.Windows.Forms;
using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View
{
#if DEBUG

        //For showing in Visual Studio Designer
        public class AlgorithmView : DoubleBufferedUserControl
    {
            public virtual Control VisualizationContainer { get; set; }

            public virtual void Reset() { }
        }

#else

        abstract class AlgorithmViewBase : DoubleBufferedUserControl
        {
            public abstract Control VisualizationContainer { get; set; }

            public abstract void Reset();
        }

#endif
}
