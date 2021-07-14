
namespace CanvasDemo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cInit = new System.Windows.Forms.ToolStripButton();
            this.cLayer1 = new System.Windows.Forms.ToolStripButton();
            this.cLayer2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cFocusText = new System.Windows.Forms.ToolStripTextBox();
            this.cFocusBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cState = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timCanvas1 = new CanvasDemo.Canvas.TimCanvas();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timCanvas1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cInit,
            this.cLayer1,
            this.cLayer2,
            this.toolStripSeparator1,
            this.cFocusText,
            this.cFocusBtn,
            this.toolStripSeparator2,
            this.cState,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1036, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cInit
            // 
            this.cInit.Image = ((System.Drawing.Image)(resources.GetObject("cInit.Image")));
            this.cInit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cInit.Name = "cInit";
            this.cInit.Size = new System.Drawing.Size(76, 22);
            this.cInit.Text = "初始数据";
            this.cInit.Click += new System.EventHandler(this.cInit_Click);
            // 
            // cLayer1
            // 
            this.cLayer1.Checked = true;
            this.cLayer1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cLayer1.Image = ((System.Drawing.Image)(resources.GetObject("cLayer1.Image")));
            this.cLayer1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cLayer1.Name = "cLayer1";
            this.cLayer1.Size = new System.Drawing.Size(76, 22);
            this.cLayer1.Text = "方块图层";
            this.cLayer1.Click += new System.EventHandler(this.cLayer1_Click);
            // 
            // cLayer2
            // 
            this.cLayer2.Image = ((System.Drawing.Image)(resources.GetObject("cLayer2.Image")));
            this.cLayer2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cLayer2.Name = "cLayer2";
            this.cLayer2.Size = new System.Drawing.Size(76, 22);
            this.cLayer2.Text = "圆形图层";
            this.cLayer2.Click += new System.EventHandler(this.cLayer2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cFocusText
            // 
            this.cFocusText.Name = "cFocusText";
            this.cFocusText.Size = new System.Drawing.Size(100, 25);
            // 
            // cFocusBtn
            // 
            this.cFocusBtn.Image = ((System.Drawing.Image)(resources.GetObject("cFocusBtn.Image")));
            this.cFocusBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cFocusBtn.Name = "cFocusBtn";
            this.cFocusBtn.Size = new System.Drawing.Size(52, 22);
            this.cFocusBtn.Text = "定位";
            this.cFocusBtn.Click += new System.EventHandler(this.cFocusBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cState
            // 
            this.cState.Image = ((System.Drawing.Image)(resources.GetObject("cState.Image")));
            this.cState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cState.Name = "cState";
            this.cState.Size = new System.Drawing.Size(76, 22);
            this.cState.Text = "显示状态";
            this.cState.Click += new System.EventHandler(this.cState_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(76, 22);
            this.toolStripButton1.Text = "悬停高亮";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timCanvas1
            // 
            this.timCanvas1.AllowDrop = true;
            this.timCanvas1.Backgrounder = null;
            this.timCanvas1.CurrentLayer = null;
            this.timCanvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timCanvas1.ElementEditor = null;
            this.timCanvas1.IsLocked = false;
            this.timCanvas1.IsRootFormActivated = true;
            this.timCanvas1.Location = new System.Drawing.Point(0, 25);
            this.timCanvas1.Name = "timCanvas1";
            this.timCanvas1.Size = new System.Drawing.Size(1036, 708);
            this.timCanvas1.TabIndex = 1;
            this.timCanvas1.TabStop = false;
            this.timCanvas1.Viewer = null;
            // 
            // toolTip1
            // 
            this.toolTip1.Active = false;
            this.toolTip1.ShowAlways = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 733);
            this.Controls.Add(this.timCanvas1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timCanvas1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cInit;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton cLayer1;
        private System.Windows.Forms.ToolStripButton cLayer2;
        private Canvas.TimCanvas timCanvas1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox cFocusText;
        private System.Windows.Forms.ToolStripButton cFocusBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton cState;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

