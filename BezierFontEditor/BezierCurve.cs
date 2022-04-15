using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierFontEditor
{
    public class BezierCurve
    {
        public PointF Start { get; set; }

        public PointF Middle { get; set; }

        public PointF End { get; set; }

        public IList<PointF> Points => new List<PointF>(new PointF[] { Start, Middle, End });

        public BezierCurve()
        {
        }

        public BezierCurve(PointF start, PointF middle, PointF end)
        {
            Start = start;
            Middle = middle;
            End = end;
        }

        private PointF TransformPoint(PointF point, Matrix<float> transformMatrix)
        {
            Vector<float> v = Vector<float>.Build.DenseOfArray(new float[] { point.X, point.Y, 1 });
            Vector<float> v2 = transformMatrix.LeftMultiply(v);
            return new PointF(v2[0], v2[1]);
        }
        public void Draw(Graphics gr, Pen pen)
        {
            Draw(gr, Matrix<float>.Build.Diagonal(3, 3, 1), pen);
        }
        public void Draw(Graphics gr, Matrix<float> transformMatrix, Pen pen)
        {
            PointF tStart = TransformPoint(Start, transformMatrix);
            PointF tMiddle = TransformPoint(Middle, transformMatrix);
            PointF tEnd = TransformPoint(End, transformMatrix);
            float x1 = tStart.X, y1 = tStart.Y;
            float x2 = tMiddle.X, y2 = tMiddle.Y;
            float x3 = tEnd.X, y3 = tEnd.Y;
            int steps = (int)Math.Ceiling( Math.Max(Math.Abs(tEnd.X - tStart.X), Math.Abs(tEnd.Y - tStart.Y)) / 2f );
            PointF prevPoint = tStart;
            for (int i = 0; i <= steps; i++)
            {
                float t = (float)i / steps;
                if (!(0 <= t && t <= 1)) break;
                float x = (1 - t) * (1 - t) * x1 + 2 * t * (1 - t) * x2 + t * t * x3;
                float y = (1 - t) * (1 - t) * y1 + 2 * t * (1 - t) * y2 + t * t * y3;
                var p = new Point((int)x, (int)y);
                gr.DrawLine(pen, prevPoint, p);
                prevPoint = p;
            }
        }

        public override string ToString()
        {
            return $"#{Start.X},{Start.Y};{Middle.X},{Middle.Y};{End.X},{End.Y}";
        }
    }
}
