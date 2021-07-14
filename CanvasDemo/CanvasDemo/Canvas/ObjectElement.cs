using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{
    /// <summary>
    /// 对象元素
    /// </summary>
    public abstract class ObjElement<T> : ObjElement where T : IElementData
    {
        /// <summary>
        /// 对象实体
        /// </summary>
        public T Data { get; set; }

        public ObjElement(Layer layer, T data) : base(layer, data.ID)
        {
            Data = data;
        }
    }

    public abstract class ObjElement : Element
    {

        public Layer Layer;

        public ObjElement(Layer layer, string id) : base(layer.Canvas, id)
        {
            Layer = layer;
            Layer.Elements.Add(this);
        }

        /// <summary>
        /// 是否非选中
        /// </summary>
        public bool IsSelected { get; private set; } = false;

        /// <summary>
        /// 是否是当前对象
        /// </summary>
        public bool IsCurrent { get; private set; } = false;

        /// <summary>
        /// 选择对象
        /// </summary>
        public void Selected()
        {
            IsSelected = true;
            SelectedEvent();
        }
        protected virtual void SelectedEvent() { }

        /// <summary>
        /// 清除对象选择
        /// </summary>
        public void UnSelected()
        {
            IsSelected = false;
            UnSelectedEvent();
        }
        protected virtual void UnSelectedEvent() { }

        public void Current()
        {
            IsCurrent = true;
            CurrentEvent();
        }
        protected virtual void CurrentEvent() { }

        public void UnCurrent()
        {
            IsCurrent = false;
            UnCurrentEvent();
        }
        protected virtual void UnCurrentEvent() { }


        public int JoystickSize { get { return (Rect.Width + Rect.Height) / 20 + 1; } }

        static Brush JoystickCurrent = new SolidBrush(Color.FromArgb(230, 255, 255, 255));
        static Brush JoystickSelect = new SolidBrush(Color.FromArgb(230, 50, 50, 50));

        /// <summary>
        /// 绘制八个操纵柄
        /// </summary>
        /// <param name="painter"></param>
        public void DrawingJoystick(Graphics g)
        {
            if (IsSelected == false) return;

            var cX = Rect.Width / 2;
            var cY = Rect.Height / 2;
            var s = JoystickSize;
            if (IsCurrent == false)
            {
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X, Rect.Y + (0), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (0), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (0), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (cY - s / 2), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickSelect, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (cY - s / 2), s, s));
            }
            else if (IsCurrent == true)
            {
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X, Rect.Y + (0), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (0), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (0), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (cY - s / 2), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (Rect.Height - s), s, s));
                g.FillRectangle(JoystickCurrent, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (cY - s / 2), s, s));

                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X, Rect.Y + (0), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (0), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (0), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (cY - s / 2), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (Rect.Width - s), Rect.Y + (Rect.Height - s), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (cX - s / 2), Rect.Y + (Rect.Height - s), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (Rect.Height - s), s, s));
                g.DrawRectangle(Pens.Black, Viewer.LocalToShow(Rect.X + (0), Rect.Y + (cY - s / 2), s, s));

            }
        }



    }


}
