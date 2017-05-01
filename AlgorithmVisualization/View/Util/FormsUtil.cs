using System;
using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    public static class FormsUtil
    {
        public static void Fill(this Control container, Control control, bool clear = true)
        {
            if (clear)
                container.Controls.Clear();

            if (control != null)
            {
                control.CreateControl();
                control.Dock = DockStyle.Fill;
                control.Location = new System.Drawing.Point(0, 0);

                container.Controls.Add(control);                
            }
        }

        public static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static void InvokeIfRequired<T>(this Control control, Action<T> action, T par)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, par);
            }
            else
            {
                action(par);
            }
        }

        public static void ShowErrorMessage(string str)
        {
            MessageBox.Show(str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }




    }
}
