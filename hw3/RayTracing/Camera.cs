using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace hw3
{
    public class Camera
    {
        public void MoveCamera(RTPoint lookFrom, RTPoint lookAt, RTVector up)
        {
            Eye = new RTPoint(lookFrom.Vector);

            W = (lookFrom - lookAt).Normalize();
            U = RTVector.CrossProduct(up, W).Normalize();
            V = RTVector.CrossProduct(W, U);
        }

        public Camera(RTPoint lookFrom, RTPoint lookAt, RTVector up, double fovy, int width, int height)
        {
            MoveCamera(lookFrom, lookAt, up);

            Width = width;
            Height = height;
            
            FovY = fovy.ToRadians();
            FovX = 2.0d * Math.Atan(Math.Tan(FovY / 2.0d) * (width / (double)height));
        }

        public RTPoint Eye { get; private set; }
        public RTVector U { get; private set; }
        public RTVector V { get; private set; }
        public RTVector W { get; private set; }

        public int Width { get; }
        public int Height { get; }

        public double FovX { get; }
        public double FovY { get; }

        public Ray GenerateRay(RTPoint p)
        {
            double alpha = Math.Tan(FovX / 2.0d) * ((p.X - (Width / 2.0d)) / (Width / 2.0d));
            double beta = Math.Tan(FovY / 2.0d) * (((Height / 2.0d) - p.Y) / (Height / 2.0d));

            RTVector rayVec = new RTVector(alpha * U.Vector + beta * V.Vector - W.Vector).Normalize();

            return new Ray(Eye, rayVec, 0, double.MaxValue, false);
        }
    }
}
