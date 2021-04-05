# SynopticPanelSvg
 Library for creating svgs compatible with Power BI's Synoptic Panel programmatically.

# SynopticPanelSvg API
- The viewbox of the svg is automatically adjusted to fit every annotation. Future work to allow manual adjustement of svg viewbox.

# Pdf to Synoptic Panel conversion
The PdfConverterApp contains an example of how SynopticPanelSvg.Pdf library can be used to convert pdf annotations to svg.

Synoptic panel requires the "Id" feild for databinding. Within a pdf annotation, this converter takes this from the "contents" of each annotation. This can be set by pdf readers like bluebeam in the annotation properties.

# Todo
- Grasshopper plugin
- Dynamo plugin