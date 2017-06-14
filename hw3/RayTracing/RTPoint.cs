using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace hw3
{
    public class RTPoint
    {
        public RTPoint(double x, double y, double z)
        {
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z, 1 });
        }
        public RTPoint(Vector<double> vector)
        {
            Vector = vector;
        }

        public static RTPoint Zero => new RTPoint(0, 0, 0);

        public Vector<double> Vector { get; }

        public double X => Vector[0];
        public double Y => Vector[1];
        public double Z => Vector[2];
        public double W => Vector[3];

        public static RTVector operator -(RTPoint p1, RTPoint p2)
        {
            return new RTVector(p1.Vector - p2.Vector);
        }

        public static RTPoint operator +(RTPoint p, RTVector v)
        {
            return new RTPoint(p.Vector + v.Vector);
        }
        public static RTPoint operator +(RTPoint p1, RTPoint p2)
        {
            return new RTPoint(p1.Vector + p2.Vector);
        }

        public static RTPoint operator -(RTPoint p, RTVector v)
        {
            return new RTPoint(p.Vector - v.Vector);
        }

        public static RTPoint operator /(RTPoint p, double d)
        {
            return new RTPoint(p.Vector / d);
        }
    }
}
