using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class RayTracer
    {
        public RayTracer()
        {
            Primitives = new List<IPrimitive>();
            Lights = new List<ILight>();
        }

        public int MaxDepth { get; set; } = 5;

        public IList<IPrimitive> Primitives { get; set; }
        public IList<ILight> Lights { get; set; }
        public Attenuation Attenuation { get; set; } = new Attenuation();

        private RTColor Shading(LocalGeo geo, ShadingInfos si, Ray camRay, Ray lightRay, RTColor lightCol)
        {
            double nDotL = RTVector.DotProduct(geo.Normal, lightRay.Vector.Normalize());
            RTColor lambert = lightCol * si.Diffuse * (nDotL > 0 ? nDotL : 0.0d);

            RTVector half = (lightRay.Vector.Normalize() - camRay.Vector.Normalize()).Normalize();
            double nDotH = RTVector.DotProduct(geo.Normal, half);
            RTColor phong = lightCol * si.Specular * Math.Pow((nDotH > 0 ? nDotH : 0.0d), si.Shininess);

            double r = lightRay.Directional ? 0 : (geo.Point + lightRay.Vector).Vector.L2Norm();
            RTColor res = (lambert + phong) / (Attenuation.Constant + Attenuation.Linear * r + Attenuation.Quadratic * Math.Pow(r, 2));

            return res;
        }

        public RTColor Trace(Ray ray, int depth)
        {
            RTColor res = new RTColor();

            double lastT = double.MaxValue;
            double currT;
            bool shadow;
            foreach (IPrimitive prim in Primitives)
            {
                LocalGeo geo;
                if (prim.Intersect(ray, true, out geo, out currT))
                {
                    if (currT < lastT)
                    {
                        lastT = currT;

                        ShadingInfos si = prim.GetShading(geo);

                        // Ambient & emission
                        res = si.Emission + si.Ambient;

                        // Phong shading
                        foreach (ILight light in Lights)
                        {
                            shadow = false;
                            RTColor lightCol;
                            Ray lightRay = light.GenerateRay(geo, out lightCol);

                            foreach (IPrimitive pShadow in Primitives)
                            {
                                if (pShadow.Intersect(lightRay, false, out _, out _))
                                    shadow = true;
                            }

                            if (!shadow)
                                res += Shading(geo, si, ray, lightRay, lightCol);
                        }

                        // Reflection
                        if (si.Specular.ARGB[1] > 0 && si.Specular.ARGB[2] > 0 && si.Specular.ARGB[3] > 0 && depth < MaxDepth)
                        {
                            Ray reflected = new Ray(geo.Point, ray.Vector - 2.0d * RTVector.DotProduct(ray.Vector, geo.Normal) * geo.Normal, 0.0001d, double.MaxValue, false);
                            res += si.Specular * Trace(reflected, depth + 1);
                        }

                    }
                }
            }

            return res;
        }
    }
}
