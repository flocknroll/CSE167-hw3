using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace hw3
{
    public class Rotation : ITransform
    {
        public Rotation(RTVector axis, double degrees)
        {
            Axis = axis;
            Degrees = degrees;
        }

        public RTVector Axis { get; set; }
        public double Degrees { get; set; }

        private Matrix<double> ComputeMatrix(RTVector axisIn, double radians)
        {
            RTVector axis = axisIn.Normalize();
            Matrix<double> part1 = Matrix<double>.Build.DenseDiagonal(4, 4, Math.Cos(radians));
            part1[3, 3] = 1;

            Matrix<double> part2 = Matrix<double>.Build.DenseOfArray(new double[,] {{ Math.Pow(axis.X, 2), axis.X * axis.Y, axis.X * axis.Z, 0 },
                                                                                    { axis.X * axis.Y, Math.Pow(axis.Y, 2), axis.Y * axis.Z, 0 },
                                                                                    { axis.X * axis.Z, axis.Y * axis.Z, Math.Pow(axis.Z, 2), 0 },
                                                                                    { 0, 0, 0, 1 } })
                                   * (1.0d - Math.Cos(radians));

            Matrix<double> part3 = Matrix<double>.Build.DenseOfArray(new double[,] {{ 0, -axis.Z, axis.Y, 0 },
                                                                                    { axis.Z, 0, -axis.X, 0 },
                                                                                    { -axis.Y, axis.X, 0, 0 },
                                                                                    { 0, 0, 0, 1 } })
                                    * Math.Sin(radians);

            return part1 + part2 + part3;
        }

        public Matrix<double> Compute()
        {
            return ComputeMatrix(Axis, Degrees.ToRadians());
        }

        public Matrix<double> ComputeInverse()
        {
            return ComputeMatrix(Axis, -Degrees.ToRadians());
        }
    }
}
