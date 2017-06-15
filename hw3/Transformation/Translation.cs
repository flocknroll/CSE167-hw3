using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace hw3
{
    public class Translation : ITransform
    {
        public Translation(RTVector vector)
        {
            Vector = vector;
        }

        public RTVector Vector { get; set; }

        public Matrix<double> Compute()
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 3] = Vector.X;
            mat[1, 3] = Vector.Y;
            mat[2, 3] = Vector.Z;

            return mat;
        }

        public Matrix<double> ComputeInverse()
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 3] = -Vector.X;
            mat[1, 3] = -Vector.Y;
            mat[2, 3] = -Vector.Z;

            return mat;
        }
    }
}
