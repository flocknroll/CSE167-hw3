using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Transformation
    {
        public Transformation(Matrix<double> matrix)
        {
            Matrix = matrix;
        }

        public Matrix<double> Matrix { get; }
        public Matrix<double> MInvT => Matrix.Inverse().Transpose();
    }
}
