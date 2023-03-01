using ScottPlot.Drawing;
using ScottPlot.Ticks;
using System;
using System.Drawing;
using System.Linq;

namespace ScottPlot.Renderable
{
    static class AxisTicksRender
    {
        private static bool EdgeIsVertical(Edge edge) => (edge == Edge.Left || edge == Edge.Right);

        private static bool EdgeIsHorizontal(Edge edge) => (edge == Edge.Top || edge == Edge.Bottom);

        public static void RenderGridLines(PlotDimensions dims, Graphics gfx, double[] positions,
            LineStyle gridLineStyle, Color gridLineColor, float gridLineWidth, Edge edge)
        {
            if (edge != Edge.Ray && edge != Edge.Circle && (positions is null || positions.Length == 0 || gridLineStyle == LineStyle.None))
                return;

            // don't draw grid lines on the last pixel to prevent drawing over the data frame
            float xEdgeLeft = dims.DataOffsetX + 1;
            float xEdgeRight = dims.DataOffsetX + dims.DataWidth - 1;
            float yEdgeTop = dims.DataOffsetY + 1;
            float yEdgeBottom = dims.DataOffsetY + dims.DataHeight - 1;

            if (EdgeIsVertical(edge))
            {
                float x = (edge == Edge.Left) ? dims.DataOffsetX : dims.DataOffsetX + dims.DataWidth;
                float x2 = (edge == Edge.Left) ? dims.DataOffsetX + dims.DataWidth : dims.DataOffsetX;
                var ys = positions.Select(i => dims.GetPixelY(i)).Where(y => yEdgeTop < y && y < yEdgeBottom);
                if (gridLineStyle != LineStyle.None)
                    using (var pen = GDI.Pen(gridLineColor, gridLineWidth, gridLineStyle))
                        foreach (float y in ys)
                            gfx.DrawLine(pen, x, y, x2, y);
            }

            if (EdgeIsHorizontal(edge))
            {
                float y = (edge == Edge.Top) ? dims.DataOffsetY : dims.DataOffsetY + dims.DataHeight;
                float y2 = (edge == Edge.Top) ? dims.DataOffsetY + dims.DataHeight : dims.DataOffsetY;
                var xs = positions.Select(i => dims.GetPixelX(i)).Where(x => xEdgeLeft < x && x < xEdgeRight);
                if (gridLineStyle != LineStyle.None)
                    using (var pen = GDI.Pen(gridLineColor, gridLineWidth, gridLineStyle))
                        foreach (float x in xs)
                            gfx.DrawLine(pen, x, y, x, y2);
            }

            // 辐射线的数量是固定的
            if (edge == Edge.Circle)
            {
                if (gridLineStyle != LineStyle.None)
                {
                    using (var pen = GDI.Pen(gridLineColor, gridLineWidth, gridLineStyle))
                    {
                        for (int i = 1; i <= 7; ++i)
                        {
                            gfx.DrawLine(pen, dims.PxCenterX, dims.PxCenterY, dims.PxCenterX + dims.PxRadius * (float)Math.Cos(Math.PI / 4 * i), dims.PxCenterY - dims.PxRadius * (float)Math.Sin(Math.PI / 4 * i));
                        }
                    }
                }
            }

            if (edge == Edge.Ray)
            {
                // 这个xs是不同环半径
                var xs = positions.Select(i => dims.GetPixelRoundWidth(i)).Where(x => 0 < x && x < dims.PxRadius);
                if (gridLineStyle != LineStyle.None)
                    using (var pen = GDI.Pen(gridLineColor, gridLineWidth, gridLineStyle))
                        foreach (float x in xs)
                            gfx.DrawEllipse(pen, dims.PxCenterX - x, dims.PxCenterY - x, 2 * x, 2 * x);
            }

        }

        public static void RenderTickMarks(PlotDimensions dims, Graphics gfx, double[] positions, float tickLength, Color tickColor, Edge edge, float pixelOffset)
        {
            if (positions is null || positions.Length == 0)
                return;

            if (EdgeIsVertical(edge))
            {
                float x = (edge == Edge.Left) ? dims.DataOffsetX - pixelOffset : dims.DataOffsetX + dims.DataWidth + pixelOffset;
                float tickDelta = (edge == Edge.Left) ? -tickLength : tickLength;

                var ys = positions.Select(i => dims.GetPixelY(i));
                using (var pen = GDI.Pen(tickColor))
                    foreach (float y in ys)
                        gfx.DrawLine(pen, x, y, x + tickDelta, y);
            }

            if (EdgeIsHorizontal(edge))
            {
                float y = (edge == Edge.Top) ? dims.DataOffsetY - pixelOffset : dims.DataOffsetY + dims.DataHeight + pixelOffset;
                float tickDelta = (edge == Edge.Top) ? -tickLength : tickLength;

                var xs = positions.Select(i => dims.GetPixelX(i));
                using (var pen = GDI.Pen(tickColor))
                    foreach (float x in xs)
                        gfx.DrawLine(pen, x, y, x, y + tickDelta);
            }

            if (edge == Edge.Ray)
            {
                var xs = positions.Select(i => dims.GetPixelRoundWidth(i));
                using (var pen = GDI.Pen(tickColor))
                    foreach (float x in xs)
                        if (x >= 0 && x <= dims.PxRadius)
                            gfx.DrawLine(pen, dims.PxCenterX + x, dims.PxCenterY, dims.PxCenterX + x, dims.PxCenterY - tickLength);
            }

        }

        public static void RenderTickLabels(PlotDimensions dims, Graphics gfx, TickCollection tc, Drawing.Font tickFont, Edge edge, float rotation, bool rulerMode, float PixelOffset, float MajorTickLength, float MinorTickLength)
        {
            if (edge != Edge.Circle && (tc.tickLabels is null || tc.tickLabels.Length == 0))
                return;

            using var font = GDI.Font(tickFont);
            using var brush = GDI.Brush(tickFont.Color);
            using var sf = GDI.StringFormat();

            Tick[] visibleMajorTicks = tc.GetVisibleMajorTicks(dims);

            switch (edge)
            {
                case Edge.Bottom:
                    for (int i = 0; i < visibleMajorTicks.Length; i++)
                    {
                        float x = dims.GetPixelX(visibleMajorTicks[i].Position);
                        float y = dims.DataOffsetY + dims.DataHeight + MajorTickLength + PixelOffset;

                        gfx.TranslateTransform(x, y);
                        gfx.RotateTransform(-rotation);
                        sf.Alignment = rotation == 0 ? StringAlignment.Center : StringAlignment.Far;
                        if (rulerMode) sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = rotation == 0 ? StringAlignment.Near : StringAlignment.Center;
                        gfx.DrawString(visibleMajorTicks[i].Label, font, brush, 0, 0, sf);
                        GDI.ResetTransformPreservingScale(gfx, dims);
                    }
                    break;

                case Edge.Top:
                    for (int i = 0; i < visibleMajorTicks.Length; i++)
                    {
                        float x = dims.GetPixelX(visibleMajorTicks[i].Position);
                        float y = dims.DataOffsetY - MajorTickLength - PixelOffset;

                        gfx.TranslateTransform(x, y);
                        gfx.RotateTransform(-rotation);
                        sf.Alignment = rotation == 0 ? StringAlignment.Center : StringAlignment.Near;
                        if (rulerMode) sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = rotation == 0 ? StringAlignment.Far : StringAlignment.Center;
                        gfx.DrawString(visibleMajorTicks[i].Label, font, brush, 0, 0, sf);
                        GDI.ResetTransformPreservingScale(gfx, dims);
                    }
                    break;

                case Edge.Left:
                    for (int i = 0; i < visibleMajorTicks.Length; i++)
                    {
                        float x = dims.DataOffsetX - PixelOffset - MajorTickLength;
                        float y = dims.GetPixelY(visibleMajorTicks[i].Position);

                        gfx.TranslateTransform(x, y);
                        gfx.RotateTransform(-rotation);
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = rulerMode ? StringAlignment.Far : StringAlignment.Center;
                        if (rotation == 90)
                        {
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Far;
                        }
                        gfx.DrawString(visibleMajorTicks[i].Label, font, brush, 0, 0, sf);
                        GDI.ResetTransformPreservingScale(gfx, dims);
                    }
                    break;

                case Edge.Right:
                    for (int i = 0; i < visibleMajorTicks.Length; i++)
                    {
                        float x = dims.DataOffsetX + PixelOffset + MajorTickLength + dims.DataWidth;
                        float y = dims.GetPixelY(visibleMajorTicks[i].Position);

                        gfx.TranslateTransform(x, y);
                        gfx.RotateTransform(-rotation);
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = rulerMode ? StringAlignment.Far : StringAlignment.Center;
                        if (rotation == 90)
                        {
                            sf.Alignment = StringAlignment.Center;
                            sf.LineAlignment = StringAlignment.Near;
                        }
                        gfx.DrawString(visibleMajorTicks[i].Label, font, brush, 0, 0, sf);
                        GDI.ResetTransformPreservingScale(gfx, dims);
                    }
                    break;

                case Edge.Ray:
                    for (int i = 0; i < visibleMajorTicks.Length; i++)
                    {
                        // 半径只显示正的
                        if (visibleMajorTicks[i].Position >= 0)
                        {
                            float x = dims.PxCenterX + dims.GetPixelRoundWidth(visibleMajorTicks[i].Position);
                            float y = dims.PxCenterY;

                            gfx.TranslateTransform(x, y);
                            gfx.RotateTransform(-rotation);
                            sf.Alignment = rotation == 0 ? StringAlignment.Center : StringAlignment.Far;
                            if (rulerMode)
                                sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = rotation == 0 ? StringAlignment.Near : StringAlignment.Center;
                            gfx.DrawString(visibleMajorTicks[i].Label, font, brush, 0, 0, sf);
                            GDI.ResetTransformPreservingScale(gfx, dims);
                        }
                    }
                    break;

                case Edge.Circle:
                    string[] labels =
                    {
                        "0.25π",
                        "0.5π",
                        "0.75π",
                        "π",
                        "1.25π",
                        "1.5π",
                        "1.75π"
                    };
                    float[,] pixels = new float[7, 2];
                    for (int i = 0; i < 3; ++i)
                    {
                        pixels[i, 0] = dims.PxCenterX + (dims.PxRadius + PlotDimensions.Padding) * (float)Math.Cos(Math.PI / 4 * (i + 1));
                        pixels[i, 1] = dims.PxCenterY - (dims.PxRadius + PlotDimensions.Padding) * (float)Math.Sin(Math.PI / 4 * (i + 1));
                    }
                    pixels[3, 0] = dims.PxCenterX - dims.PxRadius - PlotDimensions.Padding / 2;
                    pixels[3, 1] = dims.PxCenterY - PlotDimensions.Padding / 2;
                    for (int i = 4; i < 7; ++i)
                    {
                        pixels[i, 0] = dims.PxCenterX + (dims.PxRadius + PlotDimensions.Padding) * (float)Math.Cos(Math.PI / 4 * (i + 1));
                        pixels[i, 1] = dims.PxCenterY - (dims.PxRadius) * (float)Math.Sin(Math.PI / 4 * (i + 1));
                    }
                    for (int i = 0; i < 7; ++i)
                    {
                        float x = pixels[i, 0];
                        float y = pixels[i, 1];
                        gfx.TranslateTransform(x, y);
                        gfx.RotateTransform(-rotation);
                        sf.Alignment = rotation == 0 ? StringAlignment.Center : StringAlignment.Far;
                        if (rulerMode)
                            sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = rotation == 0 ? StringAlignment.Near : StringAlignment.Center;
                        gfx.DrawString(labels[i], font, brush, 0, 0, sf);
                        GDI.ResetTransformPreservingScale(gfx, dims);
                    }
                    break;

                default:
                    throw new NotImplementedException($"unsupported edge type {edge}");
            }

            if (!string.IsNullOrWhiteSpace(tc.CornerLabel))
            {
                switch (edge)
                {
                    case Edge.Left:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        gfx.DrawString(s: "\n" + tc.CornerLabel,
                            x: dims.DataOffsetX - MajorTickLength - PixelOffset,
                            y: dims.DataOffsetY,
                            font: font, brush: brush, format: sf);
                        break;

                    case Edge.Bottom:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        gfx.DrawString(s: "\n" + tc.CornerLabel,
                            x: dims.DataOffsetX + dims.DataWidth,
                            y: dims.DataOffsetY + dims.DataHeight + MajorTickLength + PixelOffset,
                            font: font, brush: brush, format: sf);
                        break;

                    case Edge.Right:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Far;
                        gfx.DrawString(s: "\n" + tc.CornerLabel,
                            x: dims.DataOffsetX + dims.DataWidth + MajorTickLength + PixelOffset,
                            y: dims.DataOffsetY,
                            font: font, brush: brush, format: sf);
                        break;

                    case Edge.Top:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        gfx.DrawString(s: tc.CornerLabel + "\n\n",
                            x: dims.DataOffsetX + dims.DataWidth,
                            y: dims.DataOffsetY - MajorTickLength - PixelOffset,
                            font: font, brush: brush, format: sf);
                        break;

                    case Edge.Ray:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        gfx.DrawString(s: "\n" + tc.CornerLabel,
                            x: dims.PxCenterX + dims.PxRadius,
                            y: dims.PxCenterY,
                            font: font, brush: brush, format: sf);
                        break;

                    case Edge.Circle:
                        break;

                    default:
                        throw new NotImplementedException($"unsupported edge type {edge}");
                }
            }
        }
    }
}
