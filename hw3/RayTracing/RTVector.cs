﻿using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RTVector
    {
        public RTVector(double x, double y, double z)
        {
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z });
        }

        public RTVector(Vector<double> vector)
        {
            Vector = vector;
        }

        public static RTVector Zero => new RTVector(0, 0, 0);

        public RTVector Normalize()
        {
            return new RTVector(Vector.Normalize(1.0d));
        }

        public Vector<double> Vector { get; protected set; }

        public double X => Vector[0];
        public double Y => Vector[1];
        public double Z => Vector[2];

        public static RTVector CrossProduct(RTVector l, RTVector r)
        {
            return new RTVector(l.Y * r.Z - l.Z * r.Y,
                                -l.X * r.Z + l.Z * r.X,
                                l.X * r.Y - l.Y * r.X);
        }

        public static double DotProduct(RTVector v1, RTVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static RTVector operator *(double d, RTVector v)
        {
            return new RTVector(d * v.Vector);
        }

        public static RTVector operator +(RTVector v1, RTVector v2)
        {
            return new RTVector(v1.Vector + v2.Vector);
        }

        public static RTVector operator *(RTVector v, double d)
        {
            return d * v;
        }
    }
}
