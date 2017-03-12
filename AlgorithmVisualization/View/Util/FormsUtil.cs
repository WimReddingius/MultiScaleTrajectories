﻿using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    class FormsUtil
    {
        public static void FillContainer(Control container, Control control, bool clear = true)
        {
            if (control != null)
            {
                container.Controls.Clear();

                control.CreateControl();
                control.Dock = DockStyle.Fill;
                control.Location = new System.Drawing.Point(0, 0);

                container.Controls.Add(control);                
            }
        }

    }
}
