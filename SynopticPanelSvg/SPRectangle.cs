using Svg;

namespace SynopticPanelSvg
{
    public class SPRectangle : SPElement
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
            var rect = new SvgRectangle();
            rect.Width = Box.Width;
            rect.Height = Box.Height;
            var origin = TransformPoint(Box.Min, offset, flipYaxis);
            rect.X = origin.x;
            rect.Y = origin.y;
            //rect.X = Box.Min.x + offset.x;
            //rect.Y = (flipYaxis ? Box.Min.y : Box.Max.y) + offset.y;

            WriteIdDescriptions(rect);
            WriteAppearance(rect);
            return rect;
        }

        public static SPRectangle FromBox(AxisAlignedBox2f box)
        {
            return new SPRectangle() { Box = box };
        }

        public static SPRectangle FromCoords(Vector2f start, Vector2f end)
        {
            return new SPRectangle() { Box = new AxisAlignedBox2f(start, end) };
        }
    }
}
