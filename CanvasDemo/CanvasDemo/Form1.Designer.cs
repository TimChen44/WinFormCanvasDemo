
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            cInit = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            cLayer1 = new System.Windows.Forms.ToolStripButton();
            cLayer2 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            cFocusText = new System.Windows.Forms.ToolStripTextBox();
            cFocusBtn = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            cState = new System.Windows.Forms.ToolStripButton();
            toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            cText = new System.Windows.Forms.ToolStripTextBox();
            cSetText = new System.Windows.Forms.ToolStripButton();
            timer1 = new System.Windows.Forms.Timer(components);
            timCanvas1 = new Canvas.TimCanvas();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { cInit, toolStripSeparator4, cLayer1, cLayer2, toolStripSeparator1, cFocusText, cFocusBtn, toolStripSeparator2, cState, toolStripButton1, toolStripSeparator3, cText, cSetText });
            toolStrip1.Location = new System.Drawing.Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1036, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // cInit
            // 
            cInit.Image = (System.Drawing.Image)resources.GetObject("cInit.Image");
            cInit.ImageTransparentColor = System.Drawing.Color.Magenta;
            cInit.Name = "cInit";
            cInit.Size = new System.Drawing.Size(76, 22);
            cInit.Text = "初始数据";
            cInit.Click += cInit_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // cLayer1
            // 
            cLayer1.Checked = true;
            cLayer1.CheckState = System.Windows.Forms.CheckState.Checked;
            cLayer1.Image = (System.Drawing.Image)resources.GetObject("cLayer1.Image");
            cLayer1.ImageTransparentColor = System.Drawing.Color.Magenta;
            cLayer1.Name = "cLayer1";
            cLayer1.Size = new System.Drawing.Size(76, 22);
            cLayer1.Text = "方块图层";
            cLayer1.Click += cLayer1_Click;
            // 
            // cLayer2
            // 
            cLayer2.Image = (System.Drawing.Image)resources.GetObject("cLayer2.Image");
            cLayer2.ImageTransparentColor = System.Drawing.Color.Magenta;
            cLayer2.Name = "cLayer2";
            cLayer2.Size = new System.Drawing.Size(76, 22);
            cLayer2.Text = "圆形图层";
            cLayer2.Click += cLayer2_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cFocusText
            // 
            cFocusText.Name = "cFocusText";
            cFocusText.Size = new System.Drawing.Size(100, 25);
            // 
            // cFocusBtn
            // 
            cFocusBtn.Image = (System.Drawing.Image)resources.GetObject("cFocusBtn.Image");
            cFocusBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            cFocusBtn.Name = "cFocusBtn";
            cFocusBtn.Size = new System.Drawing.Size(52, 22);
            cFocusBtn.Text = "定位";
            cFocusBtn.Click += cFocusBtn_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cState
            // 
            cState.Image = (System.Drawing.Image)resources.GetObject("cState.Image");
            cState.ImageTransparentColor = System.Drawing.Color.Magenta;
            cState.Name = "cState";
            cState.Size = new System.Drawing.Size(76, 22);
            cState.Text = "显示状态";
            cState.Click += cState_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new System.Drawing.Size(76, 22);
            toolStripButton1.Text = "悬停高亮";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cText
            // 
            cText.Name = "cText";
            cText.Size = new System.Drawing.Size(100, 25);
            // 
            // cSetText
            // 
            cSetText.Image = (System.Drawing.Image)resources.GetObject("cSetText.Image");
            cSetText.ImageTransparentColor = System.Drawing.Color.Magenta;
            cSetText.Name = "cSetText";
            cSetText.Size = new System.Drawing.Size(76, 22);
            cSetText.Text = "设置标题";
            cSetText.Click += cSetText_Click;
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // timCanvas1
            // 
            timCanvas1.AllowDrop = true;
            timCanvas1.Backgrounder = null;
            timCanvas1.CurrentLayer = null;
            timCanvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            timCanvas1.ElementEditor = null;
            timCanvas1.IsLocked = false;
            timCanvas1.IsRootFormActivated = true;
            timCanvas1.Location = new System.Drawing.Point(0, 25);
            timCanvas1.Name = "timCanvas1";
            timCanvas1.Size = new System.Drawing.Size(1036, 708);
            timCanvas1.TabIndex = 1;
            timCanvas1.TabStop = false;
            timCanvas1.Viewer = null;
            // 
            // toolTip1
            // 
            toolTip1.Active = false;
            toolTip1.ShowAlways = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1036, 733);
            Controls.Add(timCanvas1);
            Controls.Add(toolStrip1);
            Name = "Form1";
            Text = "Form1";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox cText;
        private System.Windows.Forms.ToolStripButton cSetText;
    }
}

