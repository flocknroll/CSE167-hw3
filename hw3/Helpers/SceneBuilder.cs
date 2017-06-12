using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class SceneBuilder
    {
        private Camera _camera;
        private RayTracer _rayTracer;
        private Sampler _sampler;
        private Film _film;
        private string _outPath;
        
        public SceneBuilder SetRayTracer(RayTracer raytracer)
        {
            _rayTracer = raytracer;

            return this;
        }

        public SceneBuilder SetCamera(Camera camera)
        {
            _camera = camera;

            return this;
        }

        public SceneBuilder SetSampler(Sampler sampler)
        {
            _sampler = sampler;

            return this;
        }

        public SceneBuilder SetFilm(Film film)
        {
            _film = film;

            return this;
        }

        public SceneBuilder SetSize(int width, int height)
        {
            _sampler = new Sampler(width, height);
            _film = new Film(width, height);

            return this;
        }

        public SceneBuilder SetOutputPath(string outPath)
        {
            _outPath = outPath;

            return this;
        }

        public SceneBuilder AddShape(IShape shape)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.Shapes.Add(shape);

            return this;
        }

        public Scene Build()
        {
            return new Scene(_camera, _rayTracer,_sampler, _film, _outPath);
        }
    }
}
