using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Normal
    {
        public Normal(double x, double y, double z)
        {
            Vector = Vector<double>.Build.Dense(new double[] { x, y, z });
        }

        public Vector<double> Vector { get; }
    }
}
