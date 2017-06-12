using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO : read parameters
            SceneBuilder sb = new SceneBuilder();
            sb.SetSize(640, 480);
            sb.SetOutputPath(@"test.png");
            sb.SetCamera(new Camera(new RTPoint(0, 0, 0),
                                    new RTPoint(0, 0, -2),
                                    new RTVector(0, 1, 0),
                                    1.0, 640, 480));
            sb.SetRayTracer(new RayTracer());
            sb.AddShape(new Sphere(new RTPoint(0, 0, -2), 1));

            // TODO : create objects/lights/transformations

            using (Scene scene = sb.Build())
            {
                scene.Render();
            }
        }
    }
}
