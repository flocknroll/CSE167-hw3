using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class ShadingInfos
    {
        public RTColor Diffuse { get; set; } = new RTColor();
        public RTColor Specular { get; set; } = new RTColor();
        public RTColor Emission { get; set; } = new RTColor();
        public RTColor Ambient { get; set; } = new RTColor();

        public double Shininess { get; set; } = 0.0d;
    }
}
