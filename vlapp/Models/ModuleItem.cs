using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vlapp.Models
{
    internal class ModuleItem
    {
        public int blindIndex { get; set; }
        public int moduleIndex { get; set; }
        public int redVal { get; set; }
        public int greenVal { get; set; }
        public int blueVal { get; set; }
        public int currentGain { get; set; }
    }
}
