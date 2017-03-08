using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace AlgorithmVisualization.View.Exploration.Visualization.GLUtil
{
    public static class GLUtil2D
    {

        public static void DrawCircle(double radius, int numSegments)
        {
            GL.Begin(PrimitiveType.TriangleFan);

            for (int i = 0; i < numSegments; i++)
            {
                double degInRad = 2 * i * Math.PI / numSegments;
                GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }
            GL.End();
        }

        public static void RenderText(int x, int y, string str, Color color)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Color3(color);
            GLTextRenderer2D.DrawText(x, y, str);
            GL.Disable(EnableCap.Blend);
            GL.Disable(EnableCap.Texture2D);
        }

    }
}
