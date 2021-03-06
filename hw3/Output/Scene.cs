﻿using System;
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

        public void Render()
        {
            _sampler.Reset();
            _film.StartMonitor();
#if DEBUG
            while (_sampler.MoveNext())
            {
                Ray ray = _camera.GenerateRay(_sampler.Current);
                Color color = _rayTracer.Trace(ray, 0).ToColor();
                _film.Commit(_sampler.Current, color);
            }
#else
            Parallel.ForEach<RTPoint>(_sampler, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (point, state, i) =>
            {
                Ray ray = _camera.GenerateRay(point);
                Color color = _rayTracer.Trace(ray, 0).ToColor();
                _film.Commit(point, color);
            });
#endif

            _film.WriteToFile(OutPath, ImageFormat.Png);
        }

        public void Dispose()
        {
            _sampler?.Dispose();
            _film?.Dispose();
        }
    }
}
