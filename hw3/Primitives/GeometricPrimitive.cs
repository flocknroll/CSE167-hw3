using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class GeometricPrimitive : IPrimitive
    {
        public GeometricPrimitive(IIntersect shape, Material mat, Transformation trans)
        {
            Shape = shape;
            Material = mat;
            Transformation = trans;
        }
        public IIntersect Shape { get; }
        public Material Material { get; }
        public Transformation Transformation { get; }

        public ShadingInfos GetShading(LocalGeo geo)
        {
            return Material.Properties;
        }

        public bool Intersect(Ray ray, out LocalGeo geo)
        {
            return Shape.Intersect(ray, out geo);
        }
    }
}
