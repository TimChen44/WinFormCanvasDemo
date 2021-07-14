using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    public interface IMouseEvent
    {
        void MouseDown(MouseEventArgs e);
        void MouseMove(MouseEventArgs e);
        void MouseUp(MouseEventArgs e);
        void MouseWheel(MouseEventArgs e);
    }

}