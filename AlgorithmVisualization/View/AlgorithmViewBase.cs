using System.Windows.Forms;

namespace AlgorithmVisualization.View
{
    #if DEBUG

        //For showing in Visual Studio Designer
        public class AlgorithmViewBase : UserControl
        {
            public virtual Control VisualizationContainer { get; set; }

            public virtual void Reset() { }
        }

#else

        abstract class AlgorithmViewBase : UserControl
        {
            public abstract Control VisualizationContainer { get; set; }

            public abstract void Reset();
        }

#endif
}
