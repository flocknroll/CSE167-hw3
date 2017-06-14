using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class GeometricPrimitive : IPrimitive
    {
        public GeometricPrimitive(IIntersect shape, IShading mat)
        {
            Shape = shape;
            Material = mat;
        }
        public IIntersect Shape { get; }
        public IShading Material { get; }

        public ShadingInfos GetShading(LocalGeo geo)
        {
            return Material.GetShading(geo);
        }

        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo)
        {
            return Shape.Intersect(ray, computeGeo, out geo);
        }
    }
}
