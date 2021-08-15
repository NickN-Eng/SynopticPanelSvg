using Svg;

namespace SynopticPanelSvg
{
    public class SPPolygon : SPElement
    {
        public Vector2f[] Points;
        public bool IsClosedPolygon;

        public override AxisAlignedBox2f BoundingBox
        {
            get
            {
                var box = new AxisAlignedBox2f(Points[0]);
                for (int i = 1; i < Points.Length; i++)
                {
                    box.Contain(Points[i]);
                }
                box.Expand(StrokeThickness);
                return box;
            }
        }

        internal override SvgElement GetElement(Vector2f offset, bool flipYaxis)
        {
            var points = new SvgPointCollection();
            foreach (var pt in Points)
            { 
                var newPt = TransformPoint(pt, offset, flipYaxis);
                points.Add(newPt.x);
                points.Add(newPt.y);
            }
            SvgElement elem = null;
            if (IsClosedPolygon)
                elem = new SvgPolygon() { Points = points };
            else
                elem = new SvgPolyline() { Points = points };

            WriteIdDescriptions(elem);
            WriteAppearance(elem);

            if(!IsClosedPolygon)
                elem.Fill = SvgColourServer.None;

            return elem;
        }

        public static SPPolygon FromPoints(bool isClosedPolygon, params Vector2f[] points)
        {
            return new SPPolygon() { IsClosedPolygon = isClosedPolygon, Points = points };
        }
    }
}
