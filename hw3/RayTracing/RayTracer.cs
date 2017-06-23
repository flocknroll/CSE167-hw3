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
            float nDotL = RTVector.DotProduct(geo.Normal, lightRay.Vector);
            RTColor lambert = lightCol * si.Diffuse * (nDotL > 0 ? nDotL : 0.0f);

            RTVector half = (lightRay.Vector - camRay.Vector.Normalize()).Normalize();
            float nDotH = RTVector.DotProduct(geo.Normal, half);
            RTColor phong = lightCol * si.Specular * (float)Math.Pow((nDotH > 0 ? nDotH : 0.0f), si.Shininess);

            float r = lightRay.Directional ? 0 : lightRay.TMax; // Dans le cas d'un point, le t max est la distance entre le point et la source
            RTColor res = (lambert + phong) / (Attenuation.Constant + Attenuation.Linear * r + Attenuation.Quadratic * (float)Math.Pow(r, 2));

            return res;
        }

        private BBTree _tree = null;

        public RTColor Trace(Ray ray, int depth)
        {
            RTColor res = new RTColor();

            float lastT = float.MaxValue;
            float currT;
            bool shadow;

            List<IPrimitive> boxFound = new List<IPrimitive>();

            // TODO : initialiser l'arbre autrepart
            if (_tree == null)
                _tree = new BBTree(Primitives);

            BBNode hitNode;

            if (_tree.Hit(ray, out hitNode))
                boxFound.Add(hitNode.Primitive);

            foreach (IPrimitive prim in boxFound)
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
                        if ((si.Specular.Red > 0 || si.Specular.Green > 0 || si.Specular.Blue > 0) && depth < MaxDepth)
                        {
                            Ray reflected = new Ray(geo.Point, ray.Vector - 2.0f * RTVector.DotProduct(ray.Vector, geo.Normal) * geo.Normal, 1e-3f, float.MaxValue, false);
                            res += si.Specular * Trace(reflected, depth + 1);
                        }

                    }
                }
            }

            return res;
        }
    }
}
