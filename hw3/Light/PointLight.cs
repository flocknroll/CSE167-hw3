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
            RTVector ray = Source - geo.Point;

            return new Ray(geo.Point, ray.Normalize(), 1e-3f, ray.Length, false);
        }
    }
}
