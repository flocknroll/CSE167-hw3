using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RTVector
    {
        public RTVector(double x, double y, double z, double w = 0)
        {
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z, w });
        }

        public RTVector(Vector<double> vector)
        {
            Vector = vector;
        }

        public RTVector(RTVector vector)
        {
            Vector = vector.Vector;
        }

        public static RTVector Zero => new RTVector(0, 0, 0, 0);

        public RTVector Normalize()
        {
            return new RTVector(Vector / Length());
        }

        public double Length()
        {
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
        }

        public Vector<double> Vector { get; protected set; }

        public double X => Vector[0];
        public double Y => Vector[1];
        public double Z => Vector[2];
        public double W => Vector[3];

        public static RTVector CrossProduct(RTVector l, RTVector r)
        {
            return new RTVector(l.Y * r.Z - l.Z * r.Y,
                                -l.X * r.Z + l.Z * r.X,
                                l.X * r.Y - l.Y * r.X,
                                0);
        }

        public static double DotProduct(RTVector v1, RTVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static RTVector operator *(double d, RTVector v)
        {
            return new RTVector(d * v.Vector);
        }

        public static RTVector operator *(Transformation t, RTVector v)
        {
            return new RTVector(t.Matrix * v.Vector);
        }

        public static RTVector operator +(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector + v2.Vector);
        }

        public static RTVector operator -(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector - v2.Vector);
        }

        public static RTVector operator *(RTVector v, double d)
        {
            return d * v;
        }

        public static RTVector operator /(RTVector v, double d)
        {
            return new RTVector(v.Vector / d);
        }

        public static RTVector operator -(RTVector v)
        {
            return new RTVector(-v.Vector);
        }
    }
}
