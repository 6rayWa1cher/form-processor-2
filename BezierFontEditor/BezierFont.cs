using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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

        public Rectangle Borders { get; set; }

        public void Draw(Graphics gr, Point pos, float height, Pen pen, string text)
        {
            int charIndex = 0;
            foreach (string s in FontUtils.SplitIntoTextElements(text))
            {
                if (Letters.ContainsKey(s))
                {
                    BezierLetter letter = Letters[s];
                    float nHeight = height * Borders.Height / (letter.Baseline - Borders.Y);
                    float letterWidth = Borders.Width * height / (letter.Baseline - Borders.Y);
                    int x = pos.X + (int)Math.Ceiling(charIndex * letterWidth);
                    Point charPos = new Point(x, pos.Y);
                    Matrix<float> matrix = GetTransformMatrix(letter, charPos, letterWidth, nHeight);
                    letter.Draw(gr, matrix, pen);
                } else
                {
                    Console.WriteLine($"Unknown symbol {s}");
                }
                charIndex++;
            }
        }
        private Matrix<float> GetTransformMatrix(BezierLetter letter, Point target, float targetWidth, float targetHeight)
        {
            int baseline = letter.Baseline;
            Matrix<float> shiftToZBPoint = DenseMatrix.OfArray(new float[,] {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { -Borders.X, -Borders.Y, 1 }
            });
            float scaleX = targetWidth / Borders.Width;
            float scaleY = targetHeight / Borders.Height;
            Matrix<float> scaleMatrix = DenseMatrix.OfArray(new float[,] {
                { targetWidth / Borders.Width, 0, 0 },
                { 0, targetHeight / Borders.Height, 0 },
                { 0, 0, 1 }
            });
            Matrix<float> shiftToTarget = DenseMatrix.OfArray(new float[,] {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { target.X, target.Y, 1 }
            });
            Matrix<float> output = shiftToZBPoint.Multiply(scaleMatrix).Multiply(shiftToTarget);
            //Matrix<float> output = shiftToZBPoint.Multiply(scaleMatrix);
            Console.WriteLine("[[[[[[[[[[[[");
            Console.WriteLine(shiftToZBPoint.ToString());
            Console.WriteLine(scaleMatrix.ToString());
            Console.WriteLine(shiftToTarget.ToString());
            Console.WriteLine(output.ToString());
            Console.WriteLine("]]]]]]]]]]]]");
            return output;
        }
    }

    public static class FontUtils
    {
        public static string[] SplitIntoTextElements(string input)
        {
            IEnumerable<string> Helper()
            {
                for (var en = StringInfo.GetTextElementEnumerator(input); en.MoveNext();)
                    yield return en.GetTextElement();
            }
            return Helper().ToArray();
        }
    }
}
