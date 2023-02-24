using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 悬浮窗体
    /// <br></br>
    /// 失去焦点、变为非激活状态时会隐藏
    /// </summary>
    public class FrmStrip : Form
    {
        public bool EnableKeyClose_ESC { get; set; }
        public bool EnableKeyHide_ESC { get; set; }
        internal bool EnableHide { get; set; }

        public FrmStrip() : base()
        {
            InitializeComponent();
            EnableHide = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStrip";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.Location = new Point(100, 100);
            this.ResumeLayout(false);

            this.LostFocus += FrmStrip_LostFocus;
            this.Leave += FrmStrip_Leave;
            this.Deactivate += FrmStrip_Deactivate;
            this.FormClosing += FrmStrip_FormClosing;
        }

        private void FrmStrip_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void FrmStrip_Deactivate(object sender, EventArgs e)
        {
            if (EnableHide)
                this.Hide();
        }

        private void FrmStrip_Leave(object sender, EventArgs e)
        {
            if (EnableHide)
                this.Hide();
        }

        private void FrmStrip_LostFocus(object sender, EventArgs e)
        {
            if (EnableHide)
                this.Hide();
        }

        public void Show(int x, int y)
        {
            Show(new Point(x, y));
        }

        public void Show(Point p)
        {
            this.Location = p;
            this.Show();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (EnableHide)
                this.Hide();
            base.OnLostFocus(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (EnableKeyHide_ESC)
                    this.Hide();
                else if (EnableKeyClose_ESC)
                    this.Close();
            }
            base.OnKeyDown(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0112)
            {
                if ((m.WParam.ToInt32() & 0xFFF0) == 0xF060
                    && m.LParam.ToInt32() == 0)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }
    }
}