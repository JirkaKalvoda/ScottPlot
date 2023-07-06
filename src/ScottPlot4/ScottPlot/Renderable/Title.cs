using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot.Drawing;
using Font = ScottPlot.Drawing.Font;

namespace ScottPlot.Renderable
{
    public class Title : IRenderable
    {
        public string Name { get; set; }

        public float FontSize { get; set; } = 20;

        public string FontName { get; set; } = "Calibri";

        public bool IsItalic { get; set; } = true;

        public bool IsBold { get; set; } = true;

        public Color Color { get; set; } = Color.Black;

        public float Height { get; private set; }

        public bool IsVisible { get; set; } = false;

        public void Render(PlotDimensions dims, Bitmap bmp, bool lowQuality = false)
        {
            if (!IsVisible || string.IsNullOrWhiteSpace(Name))
            {
                return;
            }
            using (Graphics gfx = GDI.Graphics(bmp, dims, lowQuality, false))
            using (System.Drawing.Font font = new System.Drawing.Font(FontName, FontSize, 
                (IsBold ? FontStyle.Bold : FontStyle.Regular) | (IsItalic ? FontStyle.Italic : FontStyle.Regular)))
            using (SolidBrush brush = new SolidBrush(Color))
            {
                SizeF sizef = gfx.MeasureString(Name, font);
                gfx.DrawString(Name, font, brush, (dims.Width - sizef.Width) / 2, 0);
                Height = sizef.Height;
            }
        }
    }
}
