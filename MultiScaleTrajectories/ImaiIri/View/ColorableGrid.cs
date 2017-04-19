using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MultiScaleTrajectories.ImaiIri.View
{
    public partial class ColorableGrid : UserControl
    {
        private Color[,] colors;
        private Bitmap grid;

        private int numRows;
        private int numColumns;

        public ColorableGrid(int numRows = 0, int numColumns = 0)
        {
            InitializeComponent();
            SetDimensions(numRows, numColumns);
            Resize += (o, e) => ScaleGridImage();
        }

        public void DrawGrid()
        {
            if (numRows == 0 || numColumns == 0)
                return;

            var bmp = new Bitmap(numColumns, numRows);
            for (var i = 0; i < numColumns; i++)
            {
                for (var j = 0; j < numRows; j++)
                {
                    bmp.SetPixel(j, i, colors[i, j]);
                }
            }

            grid = bmp;
            ScaleGridImage();
        }

        private void ScaleGridImage()
        {
            if (grid == null)
                return;

            var scaledImage = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(grid, 0, 0, Width, Height);
            }

            BackgroundImage = scaledImage;
        }

        public void SetDimensions(int numRows, int numColumns)
        {
            this.numRows = numRows;
            this.numColumns = numColumns;
            colors = new Color[numColumns, numRows];
        }

        public void ColorCell(int row, int column, Color color)
        {
            colors[column, row] = color;
        }

    }
}
