using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot.Plottable;

namespace ScottPlot.WinForms
{
    /// <summary>
    /// 折线图、信号图的设置
    /// </summary>
    public class ScatterOption : ICopy, ICloneable
    {
        public List<ScatterRowOption> ScatterRows { get; set; } = new List<ScatterRowOption>();

        public void CopyTo(object target)
        {
            ScatterOption t = target as ScatterOption;
            t.ScatterRows.Clear();
            this.ScatterRows.ForEach(s => t.ScatterRows.Add(s.Clone() as ScatterRowOption));
        }

        public object Clone()
        {
            ScatterOption t = new ScatterOption();
            this.CopyTo(t);
            return t;
        }
    }


    /// <summary>
    /// 折线图、信号图每行的设置
    /// </summary>
    public class ScatterRowOption : ICopy, ICloneable
    {
        public IPlottable ScatterObject { get; set; }

        public string Label { get; set; }

        public LineStyle LineStyle { get; set; }

        public Color LineColor { get; set; }

        public float LineWidth { get; set; }

        public MarkerShape MarkerShape { get; set; }

        public Color MarkerColor { get; set; }

        public float MarkerSize { get; set; }

        public void CopyTo(object target)
        {
            ScatterRowOption t = target as ScatterRowOption;
            t.ScatterObject = this.ScatterObject;
            t.Label = this.Label;
            t.LineStyle = this.LineStyle;
            t.LineColor = this.LineColor;
            t.LineWidth = this.LineWidth;
            t.MarkerShape = this.MarkerShape;
            t.MarkerColor = this.MarkerColor;
            t.MarkerSize = this.MarkerSize;
        }

        public object Clone()
        {
            ScatterRowOption t = new ScatterRowOption();
            this.CopyTo(t);
            return t;
        }
    }
}
