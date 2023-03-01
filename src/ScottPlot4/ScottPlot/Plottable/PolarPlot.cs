using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot.Drawing;

namespace ScottPlot.Plottable
{
    /// <summary>
    /// 极坐标系折线
    /// </summary>
    public class PolarPlot : ScatterPlot
    {
        /// <summary>
        /// 极坐标系折线
        /// </summary>
        /// <param name="radii">半径，负数自动变正数加半圈</param>
        /// <param name="angles">角，弧度</param>
        public PolarPlot(double[] radii, double[] angles)
            : base(null, null, null, null)
        {
            Xs = new double[radii.Length];
            Ys = new double[angles.Length];
            // 半径负数自动变正数加半圈
            // 处理数组长度不一样的情况，等后面有专门的地方判断
            for (int i = 0; i < Math.Min(radii.Length, angles.Length); ++i)
            {
                if (radii[i] < 0)
                {
                    Xs[i] = -radii[i];
                    Ys[i] = angles[i] + Math.PI / 2;
                }
                else
                {
                    Xs[i] = radii[i];
                    Ys[i] = angles[i];
                }
            }

            for (int j = Math.Min(radii.Length, angles.Length); j < radii.Length; ++j)
            {
                Xs[j] = radii[j];
            }
            for (int j = Math.Min(radii.Length, angles.Length); j < angles.Length; ++j)
            {
                Ys[j] = angles[j];
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
                int from = MinRenderIndex ?? 0;
                int to = MaxRenderIndex ?? (Xs.Length - 1);
                PointF[] points = new PointF[to - from + 1];
                for (int i = from; i <= to; i++)
                {
                    float radius = dims.GetPixelRoundWidth(Xs[i] + OffsetX);
                    float x = dims.PxCenterX + radius * (float) Math.Cos(Ys[i] + OffsetY);
                    float y = dims.PxCenterY - radius * (float) Math.Sin(Ys[i] + OffsetY);     // 从上到下所以是-
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
