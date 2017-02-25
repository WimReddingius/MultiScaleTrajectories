using OpenTK.Graphics.OpenGL;
using System;

namespace MultiScaleTrajectories.View.Type.Visualization.GL
{
    static class Util
    {

        public static void drawCircle(float radius)
        {
            OpenTK.Graphics.OpenGL.GL.Begin(PrimitiveType.TriangleFan);

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                OpenTK.Graphics.OpenGL.GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }
            OpenTK.Graphics.OpenGL.GL.End();
        }


    }
}
