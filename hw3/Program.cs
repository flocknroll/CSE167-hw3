using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            sb.SetSize(500, 500);
            sb.SetOutputPath(OUTPUT);
            sb.SetCameraPosition(new RTPoint(0, 0, 5),
                                    new RTPoint(0, 0, 0),
                                    new RTVector(0, 1, 0));

            Sphere s1 = new Sphere(new RTPoint(-2, 0, 0), 0.8d);
            s1.Ambient = Color.Red;
            Sphere s2 = new Sphere(new RTPoint(0, 0, 0), 0.5d);
            s2.Ambient = Color.Blue;
            Sphere s3 = new Sphere(new RTPoint(2, 0, 0), 0.3d);
            s3.Ambient = Color.Green;

            sb.AddShape(s1)
                .AddShape(s2)
                .AddShape(s3);

            // TODO : create objects/lights/transformations

            using (Scene scene = sb.Build())
            {
                Console.WriteLine($"Temps : {scene.Render()}ms");
            } 

            Process.Start(OUTPUT);
            Console.ReadKey();
        }
    }
}
