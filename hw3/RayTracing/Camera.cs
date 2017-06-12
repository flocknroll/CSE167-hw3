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
        public Camera(RTPoint lookFrom, RTPoint lookAt, RTVector up, double fovy, int width, int height)
        {
            Eye = new RTPoint(lookFrom.Vector);

            W = (lookAt - lookFrom).Normalize();
            U = RTVector.CrossProduct(up, W).Normalize();
            V = RTVector.CrossProduct(W, U);

            Width = width;
            Height = height;

            FovX = width / (double)height;
            FovY = fovy;
        }

        public RTPoint Eye { get; }
        public RTVector U { get; }
        public RTVector V { get; }
        public RTVector W { get; }

        public int Width { get; }
        public int Height { get; }

        public double FovX { get; }
        public double FovY { get; }

        public Ray GenerateRay(Point p)
        {
            double alpha = Math.Tan(FovX / 2.0d) * ((p.Y - (Width / 2)) / (Width / 2));
            double beta = Math.Tan(FovY / 2.0d) * (((Height / 2) - p.X) / (Height / 2));

            Vector<double> vec = (alpha * U.Vector + beta * V.Vector - W.Vector);
            RTVector rayVec = new RTVector(vec.Normalize(1));

            return new Ray(Eye, rayVec, 0, double.MaxValue);
        }
    }
}
