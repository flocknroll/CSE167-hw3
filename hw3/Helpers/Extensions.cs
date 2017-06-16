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
    }
}
