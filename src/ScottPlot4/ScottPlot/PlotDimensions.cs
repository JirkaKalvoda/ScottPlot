using System;
using System.Drawing;

namespace ScottPlot
{
    /// <summary>
    /// PlotDimensions supplies figure dimensions and pixel/coordinate lookup methods for a single 2D plane
    /// </summary>
    public class PlotDimensions
    {
        // plot dimensions
        public readonly float Width;
        public readonly float Height;
        //public readonly float DataWidth;
        //public readonly float DataHeight;
        //public readonly float DataOffsetX;
        //public readonly float DataOffsetY;
        public float DataWidth;
        public float DataHeight;
        public float DataOffsetX;
        public float DataOffsetY;

        // rendering options
        public readonly double ScaleFactor;

        // axis limits
        public readonly double XMin;
        public readonly double XMax;
        public readonly double YMin;
        public readonly double YMax;
        public readonly double XSpan;
        public readonly double YSpan;
        public readonly double XCenter;
        public readonly double YCenter;
        public readonly AxisLimits AxisLimits;

        // pixel/coordinate conversions
        public readonly double PxPerUnitX;
        public readonly double PxPerUnitY;
        public readonly double UnitsPerPxX;
        public readonly double UnitsPerPxY;

        /// <summary>
        /// 防止弧度值显示在画布外所以留了边距
        /// </summary>
        public const float Padding = 25;

        /// <summary>
        /// 极坐标系像素半径
        /// </summary>
        public float PxRadius { get => Math.Min(DataWidth, DataHeight) / 2 - Padding; }

        /// <summary>
        /// 极坐标图中心像素横坐标像素
        /// </summary>
        public float PxCenterX { get => DataOffsetX + DataWidth / 2; }

        /// <summary>
        /// 极坐标图中心像素纵坐标像素
        /// </summary>
        public float PxCenterY { get => DataOffsetY + DataHeight / 2; }

        /// <summary>
        /// 极坐标系每2个major刻度之间的宽度的像素，X值实际是半径
        /// </summary>
        public double PxPerUnitRound { get => PxRadius / XMax; }

        /// <summary>
        /// 极坐标每个圆相对于中心的半径（像素）
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float GetPixelRoundWidth(double position) => (float)(position * PxPerUnitRound);
        
        public Pixel GetPixel(Coordinate coordinate) => new Pixel(GetPixelX(coordinate.X), GetPixelY(coordinate.Y));
        public float GetPixelX(double position) => (float)(DataOffsetX + ((position - XMin) * PxPerUnitX));
        public float GetPixelY(double position) => (float)(DataOffsetY + ((YMax - position) * PxPerUnitY));
        public Coordinate GetCoordinate(Pixel pixel) => new(GetCoordinateX(pixel.X), GetCoordinateY(pixel.Y));
        public Coordinate GetCoordinate(float xPixel, float yPixel) => new(GetCoordinateX(xPixel), GetCoordinateY(yPixel));
        public double GetCoordinateX(float pixel) => (pixel - DataOffsetX) / PxPerUnitX + XMin;
        public double GetCoordinateY(float pixel) => YMax - (pixel - DataOffsetY) / PxPerUnitY;

        public PlotDimensions(SizeF figureSize, SizeF dataSize, PointF dataOffset, AxisLimits axisLimits, double scaleFactor)
        {
            (Width, Height) = (figureSize.Width, figureSize.Height);
            (DataWidth, DataHeight) = (dataSize.Width, dataSize.Height);
            (DataOffsetX, DataOffsetY) = (dataOffset.X, dataOffset.Y);
            AxisLimits = axisLimits;
            (XMin, XMax, YMin, YMax) = (axisLimits.XMin, axisLimits.XMax, axisLimits.YMin, axisLimits.YMax);
            (XSpan, YSpan) = (XMax - XMin, YMax - YMin);
            (XCenter, YCenter) = ((XMin + XMax) / 2, (YMin + YMax) / 2);
            (PxPerUnitX, PxPerUnitY) = (DataWidth / XSpan, DataHeight / YSpan);
            (UnitsPerPxX, UnitsPerPxY) = (XSpan / DataWidth, YSpan / DataHeight);
            ScaleFactor = scaleFactor;
        }

        public override string ToString() =>
            $"Dimensions for figure ({Width}x{Height}), " +
            $"data area ({DataWidth}x{DataHeight}), " +
            $"and axes ({XMin}, {XMax}, {YMin}, {YMax})";
    }
}
