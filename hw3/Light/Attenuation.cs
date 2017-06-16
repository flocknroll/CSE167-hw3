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
            Constant = 1.0f;
            Linear = 0.0f;
            Quadratic = 0.0f;
        }

        public Attenuation(float constant, float linear, float quadratic)
        {
            Constant = constant;
            Linear = linear;
            Quadratic = quadratic;
        }

        public float Constant { get; set; }
        public float Linear { get; set; }
        public float Quadratic { get; set; }
    }
}
