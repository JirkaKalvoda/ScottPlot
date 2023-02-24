
namespace ScottPlot.WinForms
{
    partial class FormPlotOption
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gb_Scatter = new System.Windows.Forms.GroupBox();
            this.dgv_Scatter = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.b_Discard = new System.Windows.Forms.Button();
            this.b_Save = new System.Windows.Forms.Button();
            this.dgc_Scatter_Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_Scatter_LineStyle = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgc_Scatter_LineColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_Scatter_LineWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_Scatter_MarkerShape = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgc_Scatter_MarkerColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgc_Scatter_MarkerSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gb_Scatter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Scatter)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Scatter
            // 
            this.gb_Scatter.Controls.Add(this.dgv_Scatter);
            this.gb_Scatter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gb_Scatter.Location = new System.Drawing.Point(6, 6);
            this.gb_Scatter.Margin = new System.Windows.Forms.Padding(10);
            this.gb_Scatter.Name = "gb_Scatter";
            this.gb_Scatter.Padding = new System.Windows.Forms.Padding(6);
            this.gb_Scatter.Size = new System.Drawing.Size(617, 209);
            this.gb_Scatter.TabIndex = 0;
            this.gb_Scatter.TabStop = false;
            this.gb_Scatter.Text = "Scatter Options";
            // 
            // dgv_Scatter
            // 
            this.dgv_Scatter.AllowUserToAddRows = false;
            this.dgv_Scatter.AllowUserToDeleteRows = false;
            this.dgv_Scatter.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_Scatter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Scatter.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgc_Scatter_Label,
            this.dgc_Scatter_LineStyle,
            this.dgc_Scatter_LineColor,
            this.dgc_Scatter_LineWidth,
            this.dgc_Scatter_MarkerShape,
            this.dgc_Scatter_MarkerColor,
            this.dgc_Scatter_MarkerSize});
            this.dgv_Scatter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Scatter.Location = new System.Drawing.Point(6, 22);
            this.dgv_Scatter.Name = "dgv_Scatter";
            this.dgv_Scatter.RowHeadersVisible = false;
            this.dgv_Scatter.RowTemplate.Height = 23;
            this.dgv_Scatter.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_Scatter.Size = new System.Drawing.Size(605, 181);
            this.dgv_Scatter.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.b_Discard, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.b_Save, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 271);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(617, 55);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // b_Discard
            // 
            this.b_Discard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_Discard.Location = new System.Drawing.Point(411, 9);
            this.b_Discard.Name = "b_Discard";
            this.b_Discard.Size = new System.Drawing.Size(102, 37);
            this.b_Discard.TabIndex = 1;
            this.b_Discard.Text = "Discard";
            this.b_Discard.UseVisualStyleBackColor = true;
            // 
            // b_Save
            // 
            this.b_Save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.b_Save.Location = new System.Drawing.Point(103, 9);
            this.b_Save.Name = "b_Save";
            this.b_Save.Size = new System.Drawing.Size(102, 37);
            this.b_Save.TabIndex = 0;
            this.b_Save.Text = "Save";
            this.b_Save.UseVisualStyleBackColor = true;
            // 
            // dgc_Scatter_Label
            // 
            this.dgc_Scatter_Label.HeaderText = "Label";
            this.dgc_Scatter_Label.Name = "dgc_Scatter_Label";
            this.dgc_Scatter_Label.Width = 64;
            // 
            // dgc_Scatter_LineStyle
            // 
            this.dgc_Scatter_LineStyle.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.dgc_Scatter_LineStyle.HeaderText = "Line Style";
            this.dgc_Scatter_LineStyle.Name = "dgc_Scatter_LineStyle";
            this.dgc_Scatter_LineStyle.Width = 68;
            // 
            // dgc_Scatter_LineColor
            // 
            this.dgc_Scatter_LineColor.HeaderText = "Line Color";
            this.dgc_Scatter_LineColor.Name = "dgc_Scatter_LineColor";
            this.dgc_Scatter_LineColor.ReadOnly = true;
            this.dgc_Scatter_LineColor.Width = 92;
            // 
            // dgc_Scatter_LineWidth
            // 
            this.dgc_Scatter_LineWidth.HeaderText = "Line Width";
            this.dgc_Scatter_LineWidth.Name = "dgc_Scatter_LineWidth";
            this.dgc_Scatter_LineWidth.Width = 94;
            // 
            // dgc_Scatter_MarkerShape
            // 
            this.dgc_Scatter_MarkerShape.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.dgc_Scatter_MarkerShape.HeaderText = "Marker Shape";
            this.dgc_Scatter_MarkerShape.Name = "dgc_Scatter_MarkerShape";
            this.dgc_Scatter_MarkerShape.Width = 97;
            // 
            // dgc_Scatter_MarkerColor
            // 
            this.dgc_Scatter_MarkerColor.HeaderText = "Marker Color";
            this.dgc_Scatter_MarkerColor.Name = "dgc_Scatter_MarkerColor";
            this.dgc_Scatter_MarkerColor.ReadOnly = true;
            this.dgc_Scatter_MarkerColor.Width = 112;
            // 
            // dgc_Scatter_MarkerSize
            // 
            this.dgc_Scatter_MarkerSize.HeaderText = "Marker Size";
            this.dgc_Scatter_MarkerSize.Name = "dgc_Scatter_MarkerSize";
            this.dgc_Scatter_MarkerSize.Width = 103;
            // 
            // FormPlotOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 332);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.gb_Scatter);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormPlotOption";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.ShowIcon = false;
            this.Text = "Plot Options";
            this.gb_Scatter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Scatter)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Scatter;
        private System.Windows.Forms.DataGridView dgv_Scatter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button b_Discard;
        private System.Windows.Forms.Button b_Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Scatter_Label;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgc_Scatter_LineStyle;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Scatter_LineColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Scatter_LineWidth;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgc_Scatter_MarkerShape;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Scatter_MarkerColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgc_Scatter_MarkerSize;
    }
}