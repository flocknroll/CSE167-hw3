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
        public RTVector(double x, double y, double z)
        {
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z });
        }

        public RTVector(Vector<double> vector)
        {
            Vector = vector;
        }
        
        public RTVector Normalize()
        {
            return new RTVector(Vector.Normalize(2.0d));
        }

        public Vector<double> Vector { get; }

        public double X => Vector[0];
        public double Y => Vector[1];
        public double Z => Vector[2];

        public static RTVector CrossProduct(RTVector l, RTVector r)
        {
            return new RTVector(l.Y * r.Z - l.Z * r.Y,
                                -l.X * r.Z + l.Z * r.X,
                                l.X * r.Y - l.Y * r.X);
        }
    }
}
