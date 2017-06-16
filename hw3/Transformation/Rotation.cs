using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace hw3
{
    public class Rotation : ITransform
    {
        public Rotation(RTVector axis, float degrees)
        {
            Axis = axis;
            Degrees = degrees;
        }

        public RTVector Axis { get; set; }
        public float Degrees { get; set; }

        private Matrix4x4 ComputeMatrix(RTVector axisIn, float radians)
        {
            RTVector axis = axisIn.Normalize();
            Matrix4x4 part1 = Matrix4x4.Multiply(Matrix4x4.Identity, (float)Math.Cos(radians));
            part1.M44 = 1;

            Matrix4x4 part2 = Matrix4x4.Multiply(new Matrix4x4((float)Math.Pow(axis.X, 2), axis.X * axis.Y, axis.X * axis.Z, 0,
                                              axis.X * axis.Y, (float)Math.Pow(axis.Y, 2), axis.Y * axis.Z, 0,
                                              axis.X * axis.Z, axis.Y * axis.Z, (float)Math.Pow(axis.Z, 2), 0,
                                              0, 0, 0, 1),
                                   1.0f - (float)Math.Cos(radians));

            Matrix4x4 part3 = Matrix4x4.Multiply(new Matrix4x4(0, -axis.Z, axis.Y, 0,
                                            axis.Z, 0, -axis.X, 0,
                                            -axis.Y, axis.X, 0, 0,
                                            0, 0, 0, 1),
                                    (float)Math.Sin(radians));

            // TODO : utiliser Matrix4x4.CreateRotation ?
            return part1 + Matrix4x4.Transpose(part2) + Matrix4x4.Transpose(part3);
        }

        public Matrix4x4 Compute()
        {
            return ComputeMatrix(Axis, Degrees.ToRadians());
        }

        public Matrix4x4 ComputeInverse()
        {
            return ComputeMatrix(Axis, -Degrees.ToRadians());
        }
    }
}
