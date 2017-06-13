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

        private void InitPrimitiveProperties()
        {
            RTColor lastAmbient = _currentMaterial?.Properties.Ambient;

            _currentMaterial = new Material();
            if (lastAmbient != null)
                _currentMaterial.Properties.Ambient = lastAmbient;

            _currentTransformation = new Transformation(Matrix<double>.Build.DenseIdentity(4));
        }

        private static RTColor ColorFromConfig(string[] split, int offset = 1)
        {
            return new RTColor(1.0d,
                            double.Parse(split[offset]),
                            double.Parse(split[offset + 1]),
                            double.Parse(split[offset + 2]));
        }

        private static RTVector VectorFromConfig(string[] split, int offset = 1)
        {
            return new RTVector(double.Parse(split[offset]), double.Parse(split[offset + 1]), double.Parse(split[offset + 2]));
        }

        private static RTPoint PointFromConfig(string[] split, int offset = 1)
        {
            return new RTPoint(double.Parse(split[offset]), double.Parse(split[offset + 1]), double.Parse(split[offset + 2]));
        }

        public Scene BuildScene()
        {
            SceneBuilder sb = new SceneBuilder();

            string line;
            InitPrimitiveProperties();

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
                            RTPoint lFrom = PointFromConfig(split);
                            RTPoint lAt = PointFromConfig(split, 4);
                            RTVector up = VectorFromConfig(split, 7);
                            double fovy = double.Parse(split[10]);
                            sb.SetCamera(lFrom, lAt, up, fovy);
                            break;
                        #endregion

                        #region Geometry
                        case "sphere":
                            RTPoint center = PointFromConfig(split);
                            double radius = double.Parse(split[4]);
                            Sphere s = new Sphere(center, radius);

                            sb.AddGeoPrimitive(s, _currentMaterial, _currentTransformation);
                            InitPrimitiveProperties();
                            break;

                        // TODO : triangles
                        #endregion

                        #region Lights
                        case "directional":
                            RTVector dir = VectorFromConfig(split);
                            RTColor dirCol = ColorFromConfig(split, 4);
                            sb.AddLight(new DirLight(dir, dirCol));
                            break;

                        case "point":
                            RTPoint source = PointFromConfig(split);
                            RTColor pointCol = ColorFromConfig(split, 4);
                            sb.AddLight(new PointLight(source, pointCol));
                            break;

                        case "attenuation":
                            Attenuation att = new Attenuation
                            {
                                Constant = double.Parse(split[1]),
                                Linear = double.Parse(split[2]),
                                Quadratic = double.Parse(split[3])
                            };
                            sb.SetAttenuation(att);
                            break;

                        case "ambient":
                            RTColor ambient = ColorFromConfig(split);
                            _currentMaterial.Properties.Ambient = ambient;
                            break;
                        #endregion

                        #region Materials
                        case "diffuse":
                            RTColor diffuse = ColorFromConfig(split);
                            _currentMaterial.Properties.Diffuse = diffuse;
                            break;

                        case "specular":
                            RTColor spec = ColorFromConfig(split);
                            _currentMaterial.Properties.Specular = spec;
                            break;

                        case "shininess":
                            double shine = double.Parse(split[1]);
                            _currentMaterial.Properties.Shininess = shine;
                            break;

                        case "emission":
                            RTColor emission = ColorFromConfig(split);
                            _currentMaterial.Properties.Emission = emission;
                            break;
                            #endregion
                    }
                }
            }

            return sb.Build();
        }
    }
}
