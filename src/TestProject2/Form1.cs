using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Plottable;

namespace TestProject2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            FormsPlot fp;
            //fp = InsertScatter();
            fp = InsertPolar();
            Controls.Add(fp);
            Padding = new Padding(0, 0, 0, 0);
            fp.Dock = DockStyle.Fill;
        }

        private FormsPlot InsertScatter()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = { 1, 2, 3 };
            List<double[]> y = new List<double[]>();
            y.Add(new double[] { 2, 3, 4 });
            y.Add(new double[] { 3, 3, 2 });
            string[] yname = { "a", "b" };
            for (int i = 0; i < y.Count; ++i)
            {
                ScatterPlot sp = fp.Plot.AddScatter(x, y[i]);
                sp.Label = yname[i];
            }

            //PiePlot pie = fp.Plot.AddPie(x);   // 不报错但是显示不正常

            fp.Plot.XAxis.Label("xx");
            fp.Plot.YAxis.Label("yy");
            fp.Plot.XAxis2.Ticks(true);
            fp.Plot.XAxis2.Label("xx2");     // 无效，因为标题也是用X2轴名
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerCenter);
            fp.Plot.AxisAuto();
            fp.Refresh();
            
            return fp;
        }

        private FormsPlot InsertPolar()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = { 2, 3, 4 };
            double[] y = { 1, Math.PI / 2, Math.PI };
            double[] y2 = { -1, 1, -2 };

            PolarPlot pp = fp.Plot.AddPolar(x, y);
            pp.Label = "polar";
            PolarPlot pp2 = fp.Plot.AddPolar(x, y2);
            pp2.Label = "polar2";

            //fp.Plot.XAxis
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerLeft);
            fp.Plot.AxisAuto();
            fp.Refresh();
            
            return fp;
        }
    }
}
