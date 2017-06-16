using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Ray
    {
        public Ray(RTPoint point, RTVector vector, float tMin, float tMax, bool directional)
        {
            Point = point;
            Vector = vector;
            TMin = tMin;
            TMax = tMax;
            Directional = directional;
        }

        public RTPoint Point { get; }
        public RTVector Vector { get; }
        public float TMin { get; }
        public float TMax { get; }
        public bool Directional { get; set; }
    }
}
