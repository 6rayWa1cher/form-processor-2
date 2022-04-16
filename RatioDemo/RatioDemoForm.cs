using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatioDemo
{
    public partial class RatioDemoForm : Form
    {
        private Graphics _gr;
        private readonly Shape _shape;
        private int _indx = -1;
        private readonly int _controlSize = 5;
        private bool _moved = false;

        public RatioDemoForm()
        {
            InitializeComponent();
            _shape = new Shape(new List<Point>(new Point[] {
                new Point(100, 100), new Point(100, 200), new Point(200, 200), new Point(200, 100) 
            }), Pens.Black);
        }


        private int NormalizeDegrees(int deg)
        {
            return -180 + ((deg + 180) % 360);
        }


        private void RedrawShapes()
        {
            _gr.Clear(Color.White);

            PaintShape(_shape.Points, _shape.PreferredPen);

            foreach (Point point in _shape.Points)
                PaintControl(point);
        }

        private float GetLineLength(Point a, Point b)
        {
            int x = b.X - a.X;
            int y = b.Y - a.Y;
            return (float) Math.Sqrt(x * x + y * y);
        }

        private void PaintShape(List<Point> points, Pen pen)
        {
            int count = points.Count;
            for (int i = 0; i < count; i++)
            {
                Point currentPoint = points[i], nextPoint = points[(i + 1) % count];
                PaintControl(currentPoint);
                _gr.DrawLine(pen, currentPoint, nextPoint);
            }
        }

        private void PaintControl(Point point)
        {
            _gr.DrawRectangle(new Pen(Color.Green, 1), point.X - _controlSize, point.Y - _controlSize, _controlSize * 2 + 1, _controlSize * 2 + 1);
        }

        private bool IsMouseInPointControl(MouseEventArgs e, Point point)
        {
            return (e.X - _controlSize < point.X) && (point.X < e.X + _controlSize) &&
                        (e.Y - _controlSize < point.Y) && (point.Y < e.Y + _controlSize);
        }

        private void RatioDemoForm_Paint(object sender, PaintEventArgs e)
        {
            RedrawShapes();
        }

        private void RatioDemoForm_Load(object sender, EventArgs e)
        {
            _gr = CreateGraphics();
            _gr.Clear(Color.White);
            UpdateForm();
        }

        private void UpdateForm()
        {
            RatioLabel.Text = GetRatio(_shape).ToString("0.00");
        }

        private float GetRatio(Shape shape)
        {
            return GetPerimeter(shape) / (float)Math.Sqrt(GetArea(shape));
        }

        private float GetArea(Shape shape)
        {
            float sum = 0;
            IList<Point> points = shape.Points;
            int n = points.Count;
            for (int i = 0; i < n - 1; i++)
            {
                sum += points[i].X * points[i + 1].Y;
                sum -= points[i + 1].X * points[i].Y;
            }
            sum += points[n - 1].X * points[0].Y;
            sum -= points[0].X * points[n - 1].Y;
            float output = Math.Abs(sum) / 2f;
            Console.WriteLine(output);
            return output;
        }

        private float GetPerimeter(Shape shape)
        {
            int count = shape.Points.Count;
            float sum = 0;
            for (int i = 0; i < count; i++)
            {
                Point point1 = shape.Points[i];
                Point point2 = shape.Points[(i + 1) % count];
                sum += GetLineLength(point1, point2);
            }
            return sum;
        }

        private void RatioDemoForm_MouseDown(object sender, MouseEventArgs e)
        {
            _indx = -1;
            _moved = false;
            var point = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                List<Point> points = _shape.Points;
                for (int i = 0; i < points.Count; i++)
                    if (IsMouseInPointControl(e, points[i]))
                    {
                        _indx = i;
                    }
                if (_indx == -1)
                {
                    _shape.Points.Add(point);
                    _indx = _shape.Points.Count - 1;
                    RedrawShapes();
                    UpdateForm();
                }
            }
        }

        private void RatioDemoForm_MouseMove(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X, e.Y);
            if (_indx != -1)
            {
                _moved = true;
                List<Point> points = _shape.Points;
                points[_indx] = point;
                RedrawShapes();
                UpdateForm();
            }
        }

        private void RatioDemoForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_moved && _indx != -1)
            {
                List<Point> points = _shape.Points;
                points.RemoveAt(_indx);
            }
            _indx = -1;
            _moved = false;
            RedrawShapes();
            UpdateForm();
        }
    }

    public class Shape
    {
        public List<Point> Points { set; get; }
        public Pen PreferredPen { get; set; }

        public Shape(Pen preferredPen) : this(new List<Point>(), preferredPen) { }
        public Shape(List<Point> points, Pen preferredPen)
        {
            Points = points;
            PreferredPen = preferredPen;
        }
    }
}
