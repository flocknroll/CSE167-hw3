using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public static class Extensions
    {
        public static double ToRadians(this double deg)
        {
            return deg * Math.PI / 180.0d;
        }
    }
}
