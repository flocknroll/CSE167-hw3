using System.Numerics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RTColor
    {
        public RTColor()
        {
            ARGB = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
        }

        public RTColor(float alpha, float red, float green, float blue)
        {
            ARGB = new Vector4(alpha, red, green, blue);
        }

        public RTColor(Color color)
        {
            ARGB = new Vector4(color.A / 255f, color.R / 255f, color.G / 255f, color.B / 255f);
        }

        public RTColor(Vector4 vector)
        {
            ARGB = vector;
        }

        protected Vector4 ARGB { get; }

        public float Alpha => ARGB.X;
        public float Red => ARGB.Y;
        public float Green => ARGB.Z;
        public float Blue => ARGB.W;

        public Color ToColor()
        {
            int alpha = (int)(float)Math.Floor(ARGB.X * 255d);
            if (alpha > 255)
                alpha = 255;

            int red = (int)(float)Math.Floor(ARGB.Y * 255d);
            if (red > 255)
                red = 255;

            int green = (int)(float)Math.Floor(ARGB.Z * 255d);
            if (green > 255)
                green = 255;

            int blue = (int)(float)Math.Floor(ARGB.W * 255d);
            if (blue > 255)
                blue = 255;

            return Color.FromArgb(alpha, red, green, blue);
        }
        
        public static RTColor operator +(RTColor c1, RTColor c2)
        {
            return new RTColor(c1.ARGB + c2.ARGB);
        }

        public static RTColor operator *(float d, RTColor c)
        {
            return new RTColor(d * c.ARGB);
        }
        public static RTColor operator *(RTColor c, float d)
        {
            return d * c;
        }

        public static RTColor operator *(RTColor c1, RTColor c2)
        {
            return new RTColor(c1.ARGB * c2.ARGB);
        }

        public static RTColor operator /(RTColor c, float d)
        {
            return new RTColor(c.ARGB / d);
        }
    }
}
