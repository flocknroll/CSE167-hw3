using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public interface ILight
    {
        Ray GenerateRay(LocalGeo geo, out RTColor color);
    }
}
