using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Scaling : ITransform
    {
        public Scaling(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Matrix4x4 Compute()
        {
            return Matrix4x4.CreateScale(X, Y, Z);
        }

        public Matrix4x4 ComputeInverse()
        {
            return Matrix4x4.CreateScale(1f / X, 1f / Y, 1f / Z);
        }
    }
}
