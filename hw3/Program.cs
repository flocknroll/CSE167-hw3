using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace hw3
{
    class Program
    {
        static void Main(string[] args)
        {
            string inPath = args[0];
            if (string.IsNullOrEmpty(inPath))
                inPath = @"Tests\default.test";

            if (!File.Exists(inPath))
                return;

            ConfigReader cr = new ConfigReader(inPath);
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            using (Scene scene = cr.BuildScene())
            {
                Console.WriteLine($"Temps : {scene.Render()}ms");

                Process.Start(scene.OutPath);
            }
        }
    }
}
