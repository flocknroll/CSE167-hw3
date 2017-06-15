using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public interface ITransform
    {
        Matrix<double> Compute();

        Matrix<double> ComputeInverse();
    }
}
