using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo.Canvas
{
    public delegate void LayerChangedEvent(Layer layer);

    public class TimCanvas : PictureBox, IDisposable
    {
        public event LayerChangedEvent LayerChanged;

        /// <summary>
        /// 对象编辑
        /// </summary>
        public ElementEditor ElementEditor { get; set; }

        /// <summary>
        /// 视图
        /// </summary>
        public Viewer Viewer { get; set; }

        /// <summary>
        /// 控制背景
        /// </summary>
        public Backgrounder Backgrounder { get; set; }

        public Size BackgrounderSize;//背景图大小，也就是画布大小

        public List<Layer> Layers = new List<Layer>();

        /// <summary>
        /// 当前图层
        /// </summary>
        public Layer CurrentLayer { get; set; }

        /// <summary>
        /// 是否处于锁定状态
        /// </summary>
        public bool IsLocked { get; set; } = false;

        #region 图纸配置

        /// <summary>
        /// 是否高质量图纸缩放
        /// </summary>
        public static bool IsHighQualityDrawingScaling { get; set; } = false;

        #endregion

        /// <summary>
        /// 根窗体激活状态
        /// </summary>
        public bool IsRootFormActivated { get; set; } = true;


        public TimCanvas()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;

            this.AllowDrop = true;

            InitialToolTip();

        }


        public virtual void Initialize()
        {
            Viewer = new Viewer(this);
            Backgrounder = new Backgrounder(this);

            //初始化功能性图层
            ElementEditor = new ElementEditor(this);


            //增加事件
            this.Paint += DrawingBoard_Paint;
            this.MouseMove += DrawingBoard_MouseMove;
            this.MouseDown += Canvas_MouseDown;
            this.MouseUp += Canvas_MouseUp;
            this.MouseWheel += Canvas_MouseWheel;
            this.MouseDoubleClick += TimCanvas_MouseDoubleClick;

            //监视根窗体的激活状态
            var rootForm = FindForm();
            rootForm.Activated += (s, e) => IsRootFormActivated = true;
            rootForm.Deactivate += (s, e) => IsRootFormActivated = false;
        }


        #region 鼠标事件调用

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            var l = CurrentLayer?.MouseDown(e);
            if (l != true)//优先处理图层的鼠标操作，有些操作需要屏蔽
            {
                Viewer.MouseDown(e);
                ElementEditor.MouseDown(e);
            }
            this.Refresh();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            var l = CurrentLayer?.MouseUp(e);
            if (l != true)//优先处理图层的鼠标操作，有些操作需要屏蔽
            {
                Viewer.MouseUp(e);
                ElementEditor.MouseUp(e);
            }
            this.Refresh();
        }


        private void DrawingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            var l = CurrentLayer?.MouseMove(e);
            if (l != true)//优先处理图层的鼠标操作，有些操作需要屏蔽
            {
                Viewer.MouseMove(e);
                ElementEditor.MouseMove(e);
            }
            if (e.Button != MouseButtons.None)
            {//TODO: 增加了刷新条件，尝试减少刷新来优化系统性
                this.Refresh();
            }
        }

        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {
            var l = CurrentLayer?.MouseWheel(e);
            if (l != true)//优先处理图层的鼠标操作，有些操作需要屏蔽
            {
                Viewer.MouseWheel(e);
            }
            this.Refresh();
        }

        private void TimCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #endregion

        #region  绘图

        /// <summary>
        /// 绘制画布
        /// </summary>
        private void DrawingBoard_Paint(object sender, PaintEventArgs e)
        {

            Backgrounder.Drawing(e.Graphics);

            //绘制各图层信息-正常内容
            foreach (var item in Layers)
            {
                if (item.IsVisible == true) item.Drawing(e.Graphics);
            }

            //绘制各图层信息-顶部内容
            foreach (var item in Layers)
            {
                if (item.IsVisible == true) item.DrawingAfter(e.Graphics);
            }

            if (FocusElement != null && FocusElement.IsShow == true)
                FocusElement.Drawing(e.Graphics);


            //绘制选择框
            ElementEditor.Drawing(e.Graphics);


            //绘制拖拽对象
            if (DragElement != null)
            {
                DragElement.Rect.Location = Viewer.MousePointToLocal(this.PointToClient(MousePosition));
                DragElement?.Drawing(e.Graphics);
            }


            ToolTip?.Drawing(e.Graphics);
        }

        #endregion

        public T GetLayer<T>(string name) where T : class
        {
            return Layers.FirstOrDefault(x => x.Name == name) as T;
        }

        #region 拖拽支持

        /// <summary>
        /// 拖拽对象
        /// </summary>
        public Element DragElement;

        #endregion


        #region 交互操作

        protected FocusElement FocusElement = null;

        /// <summary>
        /// 设置焦点对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="layoutName"></param>
        public void SetElementFocus(string id, string layoutName)
        {
            var layout = Layers.FirstOrDefault(x => x.Name == layoutName);
            if (layout == null) return;

            var elem = layout.Elements.FirstOrDefault(x => x.ID == id);
            if (elem == null) return;

            SetElementFocus(elem);
        }
        public void SetElementFocus(ObjElement elem)
        {
            if (elem == null) return;

            var x = (int)(-1 * (elem.Rect.X + elem.Rect.Width / 2 - Viewer.Viewport.Width / 2) * Viewer.Zoom);
            var y = (int)(-1 * (elem.Rect.Y + elem.Rect.Height / 2 - Viewer.Viewport.Height / 2) * Viewer.Zoom);
            Viewer.SetZero(x, y);
            if (FocusElement == null)
                FocusElement = new Canvas.FocusElement(this);

            FocusElement.SetFocus(new Point(elem.Rect.X + elem.Rect.Width / 2, elem.Rect.Y + elem.Rect.Height / 2));
            FocusElement.IsShow = true;
        }


        public void RemoveObjElement(string name, Action<ObjElement> ElemCallback = null)
        {
            var layer = Layers.FirstOrDefault(x => x.Name == name);
            if (layer == null) return;
            if (ElementEditor.SelectedElements.Count == 0) return;
            ElementEditor.SelectedElements.ForEach(x =>
            {
                ElemCallback?.Invoke(x);
            });
            ElementEditor.RemoveSelectElements();
        }

        /// <summary>
        /// 获得单个对象
        /// </summary>
        public ObjElement GetElement(Point mousePoint)
        {
            var point = Viewer.MousePointToLocal(mousePoint);
            foreach (var item in Layers)
            {
                if (item.IsActive == false) continue;//TODO:此处可以进行优化,只对显示的对象进行检索提高效率
                var elm = item.Elements.FirstOrDefault(x => x.Rect.Contains(point) == true);
                if (elm != null)
                {
                    return elm;
                }
            }
            return null;
        }

        /// <summary>
        /// 获得显示的单个对象
        /// </summary>
        public ObjElement GetVisibleElement(Point mousePoint)
        {
            var point = Viewer.MousePointToLocal(mousePoint);
            foreach (var item in Layers)
            {
                //没有显示的和非交互图层就不能被获得对象
                if (item.IsVisible == false || item.IsInteractionLayer == false) continue;//TODO:此处可以进行优化,只对显示的对象进行检索提高效率
                var elm = item.Elements.FirstOrDefault(x => x.Rect.Contains(point) == true);
                if (elm != null)
                {
                    return elm;
                }
            }
            return null;
        }
        #endregion

        #region  图层操作
        /// <summary>
        /// 设置当前图层
        /// </summary>
        /// <param name="name"></param>
        public void SetCurrentLayer(string name)
        {
            Layers.ForEach(x => x.IsActive = false);
            var layer = Layers.FirstOrDefault(x => x.Name == name);
            SetCurrentLayer(layer);
        }

        public void SetCurrentLayer(Layer layer)
        {
            if (layer != null)
            {
                layer.IsActive = true;//当前图层必须是激活的
                layer.IsVisible = true;//当前图层必须是可见的
                CurrentLayer = layer;
                LayerChanged?.Invoke(layer);
            }
        }

        /// <summary>
        /// 设置图层显示
        /// </summary>
        /// <param name="name"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool SetLayerDisplay(string name)
        {
            var layer = Layers.FirstOrDefault(x => x.Name == name);
            return SetLayerDisplay(layer);
        }
        public bool SetLayerDisplay(Layer layer)
        {
            if (layer == null) return false;
            layer.IsVisible = true;
            this.Refresh();
            return true;
        }

        /// <summary>
        /// 设置图层隐藏
        /// </summary>
        /// <param name="name"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool SetLayerHidden(string name)
        {
            var layer = Layers.FirstOrDefault(x => x.Name == name);
            return SetLayerHidden(layer);
        }
        public bool SetLayerHidden(Layer layer)
        {
            if (layer == null) return false;
            layer.IsVisible = false;
            this.Refresh();
            return true;
        }

        #endregion

        #region 工具提示支持

        private Point ToolTipMousePosition = new Point();

        /// <summary>
        /// 拖拽对象
        /// </summary>
        public IToolTip ToolTip;

        Timer ToolTipTimer;

        private void InitialToolTip()
        {
            ToolTipTimer = new Timer();
            ToolTipTimer.Interval = 400;
            ToolTipTimer.Tick += ToolTipTimer_Tick;
            ToolTipTimer.Start();
        }

        private void ToolTipTimer_Tick(object sender, EventArgs e)
        {
            if (IsRootFormActivated == false) return;
            if (ToolTip == null) return;
            if (ToolTipMousePosition == MousePosition)
            {
                //只有继承了提示接口的才能用于提示
                var objElement = GetVisibleElement(this.PointToClient(ToolTipMousePosition)) as IToolTipElement;
                if (objElement == null) return;
                ToolTip.Show(objElement);
            }
            else if (ToolTipMousePosition != MousePosition)
            {
                ToolTip.Hide();
                ToolTipMousePosition = MousePosition;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            ElementEditor?.SelectedElements?.Clear();

            Layers?.ForEach(x =>
            {
                x.Elements.Clear();
            });

            Layers?.Clear();

            ToolTipTimer?.Stop();
            ToolTipTimer?.Dispose();
        }

    }

}
