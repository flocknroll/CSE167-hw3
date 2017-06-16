using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public interface ITransform
    {
        Matrix4x4 Compute();

        Matrix4x4 ComputeInverse();
    }
}
