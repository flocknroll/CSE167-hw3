using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public static class Extensions
    {
        public static float ToRadians(this float deg)
        {
            return deg * (float)Math.PI / 180.0f;
        }

        public static float Min(float f1, float f2)
        {
            if (f1 < f2)
                return f1;

            return f2;
        }

        public static float Max(float f1, float f2)
        {
            if (f1 > f2)
                return f1;

            return f2;
        }
    }
}
