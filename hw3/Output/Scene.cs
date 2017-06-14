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
    public class Scene : IDisposable
    {
        public Scene(Camera camera, RayTracer rayTracer, Sampler sampler, Film film, string outPath)
        {
            _camera = camera;
            _rayTracer = rayTracer;
            _sampler = sampler;
            _film = film;
            OutPath = outPath;
        }

        private Camera _camera;
        private RayTracer _rayTracer;
        private Sampler _sampler;
        private Film _film;
        public string OutPath { get; }

        public long Render()
        {
            _sampler.Reset();
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Parallel.ForEach<Point>(_sampler, (point, state, i) =>
            {
                Ray ray = _camera.GenerateRay(point);
                Color color = _rayTracer.Trace(ray, 0).ToColor();
                _film.Commit(point, color);
            });

            //while (_sampler.MoveNext())
            //{
            //    Ray ray = _camera.GenerateRay(_sampler.Current);
            //    Color color = _rayTracer.Trace(ray, 0).ToColor();
            //    _film.Commit(_sampler.Current, color);
            //}
            sw.Stop();

            _film.WriteToFile(OutPath, ImageFormat.Png);
            return sw.ElapsedMilliseconds;
        }

        public void Dispose()
        {
            _sampler?.Dispose();
            _film?.Dispose();
        }
    }
}
