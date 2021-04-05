using Svg;

namespace SynopticPanelSvg
{
    public class SPCircle : SPElement
    {
        public Vector2f Centre;

        public float Radius;

        public override AxisAlignedBox2f BoundingBox
        {
            get
            {
                return new AxisAlignedBox2f(Centre, Radius);
            }
        }

        internal override SvgElement GetElement(Vector2f offset, bool flipYaxis)
        {
            var c = new SvgCircle();
            var origin = TransformPoint(Centre, offset, flipYaxis);
            c.CenterX = origin.x;
            c.CenterY = origin.y;
            c.Radius = Radius;

            WriteIdDescriptions(c);
            WriteAppearance(c);
            return c;
        }

        public static SPCircle FromDims(Vector2f centre, float radius)
        {
            return new SPCircle() { Centre = centre, Radius = radius };
        }
    }
}
