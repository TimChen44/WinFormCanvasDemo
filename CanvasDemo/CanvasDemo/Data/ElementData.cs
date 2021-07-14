using CanvasDemo.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDemo.Data
{
    public class ElementData : IElementData
    {
        public string ID { get; set; }

        public int Group { get; set; }

        public string Title { get; set; }

        public bool IsError { get; set; }
    }
}
