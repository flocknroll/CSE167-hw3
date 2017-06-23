using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class BoundingBox
    {
        public BoundingBox()
        {
            Min = new RTPoint(0f, 0f, 0f);
            Max = new RTPoint(0f, 0f, 0f);
        }

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

        public static BoundingBox Merge(BoundingBox b1, BoundingBox b2)
        {
            float xMin = Extensions.Min(Extensions.Min(b1.Min.X, b1.Max.X), Extensions.Min(b2.Min.X, b2.Max.X));
            float yMin = Extensions.Min(Extensions.Min(b1.Min.Y, b1.Max.Y), Extensions.Min(b2.Min.Y, b2.Max.Y));
            float zMin = Extensions.Min(Extensions.Min(b1.Min.Z, b1.Max.Z), Extensions.Min(b2.Min.Z, b2.Max.Z));

            float xMax = Extensions.Max(Extensions.Max(b1.Min.X, b1.Max.X), Extensions.Max(b2.Min.X, b2.Max.X));
            float yMax = Extensions.Max(Extensions.Max(b1.Min.Y, b1.Max.Y), Extensions.Max(b2.Min.Y, b2.Max.Y));
            float zMax = Extensions.Max(Extensions.Max(b1.Min.Z, b1.Max.Z), Extensions.Max(b2.Min.Z, b2.Max.Z));

            return new BoundingBox(xMin, xMax, yMin, yMax, zMin, zMax);
        }

        public static BoundingBox MergeAll(IList<IPrimitive> list)
        {
            BoundingBox box = new BoundingBox();

            foreach (IIntersect i in list)
                box = Merge(box, i.GetBoundingBox());

            return box;
        }

        public bool Hit(Ray ray)
        {
            float tmin, tmax, tymin, tymax, tzmin, tzmax;

            tmin = (this[ray.Signs[0]].X - ray.Point.X) * ray.InvDir.X;
            tmax = (this[1 - ray.Signs[0]].X - ray.Point.X) * ray.InvDir.X;
            tymin = (this[ray.Signs[1]].Y - ray.Point.Y) * ray.InvDir.Y;
            tymax = (this[1 - ray.Signs[1]].Y - ray.Point.Y) * ray.InvDir.Y;

            if ((tmin > tymax) || (tymin > tmax))
                return false;

            if (tymin > tmin)
                tmin = tymin;
            if (tymax < tmax)
                tmax = tymax;

            tzmin = (this[ray.Signs[2]].Z - ray.Point.Z) * ray.InvDir.Z;
            tzmax = (this[1 - ray.Signs[2]].Z - ray.Point.Z) * ray.InvDir.Z;

            if ((tmin > tzmax) || (tzmin > tmax))
                return false;

            if (tzmin > tmin)
                tmin = tzmin;
            if (tzmax < tmax)
                tmax = tzmax;

            return true;
        }

        public float MiddleX => (Max.X - Min.X) / 2f;
        public float MiddleY => (Max.Y - Min.Y) / 2f;
        public float MiddleZ => (Max.Z - Min.Z) / 2f;

        public float Middle(int axis)
        {
            switch (axis)
            {
                case 0:
                    return MiddleX;

                case 1:
                    return MiddleY;

                case 3:
                
                    return MiddleZ;

                default:
                    return 0f;
            }
        }

        public float Length => (Max - Min).Length;
    }
}
