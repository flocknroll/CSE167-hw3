using System;
using System.Collections.Generic;
using System.Drawing;
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

        public SceneBuilder SetCamera(RTPoint lookFrom, RTPoint lookAt, RTVector up, double fovy)
        {
            if (_sampler == null)
                throw new InvalidOperationException("La taille doit être définie avant la caméra.");

            _camera = new Camera(lookFrom, lookAt, up, fovy, _sampler.Width, _sampler.Height);

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

        public SceneBuilder AddPrimitive(IPrimitive prim)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.Primitives.Add(prim);

            return this;
        }

        public SceneBuilder AddGeoPrimitive(IIntersect shape, Material mat)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.Primitives.Add(new GeometricPrimitive(shape, mat));

            return this;
        }

        public SceneBuilder AddLight(ILight light)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.Lights.Add(light);

            return this;
        }

        public SceneBuilder SetAttenuation(Attenuation att)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.Attenuation = att;

            return this;
        }

        public SceneBuilder SetMaxDepth(int max)
        {
            if (_rayTracer == null)
                _rayTracer = new RayTracer();

            _rayTracer.MaxDepth = 5;

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
