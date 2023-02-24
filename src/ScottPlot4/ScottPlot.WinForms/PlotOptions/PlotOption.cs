using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottPlot.WinForms
{
    public class PlotOption : ICopy, ICloneable
    {
        public ScatterOption ScatterOption { get; set; } = new ScatterOption();

        public void CopyTo(object target)
        {
            PlotOption t = target as PlotOption;
            t.ScatterOption = this.ScatterOption.Clone() as ScatterOption;
        }

        public object Clone()
        {
            PlotOption t = new PlotOption();
            this.CopyTo(t);
            return t;
        }
    }
}
