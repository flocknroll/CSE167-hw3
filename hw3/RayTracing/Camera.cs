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

            W = (lookFrom - lookAt).Normalize();
            U = RTVector.CrossProduct(up, W).Normalize();
            V = RTVector.CrossProduct(W, U);

            Width = width;
            Height = height;

            
            FovY = fovy * Math.PI / 180.0d;
            FovX = (FovY / height) * width;
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
            double alpha = Math.Tan(FovX / 2.0d) * ((p.X - (Width / 2.0d)) / (Width / 2.0d));
            double beta = Math.Tan(FovY / 2.0d) * (((Height / 2.0d) - p.Y) / (Height / 2.0d));

            Vector<double> vec = (alpha * U.Vector + beta * V.Vector - W.Vector);
            RTVector rayVec = new RTVector(Eye.Vector + vec.Normalize(1));

            //Console.WriteLine(rayVec.Vector.ToString());

            return new Ray(Eye, rayVec, 0, double.MaxValue);
        }
    }
}
