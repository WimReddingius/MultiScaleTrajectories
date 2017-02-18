using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.View.Util
{
    static class GLU
    {

        public static void drawCircle(float radius)
        {
            GL.Begin(PrimitiveType.TriangleFan);

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }
            GL.End();
        }


    }
}
