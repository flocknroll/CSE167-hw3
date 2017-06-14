using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Normal : RTVector
    {
        public Normal(double x, double y, double z) : base(x, y, z)
        {
            Vector = Normalize().Vector;
        }

        public Normal(Vector<double> vec) : base(vec)
        {
            Vector = Normalize().Vector;
        }

        public Normal(RTVector vec) : base(vec)
        {
            Vector = Normalize().Vector;
        }
    }
}
