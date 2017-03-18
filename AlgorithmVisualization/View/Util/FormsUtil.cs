using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    static class FormsUtil
    {
        public static void Fill(this Control container, Control control, bool clear = true)
        {
            if (control != null)
            {
                if (clear)
                    container.Controls.Clear();

                control.CreateControl();
                control.Dock = DockStyle.Fill;
                control.Location = new System.Drawing.Point(0, 0);

                container.Controls.Add(control);                
            }
        }

        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

    }
}
