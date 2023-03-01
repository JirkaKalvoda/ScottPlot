using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScottPlot;
using ScottPlot.Plottable;
using Orientation = ScottPlot.Orientation;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLegend()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = {1, 2, 3};
            List<double[]> y = new List<double[]>();
            y.Add(new double[] { 2, 3, 4});
            y.Add(new double[] { 3, 3, 2});
            string[] yname = {"a", "b"};
            for (int i = 0; i < y.Count; ++i)
            {
                SignalPlotXY sp = fp.Plot.AddSignalXY(x, y[i]);
                sp.Label = yname[i];
            }

            fp.Plot.XAxis.Label("xx");
            fp.Plot.YAxis.Label("yy");
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerCenter);
            fp.Plot.AxisAuto();
            fp.Size = new Size(400, 600);
            fp.Refresh();

            Form f = new Form();
            f.Controls.Add(fp);
            f.Padding = new Padding(0, 0, 0, 0);
            f.ShowDialog();
        }


        [TestMethod]
        public void TestScatterStep()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = { 1, 3};
            List<double[]> y = new List<double[]>();
            y.Add(new double[] { 2, 5 });
            //y.Add(new double[] { 3, 3, 2 });
            string[] yname = { "a", "b" };
            for (int i = 0; i < y.Count; ++i)
            {
                ScatterPlot sp = fp.Plot.AddScatterStep(x, y[i]);
                sp.Label = yname[i];
            }

            fp.Plot.XAxis.Label("xx");
            fp.Plot.YAxis.Label("yy");
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerRight);
            fp.Plot.AxisAuto();
            //fp.Size = new Size(400, 600);

            fp.Refresh();

            Form f = new Form();
            f.Controls.Add(fp);
            f.Padding = new Padding(0, 0, 0, 0);
            f.ShowDialog();
        }


        [TestMethod]
        public void TestCleveland()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = { 1, 1, 1, 1 };
            double[] y = { 1, 4, 7, 10 };
            double[] y2 = { 3, 6, 9, 12 };
            ClevelandDotPlot cp = fp.Plot.AddClevelandDot(y, y2, x);
            cp.Orientation = Orientation.Horizontal;
            cp.SetPoint1Style(color: Color.Green, markerShape: MarkerShape.none, label: "line1");
            cp.SetPoint2Style(color: Color.Green, markerShape: MarkerShape.none);
            cp.StemColor = Color.Red;

            VLine vl = fp.Plot.AddVerticalLine(1, color: Color.Red);
            vl.Min = 0;
            vl.Max = 1.5;

            fp.Plot.XAxis.Label("xx");
            fp.Plot.YAxis.Label("yy");
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerCenter);
            fp.Plot.AxisAuto();
            //fp.Size = new Size(400, 600);

            fp.Refresh();

            Form f = new Form();
            f.Controls.Add(fp);
            //f.Padding = new Padding(0, 0, 0, 0);
            f.ShowDialog();
        }


        [TestMethod]
        public void TestErrorBar()
        {
            FormsPlot fp = new FormsPlot();
            double[] x = { 1, 5, 9, 13 };
            double[] y = {1, 1, 1, 1};
            double[] y2 = {3, 3, 3, 3};
            double[] errNeg = {0, 0, 0, 0};
            double[] errPos = {1, 1, 1, 1};

            ScatterPlot sp = fp.Plot.AddScatterPoints(x, y);
            sp.MarkerSize = 12;
            sp.MarkerShape = MarkerShape.verticalBar;
            sp.Color = Color.Orange;
            //sp.LineWidth = 0;
            //sp.LineStyle = LineStyle.None;
            ScatterPlot sp2 = fp.Plot.AddScatterPoints(x, y2);
            sp2.MarkerSize = 12;
            sp2.MarkerShape = MarkerShape.verticalBar;
            sp2.Color = Color.Blue;
            sp2.Label = "sunlight";
            sp2.YAxisIndex = 1;
            ErrorBar eb = fp.Plot.AddErrorBars(x, y, errPos, errNeg, null, null, color:Color.Orange, markerSize: 10);
            eb.CapSize = 8;
            ErrorBar eb2 = fp.Plot.AddErrorBars(x, y2, errPos, errNeg, null, null, color: Color.Blue, markerSize: 10);
            eb.CapSize = 8;
            //eb.MarkerShape = MarkerShape.triDown;
            fp.Plot.XAxis.Label("x");
            fp.Plot.YAxis.Label("y");
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerCenter);
            fp.Plot.AxisAuto();
            fp.Plot.YAxis2.Ticks(true);
            Crosshair ch = fp.Plot.AddCrosshair(0, 0);
            ch.Color = Color.Gray;
            Crosshair ch2 = fp.Plot.AddCrosshair(0, 0);
            ch2.Color = Color.Gray;
            ch2.YAxisIndex = 1;
            ch2.HorizontalLine.PositionLabelOppositeAxis = true;
            ch2.VerticalLine.IsVisible = false;
            
            fp.MouseMove += (o, e) =>
            {
                (double cx, double cy) = fp.GetMouseCoordinates(0, 0);
                ch.X = cx;
                ch.Y = cy;
                (double cx2, double cy2) = fp.GetMouseCoordinates(0, 1);
                ch2.X = cx2;
                ch2.Y = cy2;

                fp.Refresh(lowQuality:true, skipIfCurrentlyRendering: true);
            };
            // 坐标轴、标题背景色改成不透明
            fp.Plot.Style(figureBackground:Color.White);
            //fp.Refresh();
            Form f = new Form();
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Size = new Size(800, 600);
            f.Controls.Add(fp);
            fp.Dock = DockStyle.Fill;
            f.ShowDialog();
        }

        [TestMethod]
        public void TestScatter()
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

            fp.Plot.XAxis.Label("xx");
            fp.Plot.YAxis.Label("yy");
            fp.Plot.Title("title");
            fp.Plot.Legend(true, Alignment.LowerCenter);
            fp.Plot.AxisAuto();
            fp.Size = new Size(400, 400);
            fp.Refresh();
            
            Form f = new Form();
            f.Size = new Size(450, 450);
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Controls.Add(fp);
            f.Padding = new Padding(0, 0, 0, 0);
            f.ShowDialog();
        }
    }
}
