using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    public class Viewer
    {
        TimCanvas Canvas;
        public Viewer(TimCanvas canvas)
        {
            Canvas = canvas;

            //默认图纸坐标
            Zero = new Point(Canvas.Width / 2, Canvas.Height / 2);
            Viewport = new Rectangle(0 - Zero.X, 0 - Zero.Y, Canvas.Width, Canvas.Height);

        }

        /// <summary>
        /// 零点坐标（默认为画板中间）
        /// </summary>
        public Point Zero;//不能用属性，不然没法使用Offset之类函数

        /// <summary>
        /// 视口，当前用户可以看到的区域
        /// </summary>
        public Rectangle Viewport;//不能用属性，不然没法使用Offset之类函数

        // int DebugVX = 50, DebugVL = 100;//调试时故意减少视口，用于调试

        //缩放比例
        public float Zoom = 1;

        //最小比例
        private float MinZoom = 0.01f;
        //最大比例
        private float MaxZoom = 100;

        #region 视图调整

        //鼠标中键按下
        bool IsMouseMiddleDown = false;

        /// <summary>
        /// 移动前鼠标位置
        /// </summary>
        Point OldMousePoint;

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) IsMouseMiddleDown = true;
            OldMousePoint = e.Location;
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) IsMouseMiddleDown = false;
        }


        public void MouseMove(MouseEventArgs e)
        {
            var newLocation = e.Location;

            if (IsMouseMiddleDown == true)
            {//鼠标中键移动图纸

                var x = (newLocation.X - OldMousePoint.X);
                var y = (newLocation.Y - OldMousePoint.Y);

                Zero.Offset(x, y);

                Viewport.X = (int)((0 - Zero.X) / Zoom);
                Viewport.Y = (int)((0 - Zero.Y) / Zoom);
            }

            OldMousePoint = newLocation;
        }

        public void MouseWheel(MouseEventArgs e)
        {
            //鼠标滚轮滚动图纸
            int tZeroX = 0;
            int tZeroY = 0;
            if (e.Delta > 0)
            {
                if (Zoom == MaxZoom) return;

                Zoom = Zoom * 1.25f;
                if (Zoom > MaxZoom) Zoom = MaxZoom;

                tZeroX = (int)((e.X - Zero.X) - (e.X - Zero.X) * 1.25f);
                tZeroY = (int)((e.Y - Zero.Y) - (e.Y - Zero.Y) * 1.25f);
            }
            else
            {
                if (Zoom == MinZoom) return;

                Zoom = Zoom * 0.8f;
                if (Zoom < MinZoom) Zoom = MinZoom;

                tZeroX = (int)((e.X - Zero.X) - (e.X - Zero.X) * 0.8f);
                tZeroY = (int)((e.Y - Zero.Y) - (e.Y - Zero.Y) * 0.8f);
            }

            //调整相对坐标位置
            Zero.Offset(tZeroX, tZeroY);

            //调整视口位置
            Viewport.X = (int)((0 - Zero.X) / Zoom);
            Viewport.Y = (int)((0 - Zero.Y) / Zoom);
            Viewport.Width = (int)((Canvas.Width) / Zoom);
            Viewport.Height = (int)((Canvas.Height) / Zoom);
        }

        /// <summary>
        /// 设置缩放
        /// </summary>
        /// <param name="zoom"></param>
        public void SetZoom(float zoom)
        {
            Zoom = zoom;
            Canvas.Refresh();
        }
        public void SetZero(int x,int y)
        {
            Zero.X = x;
            Zero.Y = y;
            //调整视口位置
            Viewport.X = (int)((0 - Zero.X) / Zoom);
            Viewport.Y = (int)((0 - Zero.Y) / Zoom);
        }


            /// <summary>
            /// 设置成完整显示
            /// </summary>
            public void SetFullDisplay()
        {
            var w = (float)Canvas.Width / (float)Canvas.BackgrounderSize.Width;
            var h = (float)Canvas.Height / (float)Canvas.BackgrounderSize.Height;
            Zoom = w < h ? w : h;
            MinZoom = Zoom;
            MaxZoom = Zoom * 100;

            Zero.X = Canvas.Width / 2;
            Zero.Y = Canvas.Height / 2;

            Viewport.X = (int)((0 - Zero.X) / Zoom);
            Viewport.Y = (int)((0 - Zero.Y) / Zoom);
            Viewport.Width = (int)(Canvas.Width / Zoom);
            Viewport.Height = (int)(Canvas.Height / Zoom);

            Canvas.Refresh();
        }

        #endregion

        #region 控制是否绘制，用于优化性能

        /// <summary>
        /// 是否在区域中用于优化性能，这个涉及到性能优化
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool IsInZone(Element element)
        {
            return Viewport.IntersectsWith(element.Rect);
        }

        public bool IsInZone(Rectangle rect)
        {
            return Viewport.IntersectsWith(rect);
        }

        #endregion

        #region  本地坐标转世界坐标

        /// <summary>
        /// 本地（图纸）矩形变换到显示矩形
        /// </summary>
        public Rectangle LocalToShow(Rectangle rect)
        {
            //要清晰的确定本地和世界的关系
            var r = new Rectangle(rect.Location, rect.Size);

            //计算缩放坐标
            r.X = (int)(r.X * Zoom) + Zero.X;
            r.Y = (int)(r.Y * Zoom) + Zero.Y;

            //调整矩形大小
            r.Width = (int)Math.Round(rect.Width * Zoom, 0);
            r.Height = (int)Math.Round(rect.Height * Zoom, 0);

            return r;
        }

        public Point LocalToShow(Point point)
        {
            return new Point((int)(point.X * Zoom) + Zero.X, (int)(point.Y * Zoom) + Zero.Y);
        }

        public Rectangle LocalToShow(int x, int y, int width, int height)
        {
            return new Rectangle(
                (int)(x * Zoom) + Zero.X,
                (int)(y * Zoom) + Zero.Y,
                (int)Math.Round(width * Zoom, 0),
                (int)Math.Round(height * Zoom, 0)
             );
        }

        public int ToShowX(int x)
        {
            return (int)(x * Zoom) + Zero.X;
        }
        public int ToShowY(int y)
        {
            return (int)(y * Zoom) + Zero.Y;
        }

        /// <summary>
        /// 本地尺寸变换到显示的尺寸
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public Size LocalToShow(Size size)
        {
            return new Size((int)Math.Round(size.Width * Zoom, 0), (int)Math.Round(size.Height * Zoom, 0));
        }

        /// <summary>
        /// 鼠标坐标点变换到显示坐标点
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point MousePointToLocal(Point point)
        {
            return new Point((int)Math.Round((point.X - Zero.X) / Zoom, 0), (int)Math.Round((point.Y - Zero.Y) / Zoom, 0));
        }

        #endregion
    }
}
