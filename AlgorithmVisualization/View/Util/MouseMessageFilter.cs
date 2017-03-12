using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    class MouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public event MouseEventHandler MouseMoved;

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                Point mousePosition = Control.MousePosition;

                MouseMoved ?.Invoke(null, new MouseEventArgs(MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));;
            }

            // Always allow message to continue to the next filter control
            return false;
        }
    }
}


