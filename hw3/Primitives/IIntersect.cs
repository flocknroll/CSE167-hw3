using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public interface IIntersect
    {
        bool Intersect(Ray ray, bool computeGeo, out LocalGeo geo, out float t);
        BoundingBox GetBoundingBox();
    }
}
