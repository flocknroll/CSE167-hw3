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
        public Sphere(RTPoint center, float radius, Transformation transformation)
        {
            Center = center;
            Radius = radius;
            Transformation = transformation;

            _boundingBox = new BoundingBox(Transformation.ApplyTo(new RTPoint(Center.X - Radius, Center.Y - Radius, Center.Z - Radius)),
                                            Transformation.ApplyTo(new RTPoint(Center.X + Radius, Center.Y + Radius, Center.Z + Radius)));
        }

        public RTPoint Center { get; }
        public float Radius { get; }
        public Transformation Transformation { get; }
        private BoundingBox _boundingBox;

        public BoundingBox GetBoundingBox() => _boundingBox;

        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo, out float t)
        {
            RTPoint tPoint = Transformation.ApplyInverseTo(new RTPoint(ray.Point.X, ray.Point.Y, ray.Point.Z));
            RTVector tVec = Transformation.ApplyInverseTo(new RTVector(ray.Vector.X, ray.Vector.Y, ray.Vector.Z));

            RTVector ec = tPoint - Center;

            float a = RTVector.DotProduct(tVec, tVec);
            float b = 2.0f * RTVector.DotProduct(tVec, ec);
            float c = RTVector.DotProduct(ec, ec) - (float)Math.Pow(Radius, 2);

            float det = (float)Math.Pow(b, 2) - 4.0f * a * c;
            t = float.MaxValue;

            bool intersect = false;
            if (det == 0d)
            {
                intersect = true;
                t = -b / (2.0f * a);
            }
            else if (det > 0d)
            {
                float t1 = (-b + (float)Math.Sqrt(det)) / (2.0f * a);
                float t2 = (-b - (float)Math.Sqrt(det)) / (2.0f * a);

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
                    geo.Point = tPoint + t * tVec;
                    geo.Normal = new Normal(geo.Point - Center);

                    geo.Point = Transformation.ApplyTo(geo.Point);

                    geo.Normal = new Normal(Transformation.ApplyInverseTransposeTo(geo.Normal));
                }

                return true;
            }

            return false;
        }
    }
}
