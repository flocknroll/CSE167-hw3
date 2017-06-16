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
            Eye = new RTPoint(lookFrom);

            W = (lookFrom - lookAt).Normalize();
            U = RTVector.CrossProduct(up, W).Normalize();
            V = RTVector.CrossProduct(W, U);
        }

        public Camera(RTPoint lookFrom, RTPoint lookAt, RTVector up, float fovy, int width, int height)
        {
            MoveCamera(lookFrom, lookAt, up);

            Width = width;
            Height = height;
            
            FovY = fovy.ToRadians();
            FovX = 2.0f * (float)Math.Atan((float)Math.Tan(FovY / 2.0f) * (width / (float)height));
        }

        public RTPoint Eye { get; private set; }
        public RTVector U { get; private set; }
        public RTVector V { get; private set; }
        public RTVector W { get; private set; }

        public int Width { get; }
        public int Height { get; }

        public float FovX { get; }
        public float FovY { get; }

        public Ray GenerateRay(RTPoint p)
        {
            float alpha = (float)Math.Tan(FovX / 2.0f) * ((p.X - (Width / 2.0f)) / (Width / 2.0f));
            float beta = (float)Math.Tan(FovY / 2.0f) * (((Height / 2.0f) - p.Y) / (Height / 2.0f));

            RTVector rayVec = new RTVector(alpha * U + beta * V - W).Normalize();

            return new Ray(Eye, rayVec, 0, float.MaxValue, false);
        }
    }
}
