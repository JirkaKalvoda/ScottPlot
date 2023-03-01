using System;
using System.Collections.Generic;
using System.Text;

namespace ScottPlot.Renderable
{
    public enum Edge
    {
        Left,
        Right,
        Bottom,
        Top,

        /// <summary>
        /// 最大半径的环
        /// </summary>
        Circle,

        /// <summary>
        /// 径向轴
        /// </summary>
        Ray
    };

}
