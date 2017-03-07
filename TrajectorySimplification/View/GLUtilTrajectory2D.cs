using System.Drawing;
using AlgorithmVisualization.View.Exploration.Visualization.GLUtil;
using OpenTK.Graphics.OpenGL;
using TrajectorySimplification.Algorithm.Geometry;

namespace TrajectorySimplification.View
{
    static class GLUtilTrajectory2D
    {

        public static void DrawPoint(Point2D point, float radius, Color color)
        {
            GL.PushMatrix();
            GL.Color3(color);

            GL.Translate(point.X, point.Y, 1f);
            GLUtil2D.DrawCircle(radius);
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
