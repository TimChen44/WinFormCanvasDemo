using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    /// <summary>
    /// 选择框对象
    /// </summary>
    public class ElementEditor : Element
    {
        SelectionBox SelectionBox;

        public ElementEditor(TimCanvas canvas) : base(canvas, nameof(ElementEditor))
        {
            SelectionBox = new SelectionBox(this, canvas);

        }

        public override void Drawing(Graphics g)
        {
            //绘制选择对象的拖动柄
            SelectedElements.ForEach(x => x.DrawingJoystick(g));

            SelectionBox.Drawing(g);
        }

        public Point MPoint;
        //对象状态
        private EditorState EState = EditorState.None;
        //拖动柄状态
        private TransformState TState = TransformState.None;

        public void MouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MPoint = Viewer.MousePointToLocal(e.Location);
                var elem = SelectedElements.FirstOrDefault(x => x.Rect.Contains(MPoint) == true);
                if (elem != null)
                {//点击已经选择的对象
                    SetCurrent(elem);//设定当前点的对象为操作对象

                    if (Canvas.IsLocked == false)
                    {//如果是只读，那么就不要进入移动和调整大小模式
                        if (MPoint.X > elem.Rect.Right - elem.JoystickSize && MPoint.Y > elem.Rect.Bottom - elem.JoystickSize && MPoint.X < elem.Rect.Right && MPoint.Y < elem.Rect.Bottom)
                        {
                            EState = EditorState.Transform;
                            TState = TransformState.RightBottom;
                        }
                        else if (MPoint.X > elem.Rect.Right - elem.JoystickSize && MPoint.Y > elem.Rect.Y + elem.Rect.Height / 2 - elem.JoystickSize / 2 && MPoint.X < elem.Rect.Right && MPoint.Y < elem.Rect.Y + elem.Rect.Height / 2 + elem.JoystickSize / 2)
                        {
                            EState = EditorState.Transform;
                            TState = TransformState.Right;
                        }
                        else if (MPoint.X > elem.Rect.X + elem.Rect.Width / 2 - elem.JoystickSize / 2 && MPoint.Y > elem.Rect.Bottom - elem.JoystickSize && MPoint.X < elem.Rect.X + elem.Rect.Width / 2 + elem.JoystickSize / 2 && MPoint.Y < elem.Rect.Bottom)
                        {
                            EState = EditorState.Transform;
                            TState = TransformState.Bottom;
                        }
                        else
                        {
                            EState = EditorState.Move;
                        }
                    }

                }
                else
                {
                    EState = EditorState.Selection;
                    SelectionBox.MouseDown(e);
                }
            }
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (EState == EditorState.Move)
            {//移动模式，设定对象位置
                var end = Viewer.MousePointToLocal(e.Location);
                var x = (end.X - MPoint.X);
                var y = (end.Y - MPoint.Y);

                SelectedElements.ForEach(elem => elem.Rect.Offset(x, y));
                MPoint = end;
            }
            else if (EState == EditorState.Transform)
            {//调整大小
                var end = Viewer.MousePointToLocal(e.Location);
                var x = (end.X - MPoint.X);
                var y = (end.Y - MPoint.Y);

                SelectedElements.ForEach(elem =>
                {
                    switch (TState)
                    {
                        case TransformState.RightBottom:
                            elem.Rect.Width += x;
                            elem.Rect.Height += y;
                            break;
                        case TransformState.Right:
                            elem.Rect.Width += x;
                            break;
                        case TransformState.Bottom:
                            elem.Rect.Height += y;
                            break;
                    }

                    if (elem.Rect.Width < 10) elem.Rect.Width = 10;
                    if (elem.Rect.Height < 10) elem.Rect.Height = 10;
                });

                MPoint = end;
            }
            else if (EState == EditorState.Selection)
            {//选择
                SelectionBox.MouseMove(e);

            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            if (EState == EditorState.Selection)
            {
                SelectionBox.MouseUp(e);
            }

            EState = EditorState.None;
        }

        public void MouseWheel(MouseEventArgs e)
        {

        }

        #region 对象选择操作

        /// <summary>
        /// 选择的对象集合
        /// </summary>
        public List<ObjElement> SelectedElements = new List<ObjElement>();

        /// <summary>
        /// 当前对象
        /// </summary>
        public ObjElement CurrentElement = null;

        /// <summary>
        /// 清除选的的对象
        /// </summary>
        public void ClearSelected()
        {
            SelectedElements.ForEach(x => x?.UnSelected());
            SelectedElements.Clear();
        }

        /// <summary>
        /// 设置当前选择的对象
        /// </summary>
        /// <param name="element"></param>
        public void SetCurrent(ObjElement element)
        {
            CurrentElement?.UnCurrent();
            CurrentElement = element;
            CurrentElement?.Current();
        }

        /// <summary>
        /// 添加选中的对象
        /// </summary>
        /// <param name="elements"></param>
        public void AddSelected(List<ObjElement> elements)
        {
            SelectedElements.AddRange(elements);
            elements.ForEach(x => x?.Selected());

            SelectedObjElementsEvent?.Invoke(SelectedElements);

        }
        /// <summary>
        /// 选择了对象元素后触发此方法
        /// </summary>
        public event Action<List<ObjElement>> SelectedObjElementsEvent;



        #endregion



        #region 元素操作

        /// <summary>
        /// 被删除的对象,可以理解成当前布局的回收站，可以从这里拿回已经删除的对象
        /// </summary>
        public Dictionary<string, ObjElement> DeletedElems = new Dictionary<string, ObjElement>();

        /// <summary>
        /// 移除选择的对象
        /// </summary>
        public void RemoveSelectElements()
        {
            foreach (var item in SelectedElements)
            {
                item.Layer.Elements.Remove(item);
                DeletedElems.Add(item.ID, item);
            }
            ClearSelected();

            SelectedObjElementsEvent?.Invoke(SelectedElements);

        }

        #endregion

        #region 对象布局操作

        /// <summary>
        /// 左对齐
        /// </summary>
        public void AlignLeft()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.X = CurrentElement.Rect.X;
            }
            Canvas.Refresh();
        }

        /// <summary>
        /// 右对齐
        /// </summary>
        public void AlignRight()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.X = CurrentElement.Rect.Right - item.Rect.Width;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 上对齐
        /// </summary>
        public void AlignTop()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.Y = CurrentElement.Rect.Y;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 下对齐
        /// </summary>
        public void AlignBottom()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.Y = CurrentElement.Rect.Bottom - item.Rect.Height;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 居中齐
        /// </summary>
        public void AlignCenter()
        {
            if (SelectedElements.Count <= 1) return;

            var center = CurrentElement.Rect.X + CurrentElement.Rect.Width / 2;

            foreach (var item in SelectedElements)
            {
                item.Rect.X = center - item.Rect.Width / 2;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 中间齐
        /// </summary>
        public void AlignMiddle()
        {
            if (SelectedElements.Count <= 1) return;

            var middle = CurrentElement.Rect.Y + CurrentElement.Rect.Height / 2;

            foreach (var item in SelectedElements)
            {
                item.Rect.Y = middle - item.Rect.Height / 2;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 宽度相同
        /// </summary>
        public void SameWidth()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.Width = CurrentElement.Rect.Width;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 高度相同
        /// </summary>
        public void SameHeight()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.Height = CurrentElement.Rect.Height;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 大小相同
        /// </summary>
        public void SameSize()
        {
            if (SelectedElements.Count <= 1) return;

            foreach (var item in SelectedElements)
            {
                item.Rect.Width = CurrentElement.Rect.Width;
                item.Rect.Height = CurrentElement.Rect.Height;
            };
            Canvas.Refresh();
        }

        /// <summary>
        /// 水平间距相同
        /// </summary>
        public void SameHorizontalSpace()
        {
            if (SelectedElements.Count <= 1) return;

            var orderFans = SelectedElements.OrderBy(x => x.Rect.X).ToList();
            var minLeft = orderFans.First().Rect.X;
            var maxLeft = orderFans.Last().Rect.X;
            var distance = maxLeft - minLeft;

            for (int i = 0; i < orderFans.Count; i++)
            {
                orderFans[i].Rect.X = (int)(minLeft + (float)i / ((float)orderFans.Count - 1.0f) * distance);
            }
            Canvas.Refresh();
        }

        /// <summary>
        /// 垂直间距相同
        /// </summary>
        public void SameVerticalSpace()
        {
            if (SelectedElements.Count <= 1) return;

            var orderFans = SelectedElements.OrderBy(x => x.Rect.Y).ToList();
            var minTop = orderFans.First().Rect.Y;
            var maxTop = orderFans.Last().Rect.Y;
            var distance = maxTop - minTop;

            for (int i = 0; i < orderFans.Count; i++)
            {
                orderFans[i].Rect.Y = (int)(minTop + (float)i / ((float)orderFans.Count - 1.0f) * distance);
            }
            Canvas.Refresh();
        }

        public override void DrawingAfter(Graphics g)
        {

        }


        #endregion


        enum EditorState
        {
            None,//没有任何操作
            Selection,//框选状态
            Move,//移动状态
            Transform,//调整大小状态
        }


        enum TransformState
        {
            None,
            RightBottom,
            Right,
            Bottom,

        }
    }



}