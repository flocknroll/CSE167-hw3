using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class GeometricPrimitive : IPrimitive
    {
        public GeometricPrimitive(IIntersect shape, IShading mat, Transformation trans)
        {
            Shape = shape;
            Material = mat;
            Transformation = trans;
        }
        public IIntersect Shape { get; }
        public IShading Material { get; }
        public Transformation Transformation { get; }

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
