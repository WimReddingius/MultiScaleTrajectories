using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace MultiScaleTrajectories.View.Util
{
    static class GLTextRenderer
    {
        public static string FontBitmapFilename = "test.png";
        public static bool BitmapFont = false;
        public static string FontName = "Consolas";
        public static int FontSize = 14;

        public static int GlyphsPerLine = 16;
        public static int GlyphLineCount = 16;
        public static int GlyphWidth = 11;
        public static int GlyphHeight = 22;
        public static int CharXSpacing = 11;

        // Used to offset rendering glyphs to bitmap
        public static int AtlasOffsetX = -3;
        public static int AtlassOffsetY = -1;
        
        private static int TextureWidth;
        private static int TextureHeight;
        private static int FontTextureID;


        public static void GenerateFontImage()
        {
            int bitmapWidth = GlyphsPerLine * GlyphWidth;
            int bitmapHeight = GlyphLineCount * GlyphHeight;

            using (Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                Font font;
                font = new Font(new FontFamily(FontName), FontSize);

                using (var g = Graphics.FromImage(bitmap))
                {
                    if (BitmapFont)
                    {
                        g.SmoothingMode = SmoothingMode.None;
                        g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    }
                    else {
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                        //g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }

                    for (int p = 0; p < GlyphLineCount; p++)
                    {
                        for (int n = 0; n < GlyphsPerLine; n++)
                        {
                            char c = (char)(n + p * GlyphsPerLine);
                            g.DrawString(c.ToString(), font, Brushes.White,
                                n * GlyphWidth + AtlasOffsetX, p * GlyphHeight + AtlassOffsetY);
                        }
                    }
                }
                bitmap.Save(FontBitmapFilename);
            }
        }

        public static void LoadTexture()
        {
            using (var bitmap = new Bitmap(FontBitmapFilename))
            {
                var texId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, FontTextureID);
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
                TextureWidth = bitmap.Width; TextureHeight = bitmap.Height;
                FontTextureID = texId;
            }
        }

        public static void DrawText(int x, int y, string text)
        {
            GL.Begin(PrimitiveType.Quads);

            float u_step = (float)GlyphWidth / (float)TextureWidth;
            float v_step = (float)GlyphHeight / (float)TextureHeight;

            for (int n = 0; n < text.Length; n++)
            {
                char idx = text[n];
                float u = (float)(idx % GlyphsPerLine) * u_step;
                float v = (float)(idx / GlyphsPerLine) * v_step;

                GL.TexCoord2(u, v);
                GL.Vertex2(x, y);
                GL.TexCoord2(u + u_step, v);
                GL.Vertex2(x + GlyphWidth, y);
                GL.TexCoord2(u + u_step, v + v_step);
                GL.Vertex2(x + GlyphWidth, y + GlyphHeight);
                GL.TexCoord2(u, v + v_step);
                GL.Vertex2(x, y + GlyphHeight);

                x += CharXSpacing;
            }

            GL.End();
        }

    }
}
