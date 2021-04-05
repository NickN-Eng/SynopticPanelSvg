using Svg;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynopticPanelSvg
{
    public class SPSvgDocument
    {
        private SvgDocument SvgDocument = new SvgDocument();

        public List<SPElement> Elements = new List<SPElement>();

        /// <summary>
        /// The padding around the elements
        /// </summary>
        public float SvgViewboxBuffer = 10f;

        /// <summary>
        /// Generates the svg document. Must be run before .WriteTo()
        /// </summary>
        public void Generate()
        {
            // Create a viewbox which includes every shape
            // By expanding an axis alingned box to fit every element
            var box = Elements[0].BoundingBox;
            for (int i = 1; i < Elements.Count; i++)
            {
                box.Contain(Elements[i].BoundingBox);
            }
            box.Expand(SvgViewboxBuffer);
            SvgDocument.ViewBox = new SvgViewBox(box.Min.x, box.Min.y, box.Width, box.Height);

            // Add the xml for each element to the document
            foreach (var elem in Elements)
            {
                var svgElem = elem.GetElement(Vector2f.Zero, false);
                SvgDocument.Children.Add(svgElem);
            }
        }

        /// <summary>
        /// Writes the svg document to the provided path. Generate() must have already been run.
        /// </summary>
        /// <param name="path"></param>
        public void WriteTo(string path)
        {
            SvgDocument.Write(path);
        }
    }
}
