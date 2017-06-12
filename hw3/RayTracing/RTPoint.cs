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
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z });
        }
        public RTPoint(Vector<double> vector)
        {
            Vector = vector;
        }

        public Vector<double> Vector { get; }

        public double X => Vector[0];
        public double Y => Vector[1];
        public double Z => Vector[2];

        public static RTVector operator -(RTPoint p1, RTPoint p2)
        {
            return new RTVector(p1.Vector - p2.Vector);
        }

        public static RTPoint operator +(RTPoint p, RTVector v)
        {
            return new RTPoint(p.Vector + v.Vector);
        }

        public static RTPoint operator -(RTPoint p, RTVector v)
        {
            return new RTPoint(p.Vector - v.Vector);
        }
    }
}
