using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Sphere : IShape
    {
        public Sphere(RTPoint center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public RTPoint Center { get; }
        public double Radius { get; }

        public bool Intersect(Ray ray)
        {
            RTVector ec = ray.Point - Center;

            double a = RTVector.DotProduct(ray.Vector, ray.Vector);
            double b = RTVector.DotProduct(2.0d * ray.Vector, ec);
            double c = RTVector.DotProduct(ec, ec) - Math.Pow(Radius, 2);

            double det = Math.Pow(b, 2) - 4.0d * a * c;

            return det >= 0;
        }
    }
}
