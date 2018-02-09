Notes on working with pixels in Krita
=====================================

* To make a good solid pixel brush, activate the 'sharpness' parameter on any given pixel brush.
* To get proper lines, don't use the line tool, instead use the polyline tool.
* To get proper circles, use the circular selection with anti-aliasing unchecked. There's an odd coordinates bug at work with the regular ellipse drawing tool.
* Don't forget you can lock ratios in the geometric tools.
* Use the freehand vector tools wherever you can instead of freehand brush because the results are just much nice for outlines.
* For snapping with the move-tool, realise it's the mouse-pointer that snaps, not the upper-right corner or the like. Best snapping is thus done by starting your dragging where two grid-lines meet.
* Hide any filter layers you are using when moving or transforming. The filter layer doesn't update correctly when dragging with the move tool.
* 3.0 has arrow keys based move, grids per document and snapping. Abuse this.
* If you go to the pattern dropdown in the toolbar and choose 'custom pattern', you can set the current layer as a temporary pattern. Just hit update and 'use as pattern'. This is useful when making edges for tiles.
* If you are doing arches, use selection->expand/shrink, the offset is much more useful than a perfect ellipse slightly bigger.

Notes for LPC specifically:
---------------------------

* A perfect circle at 60degress is 2:1 for width:height.
* Same for a perfect square, obviously.
