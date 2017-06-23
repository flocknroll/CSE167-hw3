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
            BBNode root = new BBNode(new BoundingBox());

            foreach (IPrimitive p in primitiveList)
            {
                root.AddPrimitive(p);
            }

            return root;
        }

        public bool Hit(Ray ray, out BBNode hitNode)
        {
            return _root.Hit(ray, out hitNode);
        }
    }
}
