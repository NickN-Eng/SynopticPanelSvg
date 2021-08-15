using Svg;

namespace SynopticPanelSvg
{
    public class SPLine : SPElement
    {
        public Vector2f Start;
        public Vector2f End;

        public override AxisAlignedBox2f BoundingBox
        {
            get
            {
                return new AxisAlignedBox2f(Start, End);
            }
        }

        internal override SvgElement GetElement(Vector2f offset, bool flipYaxis)
        {
            var line = new SvgLine();
            var start = TransformPoint(Start, offset, flipYaxis);
            var end = TransformPoint(End, offset, flipYaxis);
            line.StartX = start.x;
            line.StartY = start.y;
            line.EndX = end.x;
            line.EndY = end.y;

            WriteIdDescriptions(line);
            WriteAppearance(line);
            return line;
        }

        public static SPLine FromPoints(Vector2f start, Vector2f end)
        {
            return new SPLine() { Start = start, End = end };
        }
    }
}
