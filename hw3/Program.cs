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
            ConfigReader cr = new ConfigReader(args[0]);

            using (Scene scene = cr.BuildScene())
            {
                Console.WriteLine($"Temps : {scene.Render()}ms");
            } 

            Process.Start(OUTPUT);
            Console.ReadKey();
        }
    }
}
