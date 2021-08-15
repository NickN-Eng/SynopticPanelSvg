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

        public static SPEllipse FromCentreAndSize(Vector2f centre, float height, float width)
        {
            return new SPEllipse() { Box = new AxisAlignedBox2f(centre, width/2, height/2) };
        }

        public static SPEllipse FromCentreAndRadius(Vector2f centre, float radius)
        {
            return new SPEllipse() { Box = new AxisAlignedBox2f(centre, radius) };
        }
    }
}
