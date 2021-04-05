using Svg;

namespace SynopticPanelSvg
{
    public abstract class SPElement
    {
        /// <summary>
        /// The points which shall be accounted for when calculating the min/max rectangle
        /// </summary>
        public abstract AxisAlignedBox2f BoundingBox { get; }

        /// <summary>
        /// The stroke thickness of the shape. This will be shown by PBI Synoptic Panel if it is an unmatched area.
        /// </summary>
        public float StrokeThickness { get; set; }

        /// <summary>
        /// The stroke colour of the shape. This will be shown by PBI Synoptic Panel if it is an unmatched area.
        /// </summary>
        public SvgColourServer StrokeColor { get; set; }

        /// <summary>
        /// The fill colour of the shape. This will be ignored by PBI Synoptic Panel.
        /// </summary>
        public SvgColourServer FillColor { get; set; }

        public bool IsArea { get; set; }

        /// <summary>
        /// The Id which is used by Synotic panel to bind to data
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The Subcategory which is used by Synotic panel to bind to data
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Sets the stoke/fill colour and thickness. 
        /// Returns the same shape element for method chaining.
        /// </summary>
        public SPElement WithAppearance(float strokeThickness, SvgColourServer strokeColor, SvgColourServer fillColor = null)
        {
            StrokeThickness = strokeThickness;
            StrokeColor = strokeColor;
            FillColor = fillColor;
            return this;
        }

        /// <summary>
        /// Sets the Id and Subcategory for PBI Synoptic Panel.
        /// Returns the same shape element for method chaining.
        /// </summary>
        public SPElement AsSPArea(string id, string subcategory)
        {
            IsArea = true;
            Id = id;
            Subcategory = subcategory;
            return this;
        }

        /// <summary>
        /// Sets the element as a background element for PBI Synoptic Panel.
        /// Returns the same shape element for method chaining.
        /// </summary>
        public SPElement AsSPBackground()
        {
            IsArea = false;
            Id = "";
            Subcategory = "";
            return this;
        }

        /// <summary>
        /// Creates the xml svg element from the internal element data. 
        /// Applies transformations from the offset and flipYaxis variables for svg element position.
        /// </summary>
        /// <param name="offset">An offset coordinate to be applied to the produced shape</param>
        /// <param name="flipYaxis">If set to true, flips the y axis value.</param>
        /// <returns></returns>
        internal abstract SvgElement GetElement(Vector2f offset, bool flipYaxis);

        /// <summary>
        /// Helper method for writing Id data to a given svg element
        /// </summary>
        protected void WriteIdDescriptions(SvgElement svgElement)
        {
            svgElement.CustomAttributes["class"] = IsArea ? "area" : "area excluded";
            svgElement.ID = Id;
            svgElement.CustomAttributes["title"] = Subcategory;
        }

        /// <summary>
        /// Helper method for writing stroke and fill data to a given svg element
        /// </summary>
        protected void WriteAppearance(SvgElement svgElement)
        {
            svgElement.Stroke = StrokeColor;
            svgElement.StrokeWidth = StrokeThickness;
            svgElement.Fill = FillColor;
        }

        /// <summary>
        /// Helper method for transforming a point with an offset and flipYaxis parameters.
        /// </summary>
        protected Vector2f TransformPoint(Vector2f point, Vector2f offset, bool flipYAxis)
        {
            point += offset;
            if (flipYAxis) point.y *= -1;
            return point;
        }
    }
}
