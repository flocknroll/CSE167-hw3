using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class PointLight : ILight
    {
        public PointLight(RTPoint source, RTColor color)
        {
            Source = source;
            Color = color;
        }

        public RTPoint Source { get; }
        public RTColor Color { get; }

        public Ray GenerateRay(LocalGeo geo, out RTColor color)
        {
            color = Color;

            return new Ray(geo.Point, Source - geo.Point, 0.001d, double.MaxValue, false);
        }
    }
}
