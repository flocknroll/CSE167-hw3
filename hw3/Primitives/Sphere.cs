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
        public Sphere(RTPoint center, double radius, Transformation transformation)
        {
            Center = center;
            Radius = radius;
            Transformation = transformation;
        }

        public RTPoint Center { get; }
        public double Radius { get; }
        public Transformation Transformation { get; }


        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo, out double t)
        {
            RTVector ec = ray.Point - Center;
            RTVector tVec = Transformation * new RTVector(ray.Vector.X, ray.Vector.Y, ray.Vector.Z, 1);
            //tVec /= tVec.W;

            double a = RTVector.DotProduct(tVec, tVec);
            double b = 2.0d * RTVector.DotProduct(tVec, ec);
            double c = RTVector.DotProduct(ec, ec) - Math.Pow(Radius, 2);

            double det = Math.Pow(b, 2) - 4.0d * a * c;
            t = double.MaxValue;

            bool intersect = false;
            if (det == 0d)
            {
                intersect = true;
                t = -b / (2.0d * a);
            }
            else if (det > 0d)
            {
                double t1 = (-b + Math.Sqrt(det)) / (2.0d * a);
                double t2 = (-b - Math.Sqrt(det)) / (2.0d * a);

                if ((t1 > 0 && t2 < 0) || (t1 < 0 && t2 > 0))
                {
                    intersect = true;
                    if (t1 > 0)
                        t = t1;
                    else
                        t = t2;
                }
                else if (t1 > 0 && t2 > 0)
                {
                    intersect = true;
                    if (t1 < t2)
                        t = t1;
                    else
                        t = t2;
                }
            }

            geo = new LocalGeo();
            if (intersect && t >= ray.TMin && t <= ray.TMax)
            {
                if (computeGeo)
                {
                    geo.Point = ray.Point + t * tVec;
                    geo.Normal = new Normal(geo.Point - Center);
                }

                return true;
            }

            return false;
        }
    }
}
