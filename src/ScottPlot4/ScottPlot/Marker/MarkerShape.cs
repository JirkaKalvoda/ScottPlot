using System.ComponentModel;

namespace ScottPlot
{
    public enum MarkerShape
    {
        [Description("None")]
        none,

        [Description("Filled Circle")]
        filledCircle,

        [Description("Filled Square")]
        filledSquare,

        [Description("Open Circle")]
        openCircle,

        [Description("Open Square")]
        openSquare,

        [Description("Filled Diamond")]
        filledDiamond,

        [Description("Open Diamond")]
        openDiamond,

        [Description("Asterisk")]
        asterisk,

        [Description("Hash Tag")]
        hashTag,

        [Description("Cross")]
        cross,

        [Description("Eks")]
        eks,

        [Description("Vertical Bar")]
        verticalBar,

        [Description("Tri Up")]
        triUp,

        [Description("Tri Down")]
        triDown,

        [Description("Filled Triangle Up")]
        filledTriangleUp,

        [Description("Filled Triangle Down")]
        filledTriangleDown,

        [Description("Open Triangle Up")]
        openTriangleUp,

        [Description("Open Triangle Down")]
        openTriangleDown,
    }
}
