﻿using System.Windows.Forms;

namespace MultiScaleTrajectories.View
{
    class FormsUtil
    {
        public static void FillContainer(Control container, Control control)
        {
            if (control != null)
            {
                container.Controls.Clear();

                control.CreateControl();
                container.Controls.Add(control);

                control.Dock = DockStyle.Fill;
                control.Location = new System.Drawing.Point(0, 0);
            }
        }

    }
}
