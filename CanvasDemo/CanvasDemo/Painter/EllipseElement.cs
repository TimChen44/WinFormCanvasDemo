using CanvasDemo.Canvas;
using CanvasDemo.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Painter
{
    public class EllipseElement : ObjElement<ElementData>
    {
        public EllipseElement(EllipseLayer layer, ElementData data, int sideLength) : base(layer, data)
        {
            this.Rect.Width = sideLength;
            this.Rect.Height = sideLength;
        }

        public static Brush FillBrush = new SolidBrush(Color.Green);

        public override void Drawing(Graphics g)
        {
            g.FillEllipse(FillBrush, Viewer.LocalToShow(Rect));
        }

        public override void DrawingAfter(Graphics g)
        {

        }

    }

}
