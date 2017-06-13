using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Sphere : IIntersect
    {
        public Sphere(RTPoint center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public RTPoint Center { get; }
        public double Radius { get; }


        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo)
        {
            RTVector ec = ray.Point - Center;

            double a = RTVector.DotProduct(ray.Vector, ray.Vector);
            double b = RTVector.DotProduct(2.0d * ray.Vector, ec);
            double c = RTVector.DotProduct(ec, ec) - Math.Pow(Radius, 2);

            double det = Math.Pow(b, 2) - 4.0d * a * c;
            double t = -1d;

            if (det == 0d)
            {
                t = -b / (2.0d * a);
            }
            else if (det > 0d)
            {
                double t1 = (-b + Math.Sqrt(det)) / (2.0d * a);
                double t2 = (-b - Math.Sqrt(det)) / (2.0d * a);

                if ((t1 > 0 && t2 < 0) || (t1 < 0 && t2 > 0))
                {
                    if (t1 > 0)
                        t = t1;
                    else
                        t = t2;
                }
                else if (t1 > 0 && t2 > 0)
                {
                    if (t1 > t2)
                        t = t1;
                    else
                        t = t2;
                }
            }

            geo = new LocalGeo();
            if (t >= ray.TMin && t <= ray.TMax)
            {
                if (computeGeo)
                {
                    geo.Point = ray.Point + t * ray.Vector;
                    geo.Normal = new Normal(geo.Point.Vector - Center.Vector);
                }

                return true;
            }

            return false;
        }
    }
}
