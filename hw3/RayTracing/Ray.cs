using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Ray
    {
        public Ray(RTPoint point, RTVector vector, double tMin, double tMax)
        {
            Point = point;
            Vector = vector;
            TMin = tMin;
            TMax = tMax;
        }

        public RTPoint Point { get; }
        public RTVector Vector { get; }
        public double TMin { get; }
        public double TMax { get; }
    }
}
