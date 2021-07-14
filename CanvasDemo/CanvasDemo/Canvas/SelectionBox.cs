using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    public class SelectionBox : Element
    {
        /// <summary>
        /// 是否从左往右选择
        /// </summary>
        public bool IsLeftToRight { get; set; } = true;

        public bool SelectionBoxIsShow { get; set; } = false;

        //框选开始坐标
        Point Start;
        //鼠标中键按下
        bool IsMouseLeftDown = false;

        ElementEditor Editor;

        public SelectionBox(ElementEditor editor, TimCanvas canvas) : base(canvas,nameof(SelectionBox))
        {
            Editor = editor;
        }

        public override void Drawing(Graphics g)
        {
            if (SelectionBoxIsShow == true)
            {
                if (IsLeftToRight == true)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, 51, 153, 255)), Viewer.LocalToShow(Rect));
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 51, 153, 255)), Viewer.LocalToShow(Rect));
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, 153, 255, 51)), Viewer.LocalToShow(Rect));
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 153, 255, 51)), Viewer.LocalToShow(Rect));
                }

            }
        }

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMouseLeftDown = true;
                Start = Viewer.MousePointToLocal(e.Location);
                Rect.Width = 0;
                Rect.Height = 0;
                SelectionBoxIsShow = true;
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            //比例缩放后结束坐标也要做调整
            if (IsMouseLeftDown == true)
            {
                var end = Viewer.MousePointToLocal(e.Location);

                IsLeftToRight = Start.X < end.X;

                Rect.X = Start.X < end.X ? Start.X : end.X;
                Rect.Y = Start.Y < end.Y ? Start.Y : end.Y;

                Rect.Width = Math.Abs(Start.X - end.X);
                Rect.Height = Math.Abs(Start.Y - end.Y);
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsMouseLeftDown = false;
                SelectionBoxIsShow = false;

                var end = Viewer.MousePointToLocal(e.Location);
                if (end.Distance(Start) < 15)
                {//开始和结束距离短，认为是鼠标点击选择
                    PointSelectOver(e.Location);
                }
                else
                {
                    BoxSelectOver();
                }

            }
        }

        public void MouseWheel(MouseEventArgs e)
        {

        }

        /// <summary>
        /// 选择单个对象
        /// </summary>
        private void PointSelectOver(Point mousePoint)
        {
            if (Control.ModifierKeys != Keys.Control)
            {
                Editor.ClearSelected();
            }
            var point = Viewer.MousePointToLocal(mousePoint);
            foreach (var item in Canvas.Layers)
            {
                if (item.IsActive == false) continue;
                var elm = item.Elements.FirstOrDefault(x => x.Rect.Contains(point) == true);
                if (elm != null)
                {

                    Editor.AddSelected(new List<ObjElement>() { elm });
                    Editor.SetCurrent(elm);
                    return;
                }
            }

        }

        /// <summary>
        /// 选择被框选的对象
        /// </summary>
        private void BoxSelectOver()
        {
            if (Control.ModifierKeys != Keys.Control)
            {
                //撤销以前的选择
                Editor.ClearSelected();
            }
            foreach (var item in Canvas.Layers)
            {
                if (item.IsActive == false) continue;

                if (IsLeftToRight == true)
                {//全部选中才算选中
                    Editor.AddSelected(item.Elements.AsParallel().Where(x => Rect.Contains(x.Rect) == true).ToList());
                }
                else
                {//相交就认为已经选中
                    Editor.AddSelected(item.Elements.AsParallel().Where(x => x.Rect.IntersectsWith(Rect) == true).ToList());
                }
            }
            Editor.SetCurrent(Editor.SelectedElements.FirstOrDefault());
        }

        public override void DrawingAfter(Graphics g)
        {
        
        }


    }
}
