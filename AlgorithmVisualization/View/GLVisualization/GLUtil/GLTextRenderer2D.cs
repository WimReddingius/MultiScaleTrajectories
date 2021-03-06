﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace AlgorithmVisualization.View.GLVisualization.GLUtil
{
    static class GLTextRenderer2D
    {
        public static Bitmap FontBitmap;
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

            FontBitmap = new Bitmap(bitmapWidth, bitmapHeight, PixelFormat.Format32bppArgb);
            
            var font = new Font(new FontFamily(FontName), FontSize);

            using (var g = Graphics.FromImage(FontBitmap))
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
        }

        public static void LoadTexture()
        {
            using (var bitmap = new Bitmap(FontBitmap))
            {
                var texId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, FontTextureID);
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
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

            float u_step = GlyphWidth / (float)TextureWidth;
            float v_step = GlyphHeight / (float)TextureHeight;

            foreach (char idx in text)
            {
                float u = idx % GlyphsPerLine * u_step;
                float v = idx / GlyphsPerLine * v_step;

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
