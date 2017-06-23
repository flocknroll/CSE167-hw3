using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class BBTree
    {
        public BBTree()
        {

        }

        public BBTree(IList<IPrimitive> primitiveList)
        {
            _root = Compute(primitiveList);
        }

        private BBNode _root;

        public static BBTree ComputeTree(IList<IPrimitive> primitiveList)
        {
            return new BBTree(primitiveList);
        }

        private static BBNode Compute(IList<IPrimitive> primitiveList)
        {
            // TODO : trier primitives
            BBNode root = new BBNode();

            root.AddPrimitives(primitiveList, 0);

            return root;
        }

        public IList<HitResult> Hit(Ray ray)
        {
            IList<HitResult> hit = new List<HitResult>();

            _root.Hit(ray, ref hit);

            return hit;
        }
    }
}
