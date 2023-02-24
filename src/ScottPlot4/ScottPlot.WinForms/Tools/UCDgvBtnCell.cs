using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlot.WinForms
{
    public class UCDgvBtnCell : DataGridViewTextBoxCell
    {
        public UCDgvBtnCell()
        {
        }

        protected override bool SetValue(int rowIndex, object value)
        {
            if (value is Color)
            {
                this.Style.BackColor = (Color)value;
                return true;
            }
            else
                return base.SetValue(rowIndex, value);
        }

        public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            formattedValue = "";
            if(value is Color)
            {
                cellStyle.BackColor = (Color)value;
                cellStyle.SelectionBackColor = (Color)value;
                cellStyle.ForeColor = (Color)value;
            }
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
}
