using System;
using System.Drawing;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 颜色选择事件参数
    /// </summary>
    public class SelectedColorEventArgs : EventArgs
    {
        /// <summary>
        /// 选择的颜色
        /// </summary>
        public Color SelectedColor { get; private set; }

        public SelectedColorEventArgs(Color selectedColor)
        {
            SelectedColor = selectedColor;
        }
    }
}