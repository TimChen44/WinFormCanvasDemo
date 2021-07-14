using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{

    public abstract class Element
    {
        public string ID { get; set; }
        /// <summary>
        /// 当前对象的区域范围
        /// </summary>
        public Rectangle Rect;//不能用属性，因为属性不能给修改内部值

        /// <summary>
        /// 正常绘图
        /// </summary>
        /// <param name="g"></param>
        public abstract void Drawing(Graphics g);

        /// <summary>
        /// 第二次绘制，用于显示一些在前端的文字等
        /// </summary>
        public abstract void DrawingAfter(Graphics g);

        /// <summary>
        /// 画布
        /// </summary>
        protected TimCanvas Canvas;

        /// <summary>
        /// 视图
        /// </summary>
        protected Viewer Viewer;

        public Element(TimCanvas canvas,string id)
        {
            Canvas = canvas;
            Viewer = canvas.Viewer;
            ID = id;
        }


    }


}
