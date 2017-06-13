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

        #region Set objects
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
        #endregion

        #region Config helpers
        public SceneBuilder InitCameraDefault()
        {
            _camera = new Camera(RTPoint.Zero, RTPoint.Zero, RTVector.Zero, 90.0d, _sampler?.Width ?? 400, _sampler?.Height ?? 400);

            return this;
        }

        public SceneBuilder SetSize(int width, int height)
        {
            _sampler = new Sampler(width, height);
            _film = new Film(width, height);

            return this;
        }
        public SceneBuilder SetCameraPosition(RTPoint lookFrom, RTPoint lookAt, RTVector up)
        {
            if (_camera == null)
                InitCameraDefault();

            _camera.MoveCamera(lookFrom, lookAt, up);

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
        #endregion

        public Scene Build()
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            return new Scene(_camera, _rayTracer, _sampler, _film, _outPath);
        }
    }
}
