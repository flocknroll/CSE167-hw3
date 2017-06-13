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
        
        private RTColor Shading(LocalGeo geo, ShadingInfos si, Ray ray, RTColor lightCol)
        {
            double nDotL = RTVector.DotProduct(geo.Normal, ray.Vector);


            return si.Ambient;
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
                            res += Shading(geo, si, lightRay, lightCol);
                        }
                    }
                }
            }

            return res;
        }
    }
}
