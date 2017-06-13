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
        private static readonly Color DEFAULT_AMBIENT = Color.FromArgb(51, 51, 51);

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
