using ScottPlot.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace ScottPlot.Plottable
{
    /// <summary>
    /// 离散点部分连线的折线，分段函数
    /// </summary>
    public class ScatterPlotPiecewise : ScatterPlot
    {
        /// <summary>
        /// 需要连线的部分的起止坐标，偶数格存起始下标，奇数格存终止下标，格数必须>=2的偶数，必须是所有下标的划分
        /// </summary>
        public uint[] BoundaryPair { get; protected set; }
        
        public ScatterPlotPiecewise(double[] xs, double[] ys, uint[] boundaryPair, double[] errorX = null, double[] errorY = null)
            : base(xs, ys, errorX, errorY)
        {
            Xs = xs;
            Ys = ys;
            BoundaryPair = boundaryPair;
            XError = errorX;
            YError = errorY;
        }


        public override void ValidateData(bool deep = false)
        {
            base.ValidateData(deep);
            if (BoundaryPair == null || BoundaryPair.Length < 2 || BoundaryPair.Length % 2 != 0)
            {
                throw new Exception("Boundary array must have at least 1 pair of indices.");
            }
            if (BoundaryPair[0] != 0 || BoundaryPair.Last() != Xs.Length - 1)
            {
                throw new Exception("Boundary array should contains all indices.");
            }
            for (int i = 0; i < BoundaryPair.Length - 1; i += 2)
            {
                if (BoundaryPair[i + 1] < BoundaryPair[i])
                {
                    throw new Exception("Boundary array should be ascent.");
                }
                if (i >= 2 && BoundaryPair[i] != BoundaryPair[i - 1] + 1)
                {
                    throw new Exception("Boundary array should exactly divide all indices.");
                }
            }
        }


        public override void Render(PlotDimensions dims, Bitmap bmp, bool lowQuality = false)
        {
            if (IsVisible == false)
                return;

            using (var gfx = GDI.Graphics(bmp, dims, lowQuality))
            using (var penLine = GDI.Pen(LineColor, LineWidth, LineStyle, true))
            using (var penLineError = GDI.Pen(LineColor, ErrorLineWidth, LineStyle.Solid, true))
            {
                for (int b = 0; b < BoundaryPair.Length - 1; b += 2)
                {
                    int from = (int) BoundaryPair[b];
                    int to = (int) BoundaryPair[b + 1];
                    PointF[] points = new PointF[to - from + 1];
                    for (int i = from; i <= to; i++)
                    {
                        float x = dims.GetPixelX(Xs[i] + OffsetX);
                        float y = dims.GetPixelY(Ys[i] + OffsetY);
                        if (float.IsNaN(x) || float.IsNaN(y))
                            throw new NotImplementedException("Data must not contain NaN");
                        points[i - from] = new PointF(x, y);
                    }

                    if (YError != null)
                    {
                        for (int i = 0; i < points.Count(); i++)
                        {
                            double yWithOffset = Ys[i] + OffsetY;
                            float yBot = dims.GetPixelY(yWithOffset - YError[i + from]);
                            float yTop = dims.GetPixelY(yWithOffset + YError[i + from]);
                            gfx.DrawLine(penLineError, points[i].X, yBot, points[i].X, yTop);
                            gfx.DrawLine(penLineError, points[i].X - ErrorCapSize, yBot, points[i].X + ErrorCapSize, yBot);
                            gfx.DrawLine(penLineError, points[i].X - ErrorCapSize, yTop, points[i].X + ErrorCapSize, yTop);
                        }
                    }

                    if (XError != null)
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            double xWithOffset = Xs[i] + OffsetX;
                            float xLeft = dims.GetPixelX(xWithOffset - XError[i + from]);
                            float xRight = dims.GetPixelX(xWithOffset + XError[i + from]);
                            gfx.DrawLine(penLineError, xLeft, points[i].Y, xRight, points[i].Y);
                            gfx.DrawLine(penLineError, xLeft, points[i].Y - ErrorCapSize, xLeft, points[i].Y + ErrorCapSize);
                            gfx.DrawLine(penLineError, xRight, points[i].Y - ErrorCapSize, xRight, points[i].Y + ErrorCapSize);
                        }
                    }

                    // draw the lines connecting points
                    if (LineWidth > 0 && points.Length > 1 && LineStyle != LineStyle.None)
                    {
                        if (StepDisplay)
                        {
                            PointF[] pointsStep = new PointF[points.Length * 2 - 1];
                            for (int i = 0; i < points.Length - 1; i++)
                            {
                                pointsStep[i * 2] = points[i];
                                pointsStep[i * 2 + 1] = new PointF(points[i + 1].X, points[i].Y);
                            }
                            pointsStep[pointsStep.Length - 1] = points[points.Length - 1];
                            gfx.DrawLines(penLine, pointsStep);
                        }
                        else
                        {
                            gfx.DrawLines(penLine, points);
                        }
                    }

                    // draw a marker at each point
                    if ((MarkerSize > 0) && (MarkerShape != MarkerShape.none))
                    {
                        MarkerTools.DrawMarkers(gfx, points, MarkerShape, MarkerSize, MarkerColor, MarkerLineWidth);
                    }
                }
            }
        }
    }
}
