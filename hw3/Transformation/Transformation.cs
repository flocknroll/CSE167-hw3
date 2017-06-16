using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Transformation
    {
        public Transformation()
        {
            Transforms = new List<ITransform>();
        }

        public Transformation(Transformation oldTrans)
        {
            Transforms = new List<ITransform>(oldTrans.Transforms);
        }

        public IList<ITransform> Transforms { get; }

        public void AddTransform(ITransform transform)
        {
            Transforms.Add(transform);
        }

        protected Matrix4x4 Compute()
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            foreach (ITransform transform in Transforms)
            {
                matrix = transform.Compute() * matrix;
            }

            return matrix;
        }

        protected Matrix4x4 ComputeInverse()
        {
            Matrix4x4 matrix;

            if (!Matrix4x4.Invert(Compute(), out matrix))
            {
                matrix = Matrix4x4.Identity;

                foreach (ITransform transform in Transforms.Reverse())
                {
                    matrix = transform.ComputeInverse() * matrix;
                }
            }

            return matrix;
        }

        public RTVector ApplyTo(RTVector v)
        {
            return v.ApplyMatrix(Compute());
        }

        public RTPoint ApplyTo(RTPoint p)
        {
            return p.ApplyMatrix(Compute());
        }

        public RTVector ApplyInverseTo(RTVector v)
        {
            return v.ApplyMatrix(ComputeInverse());
        }

        public RTPoint ApplyInverseTo(RTPoint p)
        {
            return p.ApplyMatrix(ComputeInverse());
        }

        public RTVector ApplyInverseTransposeTo(RTVector v)
        {
            return v.ApplyMatrix(Matrix4x4.Transpose(ComputeInverse()));
        }
    }
}
