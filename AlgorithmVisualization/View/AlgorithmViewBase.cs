using System.Windows.Forms;

namespace AlgorithmVisualization.View
{
    #if DEBUG

        //For showing in Visual Studio Designer
        public class AlgorithmViewBase : UserControl
        {
            public virtual void Initialize(Control visualizationContainer)
            {

            }
        }

#else

        abstract class AlgorithmViewBase : UserControl
        {
            public abstract void Initialize(Control visualizationContainer);
        }

#endif
}
