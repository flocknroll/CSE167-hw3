using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Normal : RTVector
    {
        public Normal(float x, float y, float z) : base(x, y, z)
        {
            Vector4 norm = Vector4.Normalize(new Vector4(x, y, z, 0));

            Vector = norm;
        }

        public Normal(Vector4 vec) : base(vec)
        {
            Vector4 norm = Vector4.Normalize(new Vector4(vec.X, vec.Y, vec.Z, 0));

            Vector = norm;
        }

        public Normal(RTVector vec) : base(vec)
        {
            Vector4 norm = Vector4.Normalize(new Vector4(vec.X, vec.Y, vec.Z, 0));

            Vector = norm;
        }
    }
}
