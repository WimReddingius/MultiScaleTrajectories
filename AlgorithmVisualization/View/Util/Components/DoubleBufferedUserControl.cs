using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util.Components
{
    public class DoubleBufferedUserControl : UserControl
    {
        //double buffering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
    }
}
