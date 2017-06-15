using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace hw3
{
    public class Scaling : ITransform
    {
        public Scaling(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Matrix<double> Compute()
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 0] = X;
            mat[1, 1] = Y;
            mat[2, 2] = Z;

            return mat;
        }

        public Matrix<double> ComputeInverse()
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 0] = 1.0d / X;
            mat[1, 1] = 1.0d / Y;
            mat[2, 2] = 1.0d / Z;

            return mat;
        }
    }
}
