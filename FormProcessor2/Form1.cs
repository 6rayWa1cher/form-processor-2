using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace FormProcessor
{
    using TracedObject = IList<(Point point, bool border)>;
    public partial class Form1 : Form
    {
        private readonly FormData _formData = new FormData();
        private Image _image;

        private readonly Color blackColor = Color.FromArgb(0, 0, 0);
        private readonly Color whiteColor = Color.FromArgb(255, 255, 255);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formData.Fields.Add(new AnchorFormElement(new PointF(18, 18), 16));
            _formData.Fields.Add(new AnchorFormElement(new PointF(210 - 2, 18), 16));
            _formData.Fields.Add(new AnchorFormElement(new PointF(18, 297 - 2), 16));
            _formData.Fields.Add(new AnchorFormElement(new PointF(210 - 2, 297 - 2), 16));
            _formData.Fields.Add(new TextFormElement(new PointF(15, 30), "Exampletext", 16));
            _formData.Fields.Add(new TextInputFormElement(new PointF(65, 30), 10));
            TransformButton.Enabled = false;
        }

        private void DpiButton_Click(object sender, EventArgs e)
        {
            int dpi = (int)DpiUpDown.Value;
            Size size = _formData.GetPxSize(dpi);
            Image image = new Bitmap(size.Width, size.Height);
            using (Graphics gr = Graphics.FromImage(image))
            {
                _formData.Draw(gr, dpi);
            }
            UpdateImage(image);
            TransformButton.Enabled = true;
            FindAnchorsButton.Enabled = false;
        }

        private void TransformButton_Click(object sender, EventArgs e)
        {
            Image scaled = ScaleImageSameResolution(_image, 50);
            Image moved = MoveImage(scaled, new Point(scaled.Width / 4, scaled.Height / 4));
            Image rotated = RotateImage(moved, GetRandomNumber(-30, 30));
            scaled.Dispose();
            moved.Dispose();
            UpdateImage(rotated);
            TransformButton.Enabled = false;
            FindAnchorsButton.Enabled = true;
        }
        private void FindAnchorsButton_Click(object sender, EventArgs e)
        {
            IList<TracedObject> list = TraceObjects(_image);
            list = FindAllAnchors(list, 4);
            foreach(TracedObject tracedObject in list)
            {
                Console.WriteLine(GetRatio(GetPerimeter(tracedObject), GetArea(tracedObject)));
            }
            VisualizeFoundObjects(_image, list, Color.Green, false);
            FormImageBox.Refresh();
        }

        private void UpdateImage(Image image)
        {
            _image?.Dispose();
            _image = image;
            FormImageBox.Parent = FormImagePanel;
            FormImageBox.Location = Point.Empty;
            FormImageBox.Image = _image;
            FormImageBox.ClientSize = _image.Size;
        }

        private static Image MoveImage(Image img, Point point)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.White);
                gr.DrawImage(img, point);
            }
            return bmp;
        }
        private static Image ScaleImageSameResolution(Image img, float zoomPercents)
        {
            float zoom = zoomPercents / 100f;
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.White);
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.DrawImage(img, 0, 0, img.Width * zoom, img.Height * zoom);
            }
            return bmp;
        }
        private static Image RotateImage(Image img, float rotationAngleDeg)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.Clear(Color.White);
                gr.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
                gr.RotateTransform(rotationAngleDeg);
                gr.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.DrawImage(img, new Point(0, 0));
            }
            return bmp;
        }
        private static int GetRandomNumber(int from, int to)
        {
            Random rnd = new Random();
            return rnd.Next(from, to);
        }

        private void Binarize(Bitmap image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    int intensity = GetIntensity(color);
                    Color newColor = intensity < 128 ? blackColor : whiteColor;
                    image.SetPixel(x, y, newColor);
                }
            }
        }

        private Bitmap ExtendOnePixel(Image image)
        {
            Bitmap bitmap = new Bitmap(image.Width + 2, image.Height + 2);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.Clear(Color.White);
                gr.DrawImageUnscaled(image, 1, 1);
            }
            return bitmap;
        }
        
        private int GetIntensity(Color color)
        {
            return Math.Min(255, (int) Math.Round(0.3f * color.R + 0.59f * color.G + 0.11f * color.B));
        }

        private void VisualizeFoundObjects(Image image, IList<TracedObject> objs, Color color, bool bordersOnly = false)
        {
            Brush brush = new SolidBrush(color);
            using (Graphics gr = Graphics.FromImage(image))
            {
                foreach(TracedObject obj in objs)
                {
                    foreach((Point point, bool border) in obj)
                    {
                        if (bordersOnly && !border) continue;
                        gr.FillRectangle(brush, point.X, point.Y, 1, 1);
                    }
                }
            }
        }

        private IList<TracedObject> TraceObjects(Image source)
        {
            using (Bitmap img = ExtendOnePixel(source))
            {
                Binarize(img);
                var objects = new List<IList<(Point, bool)>>();
                var visited = new bool[img.Width, img.Height];
                for (int i = 1; i < img.Width - 1; i++)
                {
                    for (int j = 1; j < img.Height - 1; j++)
                    {
                        if (visited[i, j]) continue;
                        visited[i, j] = true;
                        Color color = img.GetPixel(i, j);
                        if (color == blackColor)
                        {
                            TracedObject points = TraceObject(img, new Point(i, j), visited);
                            TracedObject shiftedPoints = 
                                points
                                .Select(tuple => (new Point(tuple.point.X - 1, tuple.point.Y - 1), tuple.border))
                                .ToList();
                            objects.Add(shiftedPoints);
                        }
                    }
                }
                return objects;
            }
        }

        private TracedObject TraceObject(Bitmap img, Point start, bool[,] visited)
        {
            var points = new List<(Point, bool)>();
            var queue = new Queue<Point>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                Point p = queue.Dequeue();
                int px = p.X, py = p.Y;
                var neighbors = new Point[]
                {
                    new Point(px - 1, py - 1),
                    new Point(px - 1, py),
                    new Point(px - 1, py + 1),
                    new Point(px, py - 1),
                    new Point(px, py + 1),
                    new Point(px + 1, py - 1),
                    new Point(px + 1, py),
                    new Point(px + 1, py + 1)
                };
                bool border = false;
                foreach (Point neighbor in neighbors)
                {
                    int nx = neighbor.X, ny = neighbor.Y;
                    if (visited[nx, ny]) continue;
                    visited[nx, ny] = true;
                    Color neighborColor = img.GetPixel(nx, ny);
                    if (neighborColor == blackColor)
                    {
                        queue.Enqueue(neighbor);
                    }
                    else
                    {
                        border = true;
                    }
                }
                points.Add((p, border));
            }
            return points;
        }

        private IList<TracedObject> FindAllAnchors(IList<TracedObject> tracedObjects, int anchorsCount)
        {
            var statisticsList = new List<(float ratio, TracedObject obj)>();
            foreach(TracedObject tracedObject in tracedObjects)
            {
                float perimeter = GetPerimeter(tracedObject);
                float area = GetArea(tracedObject);
                float ratio = GetRatio(perimeter, area);
                statisticsList.Add((ratio, tracedObject));
            }
            float avgArea = tracedObjects.Average(GetArea);
            float ratioOfCircle = GetRatioOfCircle();
            return statisticsList
                .OrderBy(s => Math.Abs(s.ratio - ratioOfCircle))
                .Where(s => GetArea(s.obj) > avgArea)
                .Take(anchorsCount)
                .Select(s => s.obj)
                .ToList();
        }

        private float GetPerimeter(TracedObject tracedObject)
        {
            return tracedObject.Where(tuple => tuple.border).Count();
        }

        private float GetArea(TracedObject tracedObject)
        {
            return tracedObject.Count;
        }

        private float GetRatio(float perimeter, float area)
        {
            return perimeter / (float)Math.Sqrt(area);
        }

        private float GetRatioOfCircle()
        {
            float pi = (float)Math.PI;
            return GetRatio(2 * pi, pi);
        }
    }

    public static class DrawUtils
    {
        public static float InchToMm(float inches)
        {
            return inches * 25.4f;
        }

        public static float InchToPt(float inches)
        {
            return inches * 72f;
        }

        public static float MmToInch(float mm)
        {
            return mm / 25.4f;
        }

        public static float PtToInch(float pts)
        {
            return pts / 72f;
        }

        public static float InchToPx(float inches, int dpi)
        {
            return inches * dpi;
        }

        public static float MmToPx(float mm, int dpi)
        {
            return InchToPx(MmToInch(mm), dpi);
        }

        public static PointF MmToPx(PointF mmPoint, int dpi)
        {
            return new PointF(MmToPx(mmPoint.X, dpi), MmToPx(mmPoint.Y, dpi));
        }

        public static SizeF MmToPx(SizeF mmSize, int dpi)
        {
            return new SizeF(MmToPx(mmSize.Width, dpi), MmToPx(mmSize.Height, dpi));
        }

        public static float PtToPx(float pts, int dpi)
        {
            return InchToPx(PtToInch(pts), dpi);
        }
    }

    public class DrawContext
    {
        public Graphics Graphics { get; set; }

        public int Dpi { get; set; }

        public DrawContext(Graphics graphics, int dpi)
        {
            Graphics = graphics;
            Dpi = dpi;
        }
    }

    public class OcrContext
    {

    }

    public abstract class FormElement
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public PointF MmPosition { get; set; } = PointF.Empty;

        public Color Color { get; set; } = Color.Black;

        public abstract void Draw(DrawContext ctx);

        public FormElement()
        {

        }

        public FormElement(PointF mmPosition)
        {
            MmPosition = mmPosition;
        }
    }


    public abstract class InputFormElement<T> : FormElement
    {
        public abstract T Read(OcrContext ctx);

        public InputFormElement()
        {

        }

        public InputFormElement(PointF mmPosition) : base(mmPosition)
        {

        }
    }

    public class TextFormElement : FormElement
    {
        public string Text { get; set; } = string.Empty;

        public int Size { get; set; } = 16;

        public string FontFamily { get; set; } = "Arial";

        public override void Draw(DrawContext ctx)
        {
            float fontSize = DrawUtils.PtToPx(Size, ctx.Dpi);
            var drawFont = new Font(FontFamily, fontSize);
            ctx.Graphics.DrawString(Text, drawFont, Brushes.Black, DrawUtils.MmToPx(MmPosition, ctx.Dpi));
        }

        public TextFormElement()
        {

        }

        public TextFormElement(PointF mmPosition) : base(mmPosition)
        {

        }

        public TextFormElement(PointF mmPosition, string text, int size) : base(mmPosition)
        {
            Text = text;
            Size = size;
        }
    }

    public class CharacterInputFormElement : InputFormElement<string>
    {
        public SizeF CellSizeMm { get; set; } = new SizeF(10, 10);

        public float CellBorderSizeMm { get; set; } = 0.5f;

        public CharacterInputFormElement()
        {
        }

        public CharacterInputFormElement(PointF mmPosition) : base(mmPosition)
        {

        }

        public CharacterInputFormElement(PointF mmPosition, SizeF cellSizeMm, float cellBorderSizeMm) : this(mmPosition)
        {
            CellSizeMm = cellSizeMm;
            CellBorderSizeMm = cellBorderSizeMm;
        }

        public override void Draw(DrawContext ctx)
        {
            Graphics gr = ctx.Graphics;
            PointF position = DrawUtils.MmToPx(MmPosition, ctx.Dpi);
            SizeF cellSize = DrawUtils.MmToPx(CellSizeMm, ctx.Dpi);
            float cellBorderSize = DrawUtils.MmToPx(CellBorderSizeMm, ctx.Dpi);
            var pen = new Pen(Color, cellBorderSize);
            gr.DrawRectangle(pen, position.X, position.Y, cellSize.Width, cellSize.Height);
        }

        public override string Read(OcrContext ctx)
        {
            throw new NotImplementedException();
        }
    }

    public class TextInputFormElement : InputFormElement<string>
    {
        public int Cells { get; set; } = 10;

        public SizeF CellSizeMm { get; set; } = new SizeF(10, 10);

        public float CellBorderSizeMm { get; set; } = 0.5f;

        public TextInputFormElement()
        {
        }

        public TextInputFormElement(PointF mmPosition, int cells) : base(mmPosition)
        {
            Cells = cells;
        }

        public TextInputFormElement(PointF mmPosition, int cells, SizeF cellSizeMm, float cellBorderSizeMm) : this(mmPosition, cells)
        {
            CellSizeMm = cellSizeMm;
            CellBorderSizeMm = cellBorderSizeMm;
        }

        public override void Draw(DrawContext ctx)
        {
            var elem = new CharacterInputFormElement(MmPosition, CellSizeMm, CellBorderSizeMm)
            {
                Color = Color
            };

            for (int i = 0; i < Cells; i++)
            {
                float x = MmPosition.X + CellSizeMm.Width * i;
                var mmPos = new PointF(x, MmPosition.Y);
                elem.MmPosition = mmPos;
                elem.Draw(ctx);
            }
        }

        public override string Read(OcrContext ctx)
        {
            var elem = new CharacterInputFormElement(MmPosition, CellSizeMm, CellBorderSizeMm)
            {
                Color = Color
            };
            var sb = new StringBuilder();
            for (int i = 0; i < Cells; i++)
            {
                float x = MmPosition.X + CellSizeMm.Width * i;
                var mmPos = new PointF(x, MmPosition.Y);
                elem.MmPosition = mmPos;
                sb.Append(elem.Read(ctx));
            }
            return sb.ToString();
        }
    }

    public class AnchorFormElement : FormElement
    {
        public int Size { get; set; } = 8;

        public override void Draw(DrawContext ctx)
        {
            Graphics gr = ctx.Graphics;
            float realSize = DrawUtils.MmToPx(Size, ctx.Dpi);
            PointF position = DrawUtils.MmToPx(MmPosition, ctx.Dpi);
            gr.FillEllipse(new SolidBrush(Color), new Rectangle(
                (int)(position.X - realSize), 
                (int)(position.Y - realSize),
                (int)realSize, (int)realSize
                ));
        }

        public AnchorFormElement()
        {

        }

        public AnchorFormElement(PointF mmPosition) : base(mmPosition)
        {

        }

        public AnchorFormElement(PointF mmPosition, int size) : base(mmPosition)
        {
            Size = size;
        }
    }

    public class FormData
    {
        public ISet<FormElement> Fields { get; set; } = new HashSet<FormElement>();

        public SizeF MmSize { get; set; } = new SizeF(210, 297);

        public Size GetPxSize(int dpi)
        {
            SizeF pxSize = DrawUtils.MmToPx(MmSize, dpi);
            return new Size((int)Math.Ceiling(pxSize.Width), (int)Math.Ceiling(pxSize.Height));
        }

        public void Draw(Graphics gr, int dpi)
        {
            gr.Clear(Color.White);
            SizeF pxSize = DrawUtils.MmToPx(MmSize, dpi);
            gr.DrawRectangle(Pens.White, 0, 0, pxSize.Width, pxSize.Height);
            var ctx = new DrawContext(gr, dpi);
            foreach (FormElement formElement in Fields)
            {
                formElement.Draw(ctx);
            }
        }
    }
}
