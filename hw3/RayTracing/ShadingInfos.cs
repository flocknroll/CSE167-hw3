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
        public Color Diffuse { get; set; }
        public Color Specular { get; set; }
        public Color Emission { get; set; }
        public Color Ambient { get; set; }

        public double Shininess { get; set; }
    }
}
