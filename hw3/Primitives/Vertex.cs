using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Vertex
    {
        public Vertex(RTPoint vertex)
        {
            Location = vertex;
            Normal = null;
        }

        public Vertex(float x, float y, float z)
        {
            Location = new RTPoint(x, y, z);
            Normal = null;
        }

        public Vertex(RTPoint vertex, RTVector normal)
        {
            Location = vertex;
            if (normal != null)
                Normal = new Normal(normal);
        }

        public Vertex(float x, float y, float z, float nx, float ny, float nz)
        {
            Location = new RTPoint(x, y, z);
            Normal = new Normal(nx, ny, nz);
        }

        public RTPoint Location { get; }
        public Normal Normal { get; }
    }
}
