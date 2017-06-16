using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Material : IShading
    {
        private static readonly RTColor DEFAULT_AMBIENT = new RTColor(0, 0.2f, 0.2f, 0.2f);

        public Material()
        {
            Properties = new ShadingInfos
            {
                Ambient = DEFAULT_AMBIENT,
            };
        }

        public ShadingInfos Properties { get; set; }

        public ShadingInfos GetShading(LocalGeo geo)
        {
            return Properties;
        }
    }
}
