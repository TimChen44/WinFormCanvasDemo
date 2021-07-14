using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{
    /// <summary>
    /// 绘制十字焦点
    /// </summary>
    public class FocusElement : Element
    {
        public bool IsShow { get; set; } = false;
        public Point Focus { get; private set; }

        //public Point H1, H2;

        //public Point V1, V2;

        public void SetFocus(Point focus)
        {
            Focus = focus;
            //H1 = new Point(focus.X, 0);
            //H2 = new Point(focus.X, this.Canvas.Height);

            //V1 = new Point(0, focus.Y);
            //V2 = new Point(this.Canvas.Width, focus.Y);

        }

        public FocusElement(Canvas.TimCanvas canvas) : base(canvas,nameof(FocusElement))
        {
        }

        public override void Drawing(Graphics g)
        {
            var focus = this.Canvas.Viewer.LocalToShow(Focus);

            g.DrawLine(Pens.Black, new Point(focus.X, 0), new Point(focus.X, this.Canvas.Height));
            g.DrawLine(Pens.Black, new Point(0, focus.Y), new Point(this.Canvas.Width, focus.Y));
        }

        public override void DrawingAfter(Graphics g)
        {
          
        }
    }
}
