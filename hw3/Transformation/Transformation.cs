using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Transformation: ITransform
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

        public Matrix<double> Compute()
        {
            Matrix<double> matrix = new Identity().Compute();

            foreach (ITransform transform in Transforms)
            {
                matrix *= transform.Compute();
            }

            return matrix;
        }

        public Matrix<double> ComputeInverse()
        {
            Matrix<double> matrix = new Identity().Compute();

            foreach (ITransform transform in Transforms.Reverse())
            {
                matrix *= transform.ComputeInverse();
            }

            return matrix;

            //return Compute().Inverse();
        }

        public RTVector ApplyTo(RTVector v)
        {
            return new RTVector(Compute() * v.Vector);
        }

        public RTPoint ApplyTo(RTPoint v)
        {
            return new RTPoint(Compute() * v.Vector);
        }

        public RTVector ApplyInverseTo(RTVector v)
        {
            return new RTVector(ComputeInverse() * v.Vector);
        }

        public RTPoint ApplyInverseTo(RTPoint v)
        {
            return new RTPoint(ComputeInverse() * v.Vector);
        }
    }
}
