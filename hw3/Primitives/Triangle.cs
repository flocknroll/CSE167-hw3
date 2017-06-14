using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Triangle : IIntersect
    {
        public Triangle(List<Vertex> v)
        {
            Vertex = v;
            Normal = ComputeNormal(v[0].Normal != null);
        }

        public List<Vertex> Vertex { get; }
        public Normal Normal { get; private set; }

        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo)
        {
            RTPoint A = Vertex[0].Location;
            RTPoint B = Vertex[1].Location;
            RTPoint C = Vertex[2].Location;

            RTVector u = B - A;

            RTVector p0 = new RTVector(ray.Point.Vector);
            double pos = (RTVector.DotProduct(u, Normal) - RTVector.DotProduct(p0, Normal)) / RTVector.DotProduct(ray.Vector, Normal);

            geo = new LocalGeo();
            if (pos >= ray.TMin && pos <= ray.TMax)
            {
                geo.Point = ray.Point + pos * ray.Vector;
                geo.Normal = Normal;

                RTVector v = C - A;
                RTVector w = geo.Point - A;

                RTVector vCrossW = RTVector.CrossProduct(v, w);
                RTVector vCrossU = RTVector.CrossProduct(v, u);

                if (RTVector.DotProduct(vCrossW, vCrossU) < 0)
                    return false;

                RTVector uCrossW = RTVector.CrossProduct(u, w);
                RTVector uCrossV = RTVector.CrossProduct(u, v);

                if (RTVector.DotProduct(uCrossW, uCrossV) < 0)
                    return false;

                double denom = uCrossV.Vector.L1Norm();
                double r = vCrossW.Vector.L1Norm() / denom;
                double t = uCrossW.Vector.L1Norm() / denom;

                return r + t <= 1;
            }

            return false;
        }

        private Normal ComputeNormal(bool normVertex)
        {
            Normal res;


            if (normVertex)
            {
                // TODO
                res = new Normal(0, 1, 0);
            }
            else
            {
                RTPoint A = Vertex[0].Location;
                RTPoint B = Vertex[1].Location;
                RTPoint C = Vertex[2].Location;

                RTVector U = B - A;
                RTVector V = C - A;

                res = new Normal(RTVector.CrossProduct(V, U).Vector);
            }

            return res;
        }
    }
}
