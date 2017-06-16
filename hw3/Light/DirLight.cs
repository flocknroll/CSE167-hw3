using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class DirLight : ILight
    {
        public DirLight(RTVector dir, RTColor color)
        {
            Direction = dir;
            Color = color;
        }

        public RTVector Direction { get; }
        public RTColor Color { get; }

        public Ray GenerateRay(LocalGeo geo, out RTColor color)
        {
            color = Color;

            return new Ray(geo.Point, Direction, 1e-3f, float.MaxValue, true);
        }
    }
}
