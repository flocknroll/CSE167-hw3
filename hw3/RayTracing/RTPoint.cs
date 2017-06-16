using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace hw3
{
    public class RTPoint
    {
        public RTPoint(float x, float y, float z)
        {
            Vector = new Vector4( x, y, z, 1 );
        }
        public RTPoint(Vector4 vector)
        {
            Vector = new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }

        public RTPoint(RTPoint p)
        {
            Vector = new Vector4(p.X, p.Y, p.Z, p.W);
        }

        public static RTPoint Zero => new RTPoint(0, 0, 0);

        protected Vector4 Vector { get; set; }

        public float Length => Vector.Length();

        public float X => Vector.X;
        public float Y => Vector.Y;
        public float Z => Vector.Z;
        public float W => Vector.W;

        public static RTVector operator -(RTPoint p1, RTPoint p2)
        {
            return new RTVector(p1.Vector - p2.Vector);
        }

        public static RTPoint operator +(RTPoint p, RTVector v)
        {
            Vector4 temp = new Vector4(v.X, v.Y, v.Z, 0);
            return new RTPoint(p.Vector + temp);
        }
        public static RTPoint operator +(RTPoint p1, RTPoint p2)
        {
            return new RTPoint(p1.Vector + p2.Vector);
        }

        public static RTPoint operator -(RTPoint p, RTVector v)
        {
            Vector4 temp = new Vector4(v.X, v.Y, v.Z, 0);
            return new RTPoint(p.Vector - temp);
        }

        public static RTPoint operator /(RTPoint p, float d)
        {
            return new RTPoint(p.Vector / d);
        }

        public RTPoint ApplyMatrix(Matrix4x4 m)
        {
            return new RTPoint(Vector4.Transform(Vector, m));
        }
    }
}
