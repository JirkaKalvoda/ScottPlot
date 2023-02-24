using System.ComponentModel;

namespace ScottPlot
{
    public enum LineStyle
    {
        [Description("None")]
        None,

        [Description("Solid")]
        Solid,

        [Description("Dash")]
        Dash,

        [Description("Dash Dot")]
        DashDot,

        [Description("Dash Dot Dot")]
        DashDotDot,

        [Description("Dot")]
        Dot
    }
}
