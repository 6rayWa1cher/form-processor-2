using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormProcessor
{
    public partial class Form1 : Form
    {
        private readonly FormData _formData = new FormData();
        private Image _image;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _formData.Fields.Add(new TextFormElement(new PointF(0, 0), "Exampletext", 16));
            _formData.Fields.Add(new TextInputFormElement(new PointF(50, 0), 10));
        }

        private void DpiButton_Click(object sender, EventArgs e)
        {
            int dpi = (int)DpiUpDown.Value;
            Size size = _formData.GetPxSize(dpi);
            _image = new Bitmap(size.Width, size.Height);
            Graphics gr = Graphics.FromImage(_image);

            _formData.Draw(gr, dpi);

            FormImageBox.Parent = FormImagePanel;
            FormImageBox.Location = Point.Empty;
            FormImageBox.Image = _image;
            FormImageBox.ClientSize = _image.Size;
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
