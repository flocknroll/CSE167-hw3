using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Ray
    {
        public Ray(RTPoint point, RTVector vector, double tMin, double tMax, bool directional)
        {
            Point = point;
            Vector = vector;
            TMin = tMin;
            TMax = tMax;
            Directional = directional;
        }

        public RTPoint Point { get; }
        public RTVector Vector { get; }
        public double TMin { get; }
        public double TMax { get; }
        public bool Directional { get; set; }
    }
}
