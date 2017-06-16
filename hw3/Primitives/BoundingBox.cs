using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class BoundingBox
    {
        public BoundingBox(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            Min = new RTPoint(xMin, yMin, zMin);
            Max = new RTPoint(xMax, yMax, zMax);
        }

        public BoundingBox(RTPoint min, RTPoint max)
        {
            Min = min;
            Max = max;
        }

        public RTPoint Min { get; }
        public RTPoint Max { get; }

        public RTPoint this[int i]
        {
            get
            {
                if (i == 0)
                {
                    return Min;
                }
                else
                {
                    return Max;
                }
            }
        }
    }
}
