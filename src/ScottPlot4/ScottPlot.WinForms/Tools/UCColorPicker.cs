using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    internal static class GraphicsExt
    {
        public static Point OffsetEx(this Point p, int x, int y)
        {
            return new Point(p.X + x, p.Y + y);
        }
    }

    /// <summary>
    /// 颜色选择控件
    /// </summary>
    public partial class UCColorPicker : UserControl
    {
        private static string StrOther = "Other...";

        private Button[,] _btns;
        private int c = 5;
        internal FrmColorPicker _frm;
        private Color[,] PresetColors { get; set; }

        /// <summary>
        ///
        /// </summary>
        public Color SelectedColor { get; set; }

        public event EventHandler<SelectedColorEventArgs> OnSelectedColor;

        public UCColorPicker()
        {
            InitializeComponent();
            InitControl();
            this.btn_other.Text = StrOther;
            btn_other.Click += Btn_other_Click;
            this.Load += UCColorPicker_Load;
            this.panel1.SizeChanged += Panel1_SizeChanged;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            this.SelectedColor = (sender as Button).BackColor;
            OnSelectedColor?.Invoke(this, new SelectedColorEventArgs(this.SelectedColor));
        }

        private void Btn_other_Click(object sender, EventArgs e)
        {
            if (_frm != null)
                _frm.EnableHide = false;
            var diag = new ColorDialog()
            {
                FullOpen = true,
            };
            if (diag.ShowDialog(this.ParentForm.Owner) == DialogResult.OK)
            {
                this.SelectedColor = diag.Color;
                OnSelectedColor?.Invoke(this, new SelectedColorEventArgs(this.SelectedColor));
            }
            if (_frm != null)
                _frm.EnableHide = true;
        }

        private void InitControl()
        {
            var btns = new Button[c, c];
            this.PresetColors = new Color[c, c];
            int d1 = 255 / c, d2 = 255 / c;
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    this.PresetColors[i, j] = Color.FromArgb(i * d1, j * d2, (i * d1 + j * d2) % 255);
                    var btn = new Button()
                    {
                        Text = "",
                        BackColor = this.PresetColors[i, j],
                        FlatStyle = FlatStyle.Flat,
                    };
                    btn.Click += Btn_Click;
                    btns[i, j] = btn;
                }
            }
            _btns = btns;
        }

        private void Panel1_SizeChanged(object sender, EventArgs e)
        {
            Recalc();
        }

        private void Recalc()
        {
            this.panel1.Controls.Clear();
            var size = this.panel1.Size;
            var w = size.Width - this.panel1.Padding.Left - this.panel1.Padding.Right;
            var h = size.Height - this.panel1.Padding.Top - this.panel1.Padding.Bottom;
            var distance = 4;
            var alldis = distance * (c - 1);
            var miniWH = Math.Min(w, h) - alldis;
            var unitWH = miniWH / c;
            var bsize = new Size(unitWH, unitWH);
            Point pos;
            if (w > h)
            {
                pos = new Point(this.panel1.Padding.Left + (w - h) / 2, this.panel1.Padding.Top);
            }
            else
            {
                pos = new Point(this.panel1.Padding.Left, this.panel1.Padding.Top + (h - w) / 2);
            }
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    _btns[i, j].Size = bsize;
                    _btns[i, j].Location = pos.OffsetEx(i * distance + i * unitWH, j * distance + j * unitWH);
                    this.panel1.Controls.Add(_btns[i, j]);
                }
            }
        }

        private void UCColorPicker_Load(object sender, EventArgs e)
        {
            Recalc();
        }
    }
}
