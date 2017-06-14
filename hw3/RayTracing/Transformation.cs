using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Transformation
    {
        public Transformation(Transformation t)
        {
            Matrix = t.Matrix;
        }

        public Transformation(Matrix<double> matrix)
        {
            Matrix = matrix;
        }

        public Matrix<double> Matrix { get; }
        public Matrix<double> MInvT => Matrix.Inverse().Transpose();

        public static Transformation Identity()
        {
            return new Transformation(Matrix<double>.Build.DenseIdentity(4));
        }

        public static Transformation Translate(RTVector vec)
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 3] = vec.X;
            mat[1, 3] = vec.Y;
            mat[2, 3] = vec.Z;

            return new Transformation(mat);
        }

        public static Transformation Rotate(RTVector vec, double degrees)
        {
            double rad = degrees.ToRadians();

            Matrix<double> part1 = Matrix<double>.Build.DenseDiagonal(4, 4, Math.Cos(rad));

            Matrix<double> part2 = Matrix<double>.Build.DenseOfArray(new double[,] {{ Math.Pow(vec.X, 2), vec.X * vec.Y, vec.X * vec.Z, 0 },
                                                                                    { vec.X * vec.Y, Math.Pow(vec.Y, 2), vec.Y * vec.Z, 0 },
                                                                                    { vec.X * vec.Z, vec.Y * vec.Z, Math.Pow(vec.Z, 2), 0 },
                                                                                    { 0, 0, 0, 1 } })
                                   * (1.0d - Math.Cos(rad));

            Matrix<double> part3 = Matrix<double>.Build.DenseOfArray(new double[,] {{ 0, -vec.Z, vec.Y, 0 },
                                                                                    { vec.Z, 0, -vec.X, 0 },
                                                                                    { -vec.Y, vec.X, 0, 0 },
                                                                                    { 0, 0, 0, 1 } })
                                    * Math.Sin(rad);

            return new Transformation(part1 + part2 + part3);
        }

        public static Transformation Scale(double x, double y, double z)
        {
            Matrix<double> mat = Matrix<double>.Build.DenseIdentity(4);

            mat[0, 0] = x;
            mat[1, 1] = y;
            mat[2, 2] = z;

            return new Transformation(mat);
        }

        public static Transformation operator *(Transformation left, Transformation right)
        {
            return new Transformation(left.Matrix * right.Matrix);
        }

        public static RTPoint operator *(RTPoint left, Transformation right)
        {
            return new RTPoint(left.Vector * right.Matrix);
        }

        public static RTPoint operator *(Transformation left, RTPoint right)
        {
            return new RTPoint(left.Matrix * right.Vector);
        }

        public static RTVector operator *(RTVector left, Transformation right)
        {
            return new RTVector(left.Vector * right.Matrix);
        }
    }
}
