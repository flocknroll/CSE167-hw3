﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public interface IShape
    {
        bool Intersect(Ray ray, out LocalGeo geo);
        BRDF GetBRDF(LocalGeo geo);
    }
}
