using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace hw3
{
    public class Identity : ITransform
    {
        public Matrix<double> Compute()
        {
            return Matrix<double>.Build.DenseIdentity(4);
        }

        public Matrix<double> ComputeInverse()
        {
            return Matrix<double>.Build.DenseIdentity(4);
        }
    }
}
