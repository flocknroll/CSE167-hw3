using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Translation : ITransform
    {
        public Translation(RTVector vector)
        {
            Vector = vector;
        }

        public RTVector Vector { get; set; }

        public Matrix4x4 Compute()
        {
            return Matrix4x4.CreateTranslation(Vector.X, Vector.Y, Vector.Z);
        }

        public Matrix4x4 ComputeInverse()
        {
            return Matrix4x4.CreateTranslation(-Vector.X, -Vector.Y, -Vector.Z);
        }
    }
}
