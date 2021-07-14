using CanvasDemo.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Painter
{
    public class ToolTipComponent : IToolTip
    {
        TimCanvas Canvas;

        public ToolTipComponent(TimCanvas canvas)
        {
            Canvas = canvas;
        }

        public void Drawing(Graphics g)
        {

        }

        public void DrawingAfter(Graphics g)
        {

        }

        public void Hide()
        {
            if (LastCube != null)
            {
                LastCube.IsHover = false;
                LastCube = null;
                Canvas.Refresh();
            }
        }

        CubeElement LastCube;

        public void Show(IToolTipElement element)
        {
            if (element is CubeElement cube && LastCube != cube)
            {
                if (LastCube != null) LastCube.IsHover = false;


                LastCube = cube;
                LastCube.IsHover = true;

                Canvas.Refresh();
            }


        }
    }
}
