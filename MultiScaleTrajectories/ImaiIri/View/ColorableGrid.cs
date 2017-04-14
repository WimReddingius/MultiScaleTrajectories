using System;
using System.Drawing;
using System.Windows.Forms;
using AlgorithmVisualization.View.Util.Components;

namespace MultiScaleTrajectories.ImaiIri.View
{
    public partial class ColorableGrid : DoubleBufferedUserControl
    {
        private Color[,] colors;

        public ColorableGrid(int numRows = 1, int numColumns = 1)
        {
            InitializeComponent();
            SetDimensions(numRows, numColumns);

            Resize += (o, e) => RecalculateCellDimensions();
        }

        private void RecalculateCellDimensions()
        {
            var cellWidth = (double) Width / grid.ColumnCount;
            var cellWidthPixels = (int) Math.Floor(cellWidth);
            var widthRemainder = Width % grid.ColumnCount;
            for (var i = 0; i < grid.ColumnCount - 1; i++)
            {
                var columnStyle = grid.ColumnStyles[i];
                if (i + 1 <= widthRemainder)
                    columnStyle.Width = cellWidthPixels + 1;
                else
                    columnStyle.Width = cellWidthPixels;
            }

            var cellHeight = (double)Height / grid.RowCount;
            var cellHeightPixels = (int)Math.Floor(cellHeight);
            var heightRemainder = Height % grid.RowCount;
            for (var i = 0; i < grid.RowCount - 1; i++)
            {
                var rowStyle = grid.RowStyles[i];
                if (i + 1 <= heightRemainder)
                    rowStyle.Height = cellHeightPixels + 1;
                else
                    rowStyle.Height = cellHeightPixels;
            }
        }

        public void SetDimensions(int numRows, int numColumns)
        {
            grid.RowCount = numRows;
            grid.ColumnCount = numColumns;

            grid.RowStyles.Clear();
            for (var row = 0; row < grid.RowCount; row++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute));
            }

            grid.ColumnStyles.Clear();
            for (var row = 0; row < grid.RowCount; row++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute));
            }

            RecalculateCellDimensions();
            colors = new Color[grid.RowCount, grid.ColumnCount];
        }

        public void ColorCell(int row, int column, Color color)
        {
            colors[row, column] = color;
        }

        private void grid_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(colors[e.Row, e.Column]), e.CellBounds);

            var pen = new Pen(new SolidBrush(Color.Black));
            e.Graphics.DrawRectangle(pen, e.CellBounds);
        }
    }
}
