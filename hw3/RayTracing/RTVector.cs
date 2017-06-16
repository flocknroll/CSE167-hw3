using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RTVector
    {
        public RTVector(float x, float y, float z)
        {
            Vector = new Vector4(x, y, z, 0);
        }

        public RTVector(Vector4 vector)
        {
            Vector = new Vector4(vector.X, vector.Y, vector.Z, 0);
        }

        public RTVector(RTVector vector)
        {
            Vector = new Vector4(vector.X, vector.Y, vector.Z, 0);
        }

        public RTVector(RTPoint p)
        {
            Vector = new Vector4(p.X, p.Y, p.Z, 0);
        }

        public static RTVector Zero => new RTVector(0, 0, 0);

        public RTVector Normalize()
        {
            return new RTVector(Vector4.Normalize(Vector));
        }

        public float Length => Vector.Length();

        protected Vector4 Vector { get; set; }

        public float X => Vector.X;
        public float Y => Vector.Y;
        public float Z => Vector.Z;

        public static RTVector CrossProduct(RTVector l, RTVector r)
        {
            return new RTVector(l.Y * r.Z - l.Z * r.Y,
                                -l.X * r.Z + l.Z * r.X,
                                l.X * r.Y - l.Y * r.X);
        }

        public static float DotProduct(RTVector v1, RTVector v2)
        {
            return Vector4.Dot(v1.Vector, v2.Vector);
        }

        public static RTVector operator *(float d, RTVector v)
        {
            return new RTVector(d * v.Vector);
        }

        public static RTVector operator +(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector + v2.Vector);
        }

        public static RTVector operator -(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector - v2.Vector);
        }

        public static RTVector operator *(RTVector v, float d)
        {
            return d * v;
        }

        public static RTVector operator /(RTVector v, float d)
        {
            return new RTVector(v.Vector / d);
        }

        public static RTVector operator /(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector / v2.Vector);
        }

        public static RTVector operator -(RTVector v)
        {
            return new RTVector(-v.Vector);
        }

        public RTVector ApplyMatrix(Matrix4x4 m)
        {
            return new RTVector(Vector4.Transform(Vector, m));
        }
    }
}
