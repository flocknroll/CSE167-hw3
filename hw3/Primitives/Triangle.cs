﻿using System;
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

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float minZ = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;
            float maxZ = float.MinValue;

            foreach (Vertex v in vertices)
            {
                RTPoint point = transformation.ApplyTo(v.Location);
                //point /= point.W;

                Normal normal = null;

                if (v.Normal != null)
                    normal = new Normal(transformation.ApplyTo(v.Normal));

                Vertices.Add(new Vertex(point, normal));

                if (point.X < minX)
                    minX = point.X;
                if (point.Y < minY)
                    minY = point.Y;
                if (point.Z < minZ)
                    minZ = point.Z;
                if (point.X > maxX)
                    maxX = point.X;
                if (point.Y > maxY)
                    maxY = point.Y;
                if (point.Z > maxZ)
                    maxZ = point.Z;
            }

            Normal = ComputeNormal();
            _boundingBox = new BoundingBox(minX, maxX, minY, maxY, minZ, maxZ);
        }

        public List<Vertex> Vertices { get; }
        public Normal Normal { get; private set; }

        private BoundingBox _boundingBox;
        public BoundingBox GetBoundingBox() => _boundingBox;

        public bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo, out float pos)
        {
            RTPoint A = Vertices[0].Location;
            RTPoint B = Vertices[1].Location;
            RTPoint C = Vertices[2].Location;

            RTVector p0 = new RTVector(ray.Point);
            pos = RTVector.DotProduct(A - ray.Point, Normal) / RTVector.DotProduct(ray.Vector, Normal);

            geo = new LocalGeo();
            if (pos >= ray.TMin && pos <= ray.TMax)
            {
                geo.Point = ray.Point + pos * ray.Vector;
                geo.Normal = Normal;

                RTVector u = B - A;
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

                float denom = uCrossV.Length;
                float r = vCrossW.Length / denom;
                float t = uCrossW.Length / denom;

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

                res = new Normal(RTVector.CrossProduct(U, V));
            }

            return res;
        }
    }
}
