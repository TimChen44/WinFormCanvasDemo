using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Canvas
{
    /// <summary>
    /// 每个对象的身份
    /// </summary>
    public interface IElementData
    {
        /// <summary>
        /// 编号
        /// </summary>
        string ID { get; set; }
    }
}
