using System;
using System.Drawing;
using AlgorithmVisualization.View.Visualization.GLUtil;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK.Graphics.OpenGL;

namespace MultiScaleTrajectories.View
{
    static class GLUtilTrajectory2D
    {

        public static void DrawPoint(Point2D point, double radius, int numSegments, Color color, int? name = null)
        {
            GL.PushMatrix();
            GL.Color3(color);

            GL.Translate(point.X, point.Y, 1f);

            if (name != null)
                GL.LoadName((int)name);

            GLUtil2D.DrawCircle(radius, numSegments);

            GL.PopMatrix();
        }

        public static void DrawEdges(Trajectory2D trajectory, float lineWidth, Color color)
        {
            GL.LineWidth(lineWidth);
            GL.Color3(color);
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Point2D p in trajectory)
            {
                GL.Vertex3(p.X, p.Y, -1f);
            }
            GL.End();
        }

        public static void DrawPoints(Trajectory2D trajectory, double radius, int numSegments, Func<Point2D, Color> colorFunc, Func<Point2D, int> nameFunc = null)
        {
            foreach (Point2D p in trajectory)
            {
                DrawPoint(p, radius, numSegments, colorFunc(p), nameFunc(p));
            }
        }

    }
}
