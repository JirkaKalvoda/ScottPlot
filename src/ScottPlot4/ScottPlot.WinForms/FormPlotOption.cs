using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    public partial class FormPlotOption : Form
    {
        /// <summary>
        /// 要传回的设置
        /// </summary>
        private PlotOption po;

        /// <summary>
        /// 临时的设置
        /// </summary>
        private PlotOption temp;

        private BindingSource dgvbs_Scatter;

        /// <summary>
        /// 当前点击的颜色单元格的行号
        /// </summary>
        private int currentColorRow;

        /// <summary>
        /// 当前点击的颜色单元格的行号
        /// </summary>
        private int currentColorColumn;

        /// <summary>
        /// 选择颜色的方形窗口
        /// </summary>
        private FrmColorPicker frmColorPicker = new FrmColorPicker();

        public FormPlotOption(PlotOption plotOption)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dgv_Scatter.AutoGenerateColumns = false;
            dgc_Scatter_LineColor.CellTemplate = new UCDgvBtnCell();
            dgc_Scatter_MarkerColor.CellTemplate = dgc_Scatter_LineColor.CellTemplate;
            EnumExtension.BindEnumListToDataSource(dgc_Scatter_LineStyle, ((LineStyle[])Enum.GetValues(typeof(LineStyle))).ToList());
            EnumExtension.BindEnumListToDataSource(dgc_Scatter_MarkerShape, ((MarkerShape[])Enum.GetValues(typeof(MarkerShape))).ToList());
            po = plotOption;
            temp = po.Clone() as PlotOption;

            b_Save.Click += B_Save_Click;
            b_Discard.Click += B_Discard_Click;
            this.Load += FormPlotOption_Load;
        }

        private void Hook(bool isBind)
        {
            frmColorPicker.SelectedColor -= FrmColorPicker_SelectedColor;
            dgv_Scatter.CellClick -= Dgv_Scatter_CellClick;
            dgv_Scatter.CellValidating -= Dgv_Scatter_CellValidating;
            if (isBind)
            {
                frmColorPicker.SelectedColor += FrmColorPicker_SelectedColor;
                dgv_Scatter.CellClick += Dgv_Scatter_CellClick;
                dgv_Scatter.CellValidating += Dgv_Scatter_CellValidating;
            }
        }

        private void FrmColorPicker_SelectedColor(object sender, SelectedColorEventArgs e)
        {
            if (currentColorColumn == dgc_Scatter_LineColor.Index)
            {
                temp.ScatterOption.ScatterRows[currentColorRow].LineColor = e.SelectedColor;
            }
            else
            {
                temp.ScatterOption.ScatterRows[currentColorRow].MarkerColor = e.SelectedColor;
            }
        }

        private void Dgv_Scatter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentColorRow = e.RowIndex;
            currentColorColumn = e.ColumnIndex;
            if (e.RowIndex >= 0 && (e.ColumnIndex == dgc_Scatter_LineColor.Index || e.ColumnIndex == dgc_Scatter_MarkerColor.Index))
            {
                frmColorPicker.Show();
            }
        }

        private void Dgv_Scatter_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == dgc_Scatter_LineWidth.Index || e.ColumnIndex == dgc_Scatter_MarkerSize.Index))
            {
                if (!float.TryParse(e.FormattedValue.ToString(), out float res) || res < 0)
                {
                    MessageBox.Show("Should input a number which >= 0.", "Error", MessageBoxButtons.OK);
                    e.Cancel = true;
                }
            }
        }


        private void B_Save_Click(object sender, EventArgs e)
        {
            temp.CopyTo(po);
            this.DialogResult = DialogResult.OK;
        }

        private void B_Discard_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormPlotOption_Load(object sender, EventArgs e)
        {
            dgvbs_Scatter = new BindingSource();
            dgvbs_Scatter.DataSource = temp.ScatterOption.ScatterRows;
            dgv_Scatter.DataSource = dgvbs_Scatter;
            dgc_Scatter_Label.DataPropertyName = nameof(ScatterRowOption.Label);
            dgc_Scatter_LineStyle.DataPropertyName = nameof(ScatterRowOption.LineStyle);
            dgc_Scatter_LineColor.DataPropertyName = nameof(ScatterRowOption.LineColor);
            dgc_Scatter_LineWidth.DataPropertyName = nameof(ScatterRowOption.LineWidth);
            dgc_Scatter_MarkerShape.DataPropertyName = nameof(ScatterRowOption.MarkerShape);
            dgc_Scatter_MarkerColor.DataPropertyName = nameof(ScatterRowOption.MarkerColor);
            dgc_Scatter_MarkerSize.DataPropertyName = nameof(ScatterRowOption.MarkerSize);
            dgvbs_Scatter.ResetBindings(true);

            Hook(true);
        }
    }
}
