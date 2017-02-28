using System;
using System.Drawing;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK.Graphics.OpenGL;

namespace MultiScaleTrajectories.View.Visualization.GL
{
    static class Util2D
    {

        public static void DrawCircle(float radius)
        {
            OpenTK.Graphics.OpenGL.GL.Begin(PrimitiveType.TriangleFan);

            for (int i = 0; i < 360; i++)
            {
                double degInRad = i * Math.PI / 180;
                OpenTK.Graphics.OpenGL.GL.Vertex2(Math.Cos(degInRad) * radius, Math.Sin(degInRad) * radius);
            }
            OpenTK.Graphics.OpenGL.GL.End();
        }

        public static void RenderTrajectory(Trajectory2D trajectory)
        {
            
        }

        public static void DrawPoint(Point2D point, float radius, Color color)
        {
            OpenTK.Graphics.OpenGL.GL.PushMatrix();
            OpenTK.Graphics.OpenGL.GL.Color3(color);

            OpenTK.Graphics.OpenGL.GL.Translate(point.X, point.Y, 1f);
            DrawCircle(radius);
            OpenTK.Graphics.OpenGL.GL.PopMatrix();
        }

        public static void DrawEdges(Trajectory2D trajectory, float lineWidth, Color color)
        {
            OpenTK.Graphics.OpenGL.GL.LineWidth(lineWidth);
            OpenTK.Graphics.OpenGL.GL.Color3(color);
            OpenTK.Graphics.OpenGL.GL.Begin(PrimitiveType.LineStrip);
            foreach (Point2D p in trajectory)
            {
                OpenTK.Graphics.OpenGL.GL.Vertex3(p.X, p.Y, -1f);
            }
            OpenTK.Graphics.OpenGL.GL.End();
        }

        public static void RenderText(int x, int y, string str, Color color)
        {
            OpenTK.Graphics.OpenGL.GL.Enable(EnableCap.Texture2D);
            OpenTK.Graphics.OpenGL.GL.Enable(EnableCap.Blend);
            OpenTK.Graphics.OpenGL.GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            OpenTK.Graphics.OpenGL.GL.Color3(color);
            GL.TextRenderer2D.DrawText(x, y, str);
            OpenTK.Graphics.OpenGL.GL.Disable(EnableCap.Blend);
            OpenTK.Graphics.OpenGL.GL.Disable(EnableCap.Texture2D);
        }


        public static void DrawPoints(Trajectory2D trajectory, float radius, Color color)
        {
            for (int i = 0; i < trajectory.Count; i++)
            {
                Point2D p = trajectory[i];
                DrawPoint(p, radius, color);
            }
        }

    }
}
