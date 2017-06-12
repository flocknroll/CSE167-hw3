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
            throw new NotImplementedException();
        }
    }
}
