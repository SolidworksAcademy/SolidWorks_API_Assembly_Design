using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorksApi_Lesson3_Assembly.Parts
{
    public abstract class Base
    {
        public string FileName { get; set; }
        public string TargetFolder { get; set; }
        public string AssemblyName { get; set; }

        public abstract void CreatePart();
    }
}
