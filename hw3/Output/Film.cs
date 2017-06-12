using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw3
{
    public class Film: IDisposable
    {
        private Bitmap _film;
        public Film(int width, int height)
        {
            Width = width;
            Height = height;
            _film = new Bitmap(width, height);
        }

        public int Width { get; }
        public int Height { get; }

        public void Commit(Point point, Color color)
        {
            _film.SetPixel(point.X, point.Y, color);
        }

        public void WriteToFile(string path, ImageFormat format)
        {
            _film.Save(path, format);
        }

        public void Dispose()
        {
            _film?.Dispose();
        }
    }
}
