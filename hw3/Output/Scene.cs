using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Scene: IDisposable
    {
        public Scene(Camera camera, RayTracer rayTracer, Sampler sampler, Film film, string outPath)
        {
            _camera = camera;
            _rayTracer = rayTracer;
            _sampler = sampler;
            _film = film;
            _outPath = outPath;
        }

        private Camera _camera;
        private RayTracer _rayTracer;
        private Sampler _sampler;
        private Film _film;
        private string _outPath;

        public void Render()
        {
            _sampler.Reset();
            while (_sampler.MoveNext())
            {
                Ray ray = _camera.GenerateRay(_sampler.Current);
                Color color = _rayTracer.Trace(ray);
                _film.Commit(_sampler.Current, color);
            }
            _film.WriteToFile(_outPath, ImageFormat.Png);
        }

        public void Dispose()
        {
            _sampler?.Dispose();
            _film?.Dispose();
        }
    }
}
