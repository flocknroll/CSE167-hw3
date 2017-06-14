﻿using System;
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
            double nDotL = RTVector.DotProduct(geo.Normal, lightRay.Vector);
            RTColor lambert = lightCol * si.Diffuse * (nDotL > 0 ? nDotL : 0.0d);

            RTVector half = (lightRay.Vector.Normalize() - camRay.Vector.Normalize()).Normalize();
            double nDotH = RTVector.DotProduct(geo.Normal, half);
            RTColor phong = lightCol * si.Specular * Math.Pow((nDotH > 0 ? nDotH : 0.0d), si.Shininess);

            double r = (geo.Point - lightRay.Point).Vector.L1Norm();
            RTColor res = (lambert + phong) / (Attenuation.Constant + Attenuation.Linear * r + Attenuation.Quadratic * Math.Pow(r, 2));

            return res;
        }

        public RTColor Trace(Ray ray, int depth)
        {
            RTColor res = new RTColor();

            foreach (IPrimitive prim in Primitives)
            {
                LocalGeo geo;
                if (prim.Intersect(ray, true, out geo))
                {
                    ShadingInfos si = prim.GetShading(geo);

                    res = si.Emission + si.Ambient;

                    foreach (ILight light in Lights)
                    {
                        RTColor lightCol;
                        Ray lightRay = light.GenerateRay(geo, out lightCol);

                        if (!prim.Intersect(lightRay, false, out _))
                        {
                            res += Shading(geo, si, ray, lightRay, lightCol);
                        }
                    }
                }
            }

            return res;
        }
    }
}
