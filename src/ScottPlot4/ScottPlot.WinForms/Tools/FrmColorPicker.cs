using System;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 颜色选择窗体
    /// </summary>
    public class FrmColorPicker : FrmStrip
    {
        private UCColorPicker ucColorPicker1;

        public event EventHandler<SelectedColorEventArgs> SelectedColor;

        public FrmColorPicker()
        {
            InitializeComponent();
            ucColorPicker1._frm = this;
            this.ucColorPicker1.OnSelectedColor += UcColorPicker1_OnSelectedColor;
        }

        private void InitializeComponent()
        {
            this.ucColorPicker1 = new UCColorPicker();
            this.SuspendLayout();
            //
            // ucColorPicker1
            //
            this.ucColorPicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucColorPicker1.Location = new System.Drawing.Point(0, 0);
            this.ucColorPicker1.Name = "ucColorPicker1";
            this.ucColorPicker1.Padding = new System.Windows.Forms.Padding(5);
            this.ucColorPicker1.SelectedColor = System.Drawing.Color.Empty;
            this.ucColorPicker1.Size = new System.Drawing.Size(171, 189);
            this.ucColorPicker1.TabIndex = 0;
            //
            // FrmColorPicker
            //
            this.ClientSize = new System.Drawing.Size(171, 189);
            this.Controls.Add(this.ucColorPicker1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "FrmColorPicker";
            this.ResumeLayout(false);
        }

        private void UcColorPicker1_OnSelectedColor(object sender, SelectedColorEventArgs e)
        {
            this.Hide();
            SelectedColor?.Invoke(this, e);
        }

        public new void Show()
        {
            this.Location = System.Windows.Forms.Control.MousePosition;
            base.Show();
        }
    }
}
