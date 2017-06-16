using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Identity : ITransform
    {
        public Matrix4x4 Compute()
        {
            return Matrix4x4.Identity;
        }

        public Matrix4x4 ComputeInverse()
        {
            return Matrix4x4.Identity;
        }
    }
}
