using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{

    /// <summary>
    /// 控制背景
    /// </summary>
    public class Backgrounder 
    {
        TimCanvas Canvas;

        public Backgrounder(TimCanvas canvas)
        {
            Canvas = canvas;
        }

        Pen ZeroLinePen = new Pen(new SolidBrush(Color.Black),2);

        public void Drawing(Graphics g)
        {
            var v = Canvas.Viewer.Viewport;

            var vP1 = new Point(0 , v.Y);
            var vP2 = new Point(0 , v.Y + v.Height);
            g.DrawLine(ZeroLinePen, Canvas.Viewer.LocalToShow(vP1), Canvas.Viewer.LocalToShow(vP2));

            var hP1 = new Point(v.Left, 0);
            var hP2 = new Point(v.Left+v.Width, 0);
            g.DrawLine(ZeroLinePen, Canvas.Viewer.LocalToShow(hP1), Canvas.Viewer.LocalToShow(hP2));
        }

    }


}
