using System.Collections.Generic;
using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util.Components
{
    public class SplittableView : UserControl
    {
        protected readonly List<Control> Views;

        public SplittableView()
        {
            Views = new List<Control>();

            Clear();
        }

        public void Clear()
        {
            Controls.Clear();

            foreach (var chooser in Views)
            {
                chooser.Dispose();
            }

            Views.Clear();
        }

        protected void Split(Control view, Control newView, Orientation orientation)
        {
            var splitContainer = Split(view.Parent, orientation);
            splitContainer.Panel1.Fill(view);
            splitContainer.Panel2.Fill(newView);
        }

        public SplitContainer Split(Control container, Orientation orientation)
        {
            var splitContainer = new SplitContainer
            {
                Orientation = orientation,
                Panel1MinSize = 0,
                Panel2MinSize = 0
            };

            container.Fill(splitContainer);

            switch (orientation)
            {
                case Orientation.Horizontal:
                    splitContainer.SplitterDistance = splitContainer.Height / 2;
                    break;
                case Orientation.Vertical:
                    splitContainer.SplitterDistance = splitContainer.Width / 2;
                    break;
            }

            return splitContainer;
        }

        public void Unsplit(Control view)
        {
            if (view.Parent is SplitterPanel)
            {
                var splitterPanel = view.Parent;
                var splitContainer = (SplitContainer) splitterPanel.Parent;
                splitContainer.Parent.Fill(view);
                DisposeSplitterPanel(splitContainer.Panel1 == splitterPanel ? splitContainer.Panel2 : splitContainer.Panel1);
            }
        }

        private void DisposeSplitterPanel(SplitterPanel panel)
        {
            var child = panel.Controls[0];

            if (child is SplitContainer)
            {
                var container = child as SplitContainer;
                DisposeSplitterPanel(container.Panel1);
                DisposeSplitterPanel(container.Panel2);
                container.Dispose();
            }
            else 
            {
                child.Dispose();
                Views.Remove(child);
            }
        }

    }
}
