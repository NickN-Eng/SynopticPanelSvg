using Svg;

namespace SynopticPanelSvg
{
    public class SPEllipse : SPElement
    {
        public AxisAlignedBox2f Box;

        public override AxisAlignedBox2f BoundingBox
        {
            get
            {
                return Box;
            }
        }

        internal override SvgElement GetElement(Vector2f offset, bool flipYaxis)
        {
            var ellipse = new SvgEllipse();
            ellipse.RadiusX = Box.Width/2;
            ellipse.RadiusY = Box.Height/2;
            var origin = TransformPoint(Box.Center, offset, flipYaxis);
            ellipse.CenterX = origin.x;
            ellipse.CenterY = origin.y;

            WriteIdDescriptions(ellipse);
            WriteAppearance(ellipse);
            return ellipse;
        }

        public static SPEllipse FromBox(AxisAlignedBox2f box)
        {
            return new SPEllipse() { Box = box };
        }
    }

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
