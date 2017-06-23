using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class BBNode
    {
        public BBNode(IPrimitive primitive)
        {
            Box = primitive.GetBoundingBox();
            Primitive = primitive;
        }

        public BBNode()
        {
        }

        public BoundingBox Box { get; private set; }
        public IPrimitive Primitive { get; private set; }

        public BBNode Left { get; set; }
        public BBNode Right { get; set; }

        public void AddPrimitives(IList<IPrimitive> list, int depth)
        {

            if (list.Count == 1)
            {
                Left = new BBNode(list[0]);
                Box = list[0].GetBoundingBox();
            }
            else if (list.Count == 2)
            {
                Left = new BBNode(list[0]);
                Right = new BBNode(list[1]);
                Box = BoundingBox.Merge(list[0].GetBoundingBox(), list[1].GetBoundingBox());
            }
            else
            {
                int k = 3; // 3 dimensions

                int axis = depth % k;
                int median = list.Count / 2;

                IList<IPrimitive> axisSorted = list.OrderBy(p => p.GetBoundingBox().Middle(axis)).ToList();

                Box = BoundingBox.MergeAll(list);

                IList<IPrimitive> left = list.Take(median).ToList();
                IList<IPrimitive> right = list.Skip(median).ToList();

                if (left.Any())
                {
                    Left = new BBNode();
                    Left.AddPrimitives(left, depth + 1);
                }
                if (right.Any())
                {
                    Right = new BBNode();
                    Right.AddPrimitives(right, depth + 1);
                }
            }
        }

        public void Hit(Ray ray, ref IList<HitResult> list)
        {

            if (Primitive != null)
            {
                LocalGeo geo;
                float t;
                if (Primitive.Intersect(ray, true, out geo, out t))
                {
                    list.Add(new HitResult { Primitive = Primitive, Geo = geo, T = t });
                }
            }
            else if (Box.Hit(ray))
            {
                Left?.Hit(ray, ref list);
                Right?.Hit(ray, ref list);
            }
        }
    }
}
