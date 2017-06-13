using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RayTracer
    {
        public RayTracer()
        {
            Primitives = new List<IPrimitive>();
        }

        public int MaxDepth { get; set; } = 5;

        public IList<IPrimitive> Primitives;

        public Color Trace(Ray ray, int depth)
        {
            Color res = Color.FromArgb(25, 25, 25);

            foreach (IPrimitive prim in Primitives)
            {
                LocalGeo geo;
                if (prim.Intersect(ray, out geo))
                {
                    res = prim.GetShading(geo).Ambient;
                }
            }

            return res;
        }
    }
}
