using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hw3
{
    public class Film : IDisposable
    {
        private Bitmap _film;
        private object _lock = new object();
        private Stopwatch _sw = new Stopwatch();
        private long _lastCommit = 0L;
        private long _commited = 0L;

        public Film(int width, int height)
        {
            Width = width;
            Height = height;
            _film = new Bitmap(width, height);
        }

        public int Width { get; }
        public int Height { get; }

        public void StartMonitor()
        {
            _sw.Restart();
            _lastCommit = 0L;
            _commited = 0L;
        }

        public void Commit(RTPoint point, Color color)
        {
            lock (_lock)
            {
                // TODO : "accumuler" valeur si dans même pixel
                _film.SetPixel((int)(float)Math.Floor(point.X), (int)(float)Math.Floor(point.Y), color);
            }
            Interlocked.Increment(ref _commited);

            long time = _sw.ElapsedMilliseconds;
            if (time - _lastCommit > 500L)
            {
                long total = Width * Height;
                _lastCommit = time;

                float pps = (_commited / (float)time) * 1000f;
                TimeSpan elapsed = TimeSpan.FromMilliseconds(time);
                TimeSpan remaining = TimeSpan.FromSeconds((total - _commited) / pps);

                Console.Write($"\rElapsed : {elapsed.TotalHours:0}:{elapsed.Minutes:00}:{elapsed.Seconds:00} - {_commited}/{total} - {pps:0.00} pixel/s - Est. remaining : {remaining.TotalHours:0}:{remaining.Minutes:00}:{remaining.Seconds:00} <>");
            }
        }

        public void WriteToFile(string path, ImageFormat format)
        {
            lock (_lock)
            {
                _film.Save(path, format);
            }
        }

        public void Dispose()
        {
            _film?.Dispose();
        }
    }
}
