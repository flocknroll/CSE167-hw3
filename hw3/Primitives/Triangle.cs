using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Triangle : IIntersect
    {
        public Triangle(List<Vertex> vertices, Transformation transformation)
        {
            Vertices = new List<Vertex>();
            foreach (Vertex v in vertices)
            {
                RTPoint point = transformation * v.Location;
                //point /= point.W;

                Normal normal = null;

                if (v.Normal != null)
                    normal = new Normal(v.Normal * transformation);

                Vertices.Add(new Vertex(point, normal));
            }

            Normal = ComputeNormal();
        }

        public List<Vertex> Vertices { get; }
        public Normal Normal { get; private set; }

        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo, out double pos)
        {
            RTPoint A = Vertices[0].Location;
            RTPoint B = Vertices[1].Location;
            RTPoint C = Vertices[2].Location;

            RTVector u = B - A;

            RTVector p0 = new RTVector(ray.Point.Vector);
            pos = (RTVector.DotProduct(u, Normal) - RTVector.DotProduct(p0, Normal)) / RTVector.DotProduct(ray.Vector, Normal);

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

        private Normal ComputeNormal()
        {
            Normal res;
            bool normVertex = Vertices[0].Normal != null;

            if (normVertex)
            {
                // TODO
                res = new Normal(0, 1, 0);
            }
            else
            {
                RTPoint A = Vertices[0].Location;
                RTPoint B = Vertices[1].Location;
                RTPoint C = Vertices[2].Location;

                RTVector U = B - A;
                RTVector V = C - A;

                res = new Normal(RTVector.CrossProduct(V, U).Vector);
            }

            return res;
        }
    }
}
