using MathNet.Numerics.LinearAlgebra;
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
            ARGB = Vector<double>.Build.Dense(new double[] { 1.0d, 0.0d, 0.0d, 0.0d });
        }

        public RTColor(double alpha, double red, double green, double blue)
        {
            ARGB = Vector<double>.Build.DenseOfArray(new double[] { alpha, red, green, blue });
        }

        public RTColor(Color color)
        {
            ARGB = Vector<double>.Build.DenseOfArray(new double[] { color.A / 255d, color.R / 255d, color.G / 255d, color.B / 255d });
        }

        public RTColor(Vector<double> vector)
        {
            ARGB = vector;
        }

        public Vector<double> ARGB { get; }

        public Color ToColor()
        {
            int alpha = (int)Math.Floor(ARGB[0] * 255d);
            if (alpha > 255)
                alpha = 255;

            int red = (int)Math.Floor(ARGB[1] * 255d);
            if (red > 255)
                red = 255;

            int green = (int)Math.Floor(ARGB[2] * 255d);
            if (green > 255)
                green = 255;

            int blue = (int)Math.Floor(ARGB[3] * 255d);
            if (blue > 255)
                blue = 255;

            return Color.FromArgb(alpha, red, green, blue);
        }
        
        public static RTColor operator +(RTColor c1, RTColor c2)
        {
            return new RTColor(c1.ARGB + c2.ARGB);
        }

        public static RTColor operator *(double d, RTColor c)
        {
            return new RTColor(d * c.ARGB);
        }
        public static RTColor operator *(RTColor c, double d)
        {
            return d * c;
        }

        public static RTColor operator *(RTColor c1, RTColor c2)
        {
            return new RTColor(c1.ARGB[0] * c2.ARGB[0],
                                c1.ARGB[1] * c2.ARGB[1],
                                c1.ARGB[2] * c2.ARGB[2],
                                c1.ARGB[3] * c2.ARGB[3]);
        }
    }
}
