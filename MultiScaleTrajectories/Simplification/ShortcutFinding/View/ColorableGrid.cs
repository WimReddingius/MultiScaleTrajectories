using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.View
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
            if (grid == null || Width == 0 || Height == 0)
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

        public static List<Color> CreateColorGradient(int size, Color min, Color max)
        {
            int rMax = max.R;
            int rMin = min.R;
            int gMax = max.G;
            int gMin = min.G;
            int bMax = max.B;
            int bMin = min.B;
            var colorList = new List<Color>();
            for (int i = 0; i < size; i++)
            {
                var rAverage = rMin + (rMax - rMin) * i / size;
                var gAverage = gMin + (gMax - gMin) * i / size;
                var bAverage = bMin + (bMax - bMin) * i / size;
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
            return colorList;
        }

        public static List<Color> CreateColorGradient(int size, Color[] colors)
        {
            var numGradients = colors.Length - 1;
            var remainder = size % numGradients;
            var gradient = new List<Color>();

            var standardSize = size / numGradients;
            for (var i = 0; i < numGradients; i++)
            {
                var gradientSize = i == numGradients - 1 ? standardSize + remainder : standardSize;
                gradient.AddRange(CreateColorGradient(gradientSize, colors[i], colors[i + 1]));
            }

            return gradient;;
        }

    }
}