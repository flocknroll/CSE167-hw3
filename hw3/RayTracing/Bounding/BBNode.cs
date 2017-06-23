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

        public BBNode(BoundingBox box)
        {
            Box = box;
            Primitive = null;
        }

        public BoundingBox Box { get; private set; }
        public IPrimitive Primitive { get; private set; }

        public BBNode Left { get; set; }
        public BBNode Right { get; set; }

        public void AddPrimitive(IPrimitive p)
        {
            BoundingBox pBox = p.GetBoundingBox();

            if (Primitive != null)
            {
                // TODO : trouver un tri correct
                if (pBox.Min.X < Box.Min.X)
                {
                    Left = new BBNode(p);
                    Right = new BBNode(Primitive);
                    
                }
                else
                {
                    Left = new BBNode(Primitive);
                    Right = new BBNode(p);
                }
                Box = BoundingBox.Merge(pBox, Box);
                Primitive = null;
            }
            else
            {
                if (Left == null && Right == null)
                {
                    Primitive = p;
                    Box = pBox;
                }
                else
                {
                    // TODO : trouver un tri correct
                    if (pBox.Min.X < Box.Min.X)
                    {
                        Left.AddPrimitive(p);
                    }
                    else
                    {
                        Right.AddPrimitive(p);
                    }
                    Box = BoundingBox.Merge(pBox, Box);
                }
            }
        }

        public bool Hit(Ray ray, out BBNode hitNode)
        {
            bool hit = false;
            hitNode = null;

            if (!Box.Hit(ray))
                return hit;
            
            if (Left == null && Right == null)
            {
                if (Box.Hit(ray))
                {
                    hit = true;
                    hitNode = this;
                }
            }
            else
            {
                if (Left != null && Left.Hit(ray, out hitNode))
                {
                    hit = true;
                }

                if (!hit && Right != null && Left.Hit(ray, out hitNode))
                {
                    hit = true;
                }
            }

            return hit;
        }
    }
}
