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
    public class CubeElement : ObjElement<ElementData>, IToolTipElement
    {


        public CubeElement(CubeLayer layer, ElementData data, int sideLength) : base(layer, data)
        {
            this.Rect.Width = sideLength;
            this.Rect.Height = sideLength;
        }

        public bool IsHover { get; set; } = false;

        public static Brush FillBrush = new SolidBrush(Color.Blue);

        public static Brush ErrorBrush = new SolidBrush(Color.Red);



        public override void Drawing(Graphics g)
        {

            var titleH = (int)(Rect.Height * 0.25);

            var fillBrush = Data.IsError ? ErrorBrush : FillBrush;

            if (titleH * Viewer.Zoom > 10)//如果标题大于10就认真绘制，如哦小于那么就简化
            {
                var borderW = (int)(Rect.Height * 0.01 * Viewer.Zoom) + 1;

                g.FillRectangle(Brushes.White, Viewer.LocalToShow(Rect.X, Rect.Y, Rect.Width, titleH + 1));
                var fontSize = (int)(titleH / 2 * Viewer.Zoom) + 1;
                if (fontSize >= 3)
                {
                    g.DrawString(Data.Title.ToString(), new Font("微软雅黑", fontSize > 60 ? 60 : fontSize), Brushes.Black, Viewer.LocalToShow(Rect.X + (int)(borderW / Viewer.Zoom), Rect.Y + (int)(borderW / Viewer.Zoom), Rect.Width, Rect.Height));
                }

                var contentRect = Viewer.LocalToShow(Rect.X, Rect.Y + titleH, Rect.Width, Rect.Height - titleH);
                g.FillRectangle(fillBrush, contentRect);
                g.DrawString(Data.Group.ToString(), new Font("微软雅黑", (Rect.Height - titleH) / 2 * Viewer.Zoom),
                    Brushes.White, contentRect, SFAlignment);


                if (IsHover)
                    g.DrawRectangle(new Pen(Brushes.Red, borderW * 2), Viewer.LocalToShow(Rect));
                else
                    g.DrawRectangle(new Pen(Brushes.Black, borderW), Viewer.LocalToShow(Rect));

            }
            else if (titleH * Viewer.Zoom > 5)
            {
                g.FillRectangle(fillBrush, Viewer.LocalToShow(Rect));

                var fontSize = (int)(titleH * Viewer.Zoom) + 1;
                if (fontSize >= 3)
                {
                    g.DrawString(Data.Group.ToString(), new Font("微软雅黑", fontSize > 60 ? 60 : fontSize), Brushes.White, Viewer.LocalToShow(Rect.X + 1, Rect.Y + 1, Rect.Width, Rect.Height), SFAlignment);
                }
            }
            else
            {
                g.FillRectangle(fillBrush, Viewer.LocalToShow(Rect));
            }
        }

        public override void DrawingAfter(Graphics g)
        {

        }

        public string GetToolTipTitle()
        {
            return $"[{Data.Group}] {Data.Title}";
        }

        protected static StringFormat SFAlignment = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
        };
    }

}
