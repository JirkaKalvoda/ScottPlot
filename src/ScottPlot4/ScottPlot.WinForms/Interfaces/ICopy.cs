using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 深复制接口
    /// </summary>
    interface ICopy
    {
        /// <summary>
        /// 已有目标对象，把数据深复制到目标对象，但是有些不需要深复制
        /// </summary>
        /// <param name="target"></param>
        public void CopyTo(object target);
    }
}
