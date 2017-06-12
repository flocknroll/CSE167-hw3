using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace hw3
{
    public class Sampler: IEnumerator<Point>
    {
        private int _current;
        public Sampler(int width, int height)
        {
            Width = width;
            Height = height;
            Reset();
        }

        public int Width { get; }
        public int Height { get; }

        public Point Current => new Point(_current % Width, _current / Width);

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            Interlocked.Increment(ref _current);

            return _current < Width * Height;
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _current, -1);
        }

        public void Dispose()
        {
            //
        }
    }
}
