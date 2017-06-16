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

            InvDir = 1f / Vector;
            Signs = new int[] { InvDir.X < 0 ? 1 : 0,
                                InvDir.Y < 0 ? 1 : 0,
                                InvDir.Z < 0 ? 1 : 0 };
        }

        public RTPoint Point { get; }
        public RTVector Vector { get; }
        public float TMin { get; }
        public float TMax { get; }
        public bool Directional { get; set; }
        public RTVector InvDir { get; }
        public int[] Signs { get; }
    }
}
