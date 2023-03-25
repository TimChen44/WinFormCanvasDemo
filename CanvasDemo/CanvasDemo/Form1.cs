using CanvasDemo.Canvas;
using CanvasDemo.Data;
using CanvasDemo.Painter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timCanvas1.Initialize();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var r = new Random().Next(20);
            CubeDatas.ForEach(x => x.IsError = x.Group == r);
            timCanvas1.Refresh();
        }

        EllipseLayer EllipseLayer;


        List<ElementData> CubeDatas = new();

        CubeLayer CubeLayer;



        private void cInit_Click(object sender, EventArgs e)
        {
            var r = new Random();

            EllipseLayer = new EllipseLayer(timCanvas1);
            EllipseLayer.IsVisible = false;
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    var cubeData = new ElementData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Group = r.Next(20),
                        Title = "E-" + (x + y * 100).ToString(),
                    };
                    var elem = new EllipseElement(EllipseLayer, cubeData, 50);
                    elem.Rect.X = x * 60;
                    elem.Rect.Y = y * 60;
                }
            }
            timCanvas1.SetCurrentLayer(CubeLayer);


            CubeLayer = new CubeLayer(timCanvas1);
            for (int x = 0; x < 300; x++)
            {
                for (int y = 0; y < 200; y++)
                {
                    var cubeData = new ElementData()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Group = r.Next(20),
                        Title = (x + y * 200).ToString(),
                    };
                    CubeDatas.Add(cubeData);
                    var elem = new CubeElement(CubeLayer, cubeData, 100);
                    elem.Rect.X = x * 120;
                    elem.Rect.Y = y * 150;
                }
            }
            timCanvas1.SetCurrentLayer(CubeLayer);


            timCanvas1.Refresh();
        }

        private void cLayer1_Click(object sender, EventArgs e)
        {
            cLayer1.Checked = !cLayer1.Checked;
            CubeLayer.IsVisible = cLayer1.Checked;
            timCanvas1.Refresh();
        }

        private void cFocusBtn_Click(object sender, EventArgs e)
        {
            var elem = CubeLayer.Elements.FirstOrDefault(x => ((CubeElement)x).Data.Title == cFocusText.Text);
            if (elem == null) return;

            timCanvas1.SetElementFocus(elem);
            timCanvas1.Refresh();
        }

        private void cLayer2_Click(object sender, EventArgs e)
        {
            cLayer2.Checked = !cLayer2.Checked;
            EllipseLayer.IsVisible = cLayer2.Checked;
            timCanvas1.Refresh();
        }

        private void cState_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timCanvas1.ToolTip = new ToolTipComponent(timCanvas1);

        }

        private void cSetText_Click(object sender, EventArgs e)
        {
            timCanvas1.ElementEditor.SelectedElements.ForEach(x =>
            {
                if (x is CubeElement cube)
                {
                    cube.Data.Title = cText.Text;
                }
            });
            timCanvas1.Refresh();
        }
    }
}
