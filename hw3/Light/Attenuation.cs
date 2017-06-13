using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Attenuation
    {
        public Attenuation()
        {
            Constant = 1.0d;
            Linear = 0.0d;
            Quadratic = 0.0d;
        }

        public Attenuation(double constant, double linear, double quadratic)
        {
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
        }

        public double Constant { get; set; }
        public double Linear { get; set; }
        public double Quadratic { get; set; }
    }
}
