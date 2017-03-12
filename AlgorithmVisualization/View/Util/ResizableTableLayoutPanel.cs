using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AlgorithmVisualization.View.Util
{
    class ResizableTableLayoutPanel : TableLayoutPanel
    {
        public int SplitterSize { get; set; }

        int sizingRow = -1;
        int currentRow = -1;
        Point mdown = Point.Empty;
        int oldHeight = -1;
        bool isNormalRow;
        List<RectangleF> tlpRows = new List<RectangleF>();
        int[] rowHeights = new int[0];

        int sizingCol = -1;
        int currentCol = -1;
        int oldWidth = -1;
        bool isNormalCol;
        List<RectangleF> tlpCols = new List<RectangleF>();
        int[] colWidths = new int[0];

        public ResizableTableLayoutPanel()
        {
            MouseDown += SplitTablePanel_MouseDown;
            MouseMove += SplitTablePanel_MouseMove;
            MouseUp += SplitTablePanel_MouseUp;
            MouseLeave += SplitTablePanel_MouseLeave;
            Resize += SplitTablePanel_Resize;

            SplitterSize = 6;
        }

        void SplitTablePanel_Resize(object sender, EventArgs e)
        {
            getRowRectangles(SplitterSize);
            getColRectangles(SplitterSize);
        }
        void SplitTablePanel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        
        void SplitTablePanel_MouseUp(object sender, MouseEventArgs e)
        {
            getRowRectangles(SplitterSize);
            getColRectangles(SplitterSize);
        }

        void SplitTablePanel_MouseMove(object sender, MouseEventArgs e)
        {
            bool r = rowMove(sender, e);
            bool c = colMove(sender, e);

            if (r && !c)
                Cursor = Cursors.SizeNS;
            else if (!r && c)
                Cursor = Cursors.SizeWE;
            else if (r && c)
                Cursor = Cursors.SizeAll;
            else
                Cursor = Cursors.Default;
        }

        bool rowMove(object sender, MouseEventArgs e)
        {
            bool isMove = false;
            if (!isNormalRow) nomalizeRowStyles();
            if (tlpRows.Count <= 0) getRowRectangles(SplitterSize);
            if (rowHeights.Length <= 0) rowHeights = GetRowHeights();

            if (e.Button == MouseButtons.Left)
            {
                if (sizingRow < 0) return false;
                int newHeight = oldHeight + e.Y - mdown.Y;
                sizeRow(sizingRow, newHeight);
                isMove = true;
            }
            else
            {
                currentRow = -1;
                for (int i = 0; i < tlpRows.Count; i++)
                    if (tlpRows[i].Contains(e.Location))
                    {
                        currentRow = i;
                        isMove = true;
                        break;
                    }
            }
            return isMove;
        }

        bool colMove(object sender, MouseEventArgs e)
        {
            bool isMove = false;
            if (!isNormalCol) nomalizeColStyles();
            if (tlpCols.Count <= 0) getColRectangles(SplitterSize);
            if (colWidths.Length <= 0) colWidths = GetColumnWidths();

            if (e.Button == MouseButtons.Left)
            {
                if (sizingCol < 0) return false;
                int newWidth = oldWidth + e.X - mdown.X;
                sizeCol(sizingCol, newWidth);
                isMove = true;
            }
            else
            {
                currentCol = -1;
                for (int i = 0; i < tlpCols.Count; i++)
                    if (tlpCols[i].Contains(e.Location))
                    {
                        currentCol = i;
                        isMove = true;
                        break;
                    }
            }
            return isMove;
        }

        void SplitTablePanel_MouseDown(object sender, MouseEventArgs e)
        {
            mdown = Point.Empty;
            rowDown();
            colDown();
            mdown = e.Location;
        }

        void rowDown()
        {
            sizingRow = -1;
            if (currentRow < 0) return;
            sizingRow = currentRow;
            oldHeight = rowHeights[sizingRow];
        }

        void colDown()
        {
            sizingCol = -1;
            if (currentCol < 0) return;
            sizingCol = currentCol;
            oldWidth = colWidths[sizingCol];
        }


        void getRowRectangles(float size)
        {   // get a list of mouse sensitive rectangles
            float sz = size / 2f;
            float y = 0f;
            int w = ClientSize.Width;
            int[] rw = GetRowHeights();

            tlpRows.Clear();
            for (int i = 0; i < rw.Length - 1; i++)
            {
                y += rw[i];
                tlpRows.Add(new RectangleF(0, y - sz, w, size));
            }

        }

        void getColRectangles(float size)
        {   // get a list of mouse sensitive rectangles
            float sz = size / 2f;
            float x = 0f;
            int h = ClientSize.Height;
            int[] rw = GetColumnWidths();

            tlpCols.Clear();
            for (int i = 0; i < rw.Length - 1; i++)
            {
                x += rw[i];
                tlpCols.Add(new RectangleF(x - sz, 0, size, h));
            }

        }

        void sizeRow(int row, int newHeight)
        {   // change the height of one row
            if (newHeight == 0) return;
            if (sizingRow < 0) return;
            SuspendLayout();
            rowHeights = GetRowHeights();
            if (sizingRow >= rowHeights.Length) return;

            if (newHeight > 0)
                RowStyles[sizingRow] = new RowStyle(SizeType.Absolute, newHeight);
            ResumeLayout();
            rowHeights = GetRowHeights();
            getRowRectangles(SplitterSize);
        }

        void sizeCol(int col, int newWidth)
        {   // change the height of one row
            if (newWidth == 0) return;
            if (sizingCol < 0) return;
            SuspendLayout();
            colWidths = GetColumnWidths();
            if (sizingCol >= colWidths.Length) return;

            if (newWidth > 0)
                ColumnStyles[sizingCol] = new ColumnStyle(SizeType.Absolute, newWidth);
            ResumeLayout();
            colWidths = GetColumnWidths();
            getColRectangles(SplitterSize);
        }

        void nomalizeRowStyles()
        {   // set all rows to absolute and the last one to percent=100!
            if (rowHeights.Length <= 0) return;
            rowHeights = GetRowHeights();
            RowStyles.Clear();
            for (int i = 0; i < RowCount - 1; i++)
            {
                RowStyle cs = new RowStyle(SizeType.Absolute, rowHeights[i]);
                RowStyles.Add(cs);
            }
            RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            isNormalRow = true;
        }

        void nomalizeColStyles()
        {   // set all rows to absolute and the last one to percent=100!
            if (colWidths.Length <= 0) return;
            colWidths = GetColumnWidths();
            ColumnStyles.Clear();
            for (int i = 0; i < ColumnCount - 1; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Absolute, colWidths[i]);
                ColumnStyles.Add(cs);
            }
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            isNormalCol = true;
        }
    }
}
