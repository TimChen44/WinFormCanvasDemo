using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    public abstract class Layer
    {
        public string Name { get; set; }

        public TimCanvas Canvas { get; set; }

        public List<ObjElement> Elements { get; set; } = new List<ObjElement>();

        /// <summary>
        /// 是否被激活，只有激活状态的图层上的对象才能被选择
        /// </summary>
        public bool IsActive { get; set; } = false;

        /// <summary>
        /// 图层是否可见，但图层多的时候便于操作
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 是否是互动图层，就是用于交互操作的图层，通常用于放置对象，与他对应的就是辅助图层，用于显示辅助内容，没有交互操作
        /// </summary>
        public bool IsInteractionLayer { get; set; } = false;

        public Layer(TimCanvas canvas, string name)
        {
            Canvas = canvas;
            Name = name;
            Canvas.Layers.Add(this);
        }

        /// <summary>
        /// 正常绘图
        /// </summary>
        /// <param name="g"></param>
        public abstract void Drawing(Graphics g);

        /// <summary>
        /// 第二次绘制，用于显示一些在前端的文字等
        /// </summary>
        public abstract void DrawingAfter(Graphics g);

        public virtual bool MouseDown(MouseEventArgs e) => false;
        public virtual bool MouseMove(MouseEventArgs e) => false;
        public virtual bool MouseUp(MouseEventArgs e) => false;
        public virtual bool MouseWheel(MouseEventArgs e) => false;
    }
}
