using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class HitResult
    {
        public IPrimitive Primitive { get; set; }
        public LocalGeo Geo { get; set; }
        public float T { get; set; }
    }
}
