using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierFontEditor
{
    public partial class EditorForm : Form
    {
        private BezierFont _font = new BezierFont();
        private string _currentChar = "а";
        private Image _backImage = null;
        private BezierLetter CurrentLetter => _font.Letters[_currentChar];
        private readonly Rectangle _renderWindow;
        private Size _windowSize;
        private readonly int _controlSize = 5;
        private (int curveIndex, int pointIndex) _indx = (-1, -1);
        private bool _disableCallbacks = false;
        private FontProcessor _fontProcessor = new FontProcessor();
        public readonly Size _borders = new Size(250, 350);
        public EditorForm()
        {
            InitializeComponent();
            _font.Borders = _borders;
            _font.Letters.Add(_currentChar, new BezierLetter());
            _windowSize = FormImageBox.Size;
            _renderWindow = new Rectangle((_windowSize.Width - _borders.Width) / 2, (_windowSize.Height - _borders.Height) / 2, _borders.Width, _borders.Height);
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            FormImageBox.Image = new Bitmap(_windowSize.Width, _windowSize.Height);
            BaselineUpDown.Maximum = FormImageBox.Height;
            BaselineUpDown.Value = FormImageBox.Height / 2;
            SaveFontDialog.Filter =
                "JSON files (*.json)|*.json|All files (*.*)|*.*";
            SaveFontDialog.DefaultExt = "json";
            OpenFontDialog.Filter = SaveFontDialog.Filter;
            OpenFontDialog.DefaultExt = SaveFontDialog.DefaultExt;
            UpdateFormElements();
        }

        private void EditorForm_Paint(object sender, PaintEventArgs e)
        {
            if (_disableCallbacks) return;
            RenderLetter();
        }

        private void UpdateFormElements()
        {
            _disableCallbacks = true;
            try
            {
                AddLetterButton.Enabled = _currentChar.Length == 1;
                LetterTextBox.Text = _currentChar;
                RemoveLetterButton.Enabled = _font.Letters.Count > 1;
                int indexBefore = LetterComboBox.SelectedIndex;
                LetterComboBox.Items.Clear();
                LetterComboBox.Items.AddRange(_font.Letters.Keys.ToArray());
                LetterComboBox.SelectedIndex = indexBefore >= _font.Letters.Count || indexBefore < 0 ? 0 : indexBefore;
                Image image = _backImage ?? FormImageBox.Image;
                float zoom = (float)ZoomUpDown.Value;
                int vMax = (int)Math.Ceiling(image.Height * zoom / 100f);
                VScrollBar.Value = vMax < VScrollBar.Value ? vMax : VScrollBar.Value;
                VScrollBar.Maximum = vMax;
                VScrollBar.Enabled = vMax != 0;
                int hMax = (int)Math.Ceiling(image.Width * zoom / 100f);
                HScrollBar.Value = hMax < HScrollBar.Value ? hMax : HScrollBar.Value;
                HScrollBar.Maximum = hMax;
                HScrollBar.Enabled = hMax != 0;
            } finally
            {
                _disableCallbacks = false;
            }
        }

        private void RenderLetter()
        {
            Graphics gr = Graphics.FromImage(FormImageBox.Image);
            gr.Clear(Color.FromKnownColor(KnownColor.Control));
            if (_backImage != null)
            {
                float zoom = (float)ZoomUpDown.Value;
                gr.DrawImage(_backImage, -HScrollBar.Value, -VScrollBar.Value, _backImage.Width * zoom / 100f, _backImage.Height * zoom / 100f);
            } 
            if (ShowControlsCheckbox.Checked)
                foreach (BezierCurve curve in CurrentLetter.Curves)
                    gr.DrawLines(Pens.Aqua, curve.Points.ToArray());
            CurrentLetter.Draw(gr, Pens.Red);
            gr.DrawRectangle(Pens.Gray, _renderWindow);
            gr.DrawLine(Pens.Green, 0, (int)BaselineUpDown.Value, FormImageBox.Width, (int)BaselineUpDown.Value);
            if (ShowControlsCheckbox.Checked)
                foreach (BezierCurve curve in CurrentLetter.Curves)
                    foreach (PointF point in curve.Points)
                        PaintControl(gr, new Point((int)point.X, (int)point.Y));
            FormImageBox.Refresh();
        }

        private void ShowControlsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            RenderLetter();
        }

        private void LetterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            _currentChar = (string)LetterComboBox.SelectedItem;
            UpdateFormElements();
            RenderLetter();
        }

        private void LoadBackImageButton_Click(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            DialogResult result = OpenBackImageDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _backImage = Image.FromFile(OpenBackImageDialog.FileName);
                UpdateFormElements();
                RenderLetter();
            }
        }

        private void AddLetterButton_Click(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            string toAdd = "New letter";
            _font.Letters.Add(toAdd, new BezierLetter());
            _currentChar = toAdd;
            UpdateFormElements();
            LetterComboBox.SelectedItem = _currentChar;
            RenderLetter();
        }

        private void RemoveLetterButton_Click(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            _font.Letters.Remove(_currentChar);
            _currentChar = _font.Letters.OrderByDescending(kv => kv.Key.Length).First().Key;
            UpdateFormElements();
            LetterComboBox.SelectedIndex = new List<string>(_font.Letters.Keys).FindIndex(item => item == _currentChar);
            RenderLetter();
        }

        private void SaveFontButton_Click(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            DialogResult result = SaveFontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _fontProcessor.SaveFont(SaveFontDialog.FileName, _font);
            }
        }

        private void LoadFontButton_Click(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            DialogResult result = OpenFontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _font = _fontProcessor.LoadFont(OpenFontDialog.FileName);
                UpdateFormElements();
                RenderLetter();
            }
        }

        private void LetterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            string oldValue = _currentChar;
            string newValue = LetterTextBox.Text;
            _currentChar = newValue;
            _font.Letters[newValue] = _font.Letters[oldValue];
            _font.Letters.Remove(oldValue);
            UpdateFormElements();
        }

        private void ZoomUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            UpdateFormElements();
            RenderLetter();
        }

        private void BaselineUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (_disableCallbacks) return;
            Console.WriteLine(BaselineUpDown.Value);
            RenderLetter();
        }

        private bool IsMouseInPointControl(MouseEventArgs e, PointF point)
        {
            return (e.X - _controlSize < point.X) && (point.X < e.X + _controlSize) &&
                        (e.Y - _controlSize < point.Y) && (point.Y < e.Y + _controlSize);
        }

        private void FormImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_disableCallbacks) return;
            _indx = (-1, -1);
            IList<BezierCurve> curves = CurrentLetter.Curves;
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < curves.Count; i++)
                {
                    BezierCurve curve = curves[i];
                    if (IsMouseInPointControl(e, curve.Start))
                        _indx = (i, 0);
                    if (IsMouseInPointControl(e, curve.Middle))
                        _indx = (i, 1);
                    if (IsMouseInPointControl(e, curve.End))
                        _indx = (i, 2);
                }
                if (_indx == (-1, -1))
                {
                    PointF startPoint = curves.Count > 0 ? curves[curves.Count - 1].End : new PointF(e.X, e.Y);
                    var endPoint = new PointF(e.X, e.Y);
                    var middlePoint = new PointF((endPoint.X + startPoint.X) / 2, (endPoint.Y + startPoint.Y) / 2);
                    var bezierCurve = new BezierCurve(startPoint, middlePoint, endPoint);
                    curves.Add(bezierCurve);
                    _indx = (curves.Count - 1, 3);
                    UpdateFormElements();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < curves.Count; i++)
                {
                    BezierCurve curve = curves[i];
                    foreach (PointF point in curve.Points)
                    {
                        if (IsMouseInPointControl(e, point))
                        {
                            _indx = (i, -1);
                        }
                    }
                }
            }
        }

        private void FormImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_disableCallbacks) return;
            var point = new Point(e.X, e.Y);
            if (_indx.curveIndex != -1 && _indx.pointIndex != -1)
            {
                (int curveIndex, int pointIndex) = _indx;
                BezierCurve curve = CurrentLetter.Curves[curveIndex];
                switch (pointIndex)
                {
                    case 0:
                        curve.Start = point;
                        break;
                    case 1:
                        curve.Middle = point;
                        break;
                    case 2:
                        curve.End = point;
                        break;
                    case 3:
                        curve.Middle = new PointF((curve.Start.X + point.X) / 2, (curve.Start.Y + point.Y) / 2);
                        curve.End = point;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                RenderLetter();
            }
        }

        private void FormImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (_disableCallbacks) return;
            if (_indx.curveIndex != -1 && _indx.pointIndex == -1)
            {
                int index = _indx.curveIndex;
                IList<BezierCurve> curves = CurrentLetter.Curves;
                if (index != 0 && index != curves.Count - 1)
                {
                    BezierCurve prevCurve = curves[index - 1], nextCurve = curves[index + 1];
                    var jointPoint = new PointF((prevCurve.End.X + nextCurve.Start.X) / 2, (prevCurve.End.Y + nextCurve.Start.Y) / 2);
                    prevCurve.End = jointPoint;
                    nextCurve.Start = jointPoint;
                }
                curves.RemoveAt(index);
            }
            _indx = (-1, -1);
            RenderLetter();
        }

        private void PaintControl(Graphics gr, Point point)
        {
            gr.DrawRectangle(new Pen(Color.Green, 1), point.X - _controlSize, point.Y - _controlSize, _controlSize * 2 + 1, _controlSize * 2 + 1);
        }

        private void FormImagePanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (_disableCallbacks) return;
            RenderLetter();
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (_disableCallbacks) return;
            RenderLetter();
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (_disableCallbacks) return;
            RenderLetter();
        }
    }
}
