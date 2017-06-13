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
        private static readonly string OUTPUT = @"test.png";

        static void Main(string[] args)
        {
            // TODO : read parameters
            SceneBuilder sb = new SceneBuilder();
            sb.SetSize(640, 480);
            sb.SetOutputPath(OUTPUT);
            sb.SetCameraPosition(new RTPoint(0, 0, 1),
                                    new RTPoint(0, 0, 0),
                                    new RTVector(0, 1, 0));
            sb.AddShape(new Sphere(new RTPoint(0, 0, 0), 0.5d));

            // TODO : create objects/lights/transformations

            using (Scene scene = sb.Build())
            {
                scene.Render();
            }

            System.Diagnostics.Process.Start(OUTPUT);
        }
    }
}
