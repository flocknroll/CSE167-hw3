using MathNet.Numerics.LinearAlgebra;
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

        public Vertex(double x, double y, double z)
        {
            Location = new RTPoint(x, y, z);
            Normal = null;
        }

        public Vertex(RTPoint vertex, RTVector normal)
        {
            Location = vertex;
            Normal = new Normal(normal.Vector);
        }

        public Vertex(double x, double y, double z, double nx, double ny, double nz)
        {
            Location = new RTPoint(x, y, z);
            Normal = new Normal(nx, ny, nz);
        }

        public RTPoint Location { get; }
        public Normal Normal { get; }
    }
}
