using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class ConfigReader
    {
        private string _path;

        public ConfigReader(string path)
        {
            _path = path;
        }

        private Material _currentMaterial;
        private Transformation _currentTransformation;

        public Scene BuildScene()
        {
            SceneBuilder sb = new SceneBuilder();

            string line;
            _currentMaterial = new Material();
            _currentTransformation = new Transformation(Matrix<double>.Build.DenseIdentity(4));

            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine().TrimStart(" \t".ToCharArray()).ToLowerInvariant();

                    if (string.IsNullOrWhiteSpace(line) || line[0] == '#')
                        continue;

                    string[] split = line.Split(' ');

                    switch (split[0])
                    {
                            #region General/Camera
                        case "size":
                            int width = int.Parse(split[1]);
                            int height = int.Parse(split[2]);
                            sb.SetSize(width, height);
                            break;

                        case "maxdepth":
                            int maxDepth = int.Parse(split[1]);
                            sb.SetMaxDepth(maxDepth);
                            break;

                        case "output":
                            sb.SetOutputPath(split[1]);
                            break;

                        case "camera":
                            RTPoint lFrom = new RTPoint(double.Parse(split[1]), double.Parse(split[2]), double.Parse(split[3]));
                            RTPoint lAt = new RTPoint(double.Parse(split[4]), double.Parse(split[5]), double.Parse(split[6]));
                            RTVector up = new RTVector(double.Parse(split[7]), double.Parse(split[8]), double.Parse(split[9]));
                            double fovy = double.Parse(split[10]);
                            sb.SetCamera(lFrom, lAt, up, fovy);
                            break;
                        #endregion

                        #region Geometry
                        case "sphere":
                            RTPoint center = new RTPoint(double.Parse(split[1]), double.Parse(split[2]), double.Parse(split[3]));
                            double radius = double.Parse(split[4]);
                            Sphere s = new Sphere(center, radius);

                            sb.AddGeoPrimitive(s, _currentMaterial, _currentTransformation);
                            _currentMaterial = new Material();
                            _currentTransformation = new Transformation(Matrix<double>.Build.DenseIdentity(4));
                            break;
                            #endregion
                    }
                }
            }

            return sb.Build();
        }
    }
}
