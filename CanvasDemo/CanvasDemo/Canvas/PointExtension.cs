using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{
    public static class PointExtension
    {
        public static Point ToPoint(this Point source)
        {
            return new Point(
                Convert.ToInt32(source.X),
                Convert.ToInt32(source.Y));
        }

        /// <summary>
        /// 两个坐标围城的尺寸
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pt1"></param>
        /// <param name="pt2"></param>
        /// <returns></returns>
        public static Size Subtract(this Point source, Point pt)
        {
            return new Size(source.X - pt.X, source.Y - pt.Y);
        }


        /// <summary>
        /// 计算两点之间的距离
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static double Distance(this Point source, Point pt)
        {
            return Math.Sqrt((source.X - pt.X) * (source.X - pt.X) + (source.Y - pt.Y) * (source.Y - pt.Y));
        }


        /// <summary>
        /// 判断当前点是否在一组点组成的多边形中
        /// </summary>
        public static bool IsInZone(this Point point, Point[] points)
        {
            System.Drawing.Drawing2D.GraphicsPath myGraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
            myGraphicsPath.Reset();
            //添家多边形点，绘制出路径 
            myGraphicsPath.AddPolygon(points);
            Region myRegion = new Region();
            myRegion.MakeEmpty();
            //获得交集
            myRegion.Union(myGraphicsPath);
            //返回判断点是否在多边形里
            return myRegion.IsVisible(point);
        }

    }
}
