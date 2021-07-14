using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CanvasDemo.Canvas;

namespace CanvasDemo.Painter
{
    public class CubeLayer : Layer
    {
        public CubeLayer(TimCanvas canvas) : base(canvas, "Cube")
        {
            IsInteractionLayer = true;
        }

        public override void Drawing(Graphics g)
        {
            foreach (var item in Elements)
            {
                if (Canvas.Viewer.IsInZone(item) == false) continue;
                item.Drawing(g);
            }
        }

        public override void DrawingAfter(Graphics g)
        {
     
        }
    }
}
