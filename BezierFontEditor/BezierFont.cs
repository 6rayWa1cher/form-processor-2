using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BezierFontEditor
{
    public class BezierLetter
    {
        public IList<BezierCurve> Curves { get; set; } = new List<BezierCurve>();

        public int Baseline { get; set; } = 0;

        public void Draw(Graphics gr, Pen pen)
        {
            Draw(gr, Matrix<float>.Build.Diagonal(3, 3, 1), pen);
        }
        public void Draw(Graphics gr, Matrix<float> transformMatrix, Pen pen)
        {
            foreach (BezierCurve curve in Curves)
            {
                curve.Draw(gr, transformMatrix, pen);
            }
        }
    }
    public class BezierFont
    {
        public IDictionary<string, BezierLetter> Letters { get; set; } = new Dictionary<string, BezierLetter>();

        public int FontVersion { get; set; } = 1;

        public Size Borders { get; set; }
    }
}
