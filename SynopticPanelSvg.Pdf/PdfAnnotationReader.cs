using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Annot;
using Svg;
using SynopticPanelSvg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynopticPanelSvg.Pdf
{
    public class PdfAnnotationReader
    {
        public SPSvgDocument ExtractFromPage(PdfPage page)
        {
            var svgdoc = new SPSvgDocument();

            foreach (var annot in page.GetAnnotations())
            {
                var dict = annot.GetPdfObject();

                //the border array consists of 3 numbers:
                //[ horiz corner radius, vert corner radius, border width ]
                var borderWidth = 1f;
                var borderArray = annot.GetBorder();
                if (borderArray != null) borderWidth = borderArray.GetAsNumber(2).FloatValue();

                var borderStyle = dict.GetAsDictionary(PdfName.BS);
                if (borderStyle != null) borderWidth = borderStyle.GetAsFloat(PdfName.W).Value;

                //Color??
                var colourArray = annot.GetColorObject();
                SvgColourServer color = SPColors.Black;
                if (colourArray != null) color = GetColour(colourArray);

                var internalColourArray = dict.GetAsArray(PdfName.IC);
                SvgColourServer internalColour = SPColors.White;
                if (internalColourArray != null) internalColour = GetColour(internalColourArray);

                //Get the id tag of the annotation
                var id = annot.GetContents()?.ToString();
                if (id == null) id = "";

                var rect = GetBox(annot.GetRectangle());

                SPElement elem = null;

                //Create the SPElement based on the annotation type
                if (annot is PdfFreeTextAnnotation fAnnot)
                {
                    elem = SPRectangle.FromBox(rect).AsSPArea(id, "").WithAppearance(borderWidth, color, internalColour);
                }
                else if (annot is PdfSquareAnnotation sAnnot)
                {
                    elem = SPRectangle.FromBox(rect).AsSPArea(id, "").WithAppearance(borderWidth, color, internalColour);
                }
                else if (annot is PdfPolyGeomAnnotation pAnnot)
                {
                    var verts = GetPoints(pAnnot.GetVertices());
                    var subtype = pAnnot.GetSubtype();
                    var isClosedPolygon = subtype == PdfName.Polygon;
                    elem = SPPolygon.FromPoints(isClosedPolygon, verts).AsSPArea(id, "").WithAppearance(borderWidth, color, internalColour);
                }
                else if (annot is PdfCircleAnnotation cAnnot)
                {
                    elem = SPEllipse.FromBox(rect).AsSPArea(id, "").WithAppearance(borderWidth, color, internalColour);
                }
                else if (annot is PdfLineAnnotation lAnnot)
                {
                    var linePoints = GetPoints(lAnnot.GetLine());
                    elem = SPLine.FromPoints(linePoints[0], linePoints[1]).AsSPArea(id, "").WithAppearance(borderWidth, color, internalColour);
                }

                if (elem != null) svgdoc.Elements.Add(elem);
            }

            return svgdoc;
        }

        /// <summary>
        /// Converts a pdf array (of a list of points) into a list of Vectors
        /// </summary>
        public Vector2f[] GetPoints(PdfArray pdfArray)
        {
            var pts = new Vector2f[pdfArray.Size() / 2];

            for (int i = 0; i < pdfArray.Size(); i += 2)
            {
                pts[i / 2] = new Vector2f(pdfArray.GetAsNumber(i).FloatValue(), pdfArray.GetAsNumber(i + 1).FloatValue());
            }

            return pts;
        }

        /// <summary>
        /// Converts a pdf array (of a list of a rectanglar box) into an axis aligned bounding box
        /// </summary>
        public AxisAlignedBox2f GetBox(PdfArray pdfArray)
        {
            var pt1 = new Vector2f(pdfArray.GetAsNumber(0).FloatValue(), pdfArray.GetAsNumber(1).FloatValue());
            var pt2 = new Vector2f(pdfArray.GetAsNumber(2).FloatValue(), pdfArray.GetAsNumber(3).FloatValue());
            var box = new AxisAlignedBox2f(pt1, pt2);
            return box;
        }

        /// <summary>
        /// Converts a pdf array (of a list of a colour) into an svg colour server
        /// </summary>
        public SvgColourServer GetColour(PdfArray pdfArray)
        {
            switch (pdfArray.Size())
            {
                case 0:
                    return SPColors.Transparent;
                case 1:
                    return SPColors.FromRgb(1, 1, 1, pdfArray.GetAsNumber(0).FloatValue());
                case 3:
                    return SPColors.FromRgb(pdfArray.GetAsNumber(0).FloatValue(), pdfArray.GetAsNumber(1).FloatValue(), pdfArray.GetAsNumber(2).FloatValue());
                case 4:
                    return SPColors.FromCMYK(pdfArray.GetAsNumber(0).FloatValue(), pdfArray.GetAsNumber(1).FloatValue(), pdfArray.GetAsNumber(2).FloatValue(), pdfArray.GetAsNumber(3).FloatValue());
                default:
                    return null;
            }
        }
    }



}
