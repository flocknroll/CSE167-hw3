using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RayTracer
    {
        public RayTracer()
        {
            Shapes = new List<IShape>();
        }

        public IList<IShape> Shapes;

        public Color Trace(Ray ray)
        {
            Color res = Color.FromArgb(25, 25, 25);

            foreach (IShape shape in Shapes)
            {
                if (shape.Intersect(ray))
                {
                    res = Color.Red;
                }
            }

            return res;
        }
    }
}
