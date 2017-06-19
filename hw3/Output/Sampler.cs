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
    public class Sampler: IEnumerator<RTPoint>, IEnumerable<RTPoint>
    {
        private float _current;
        private object _lock = new object();
        // On vise le milieu des pixels
        private readonly float PIXEL_OFFSET = 0.5f;

        public Sampler(int width, int height)
        {
            Width = width;
            Height = height;
            Reset();
        }

        public int Width { get; }
        public int Height { get; }

        public RTPoint Current {
            get
            {
                return new RTPoint(_current % Width, (float)Math.Floor(_current / Width) + PIXEL_OFFSET, 0);
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            lock (_lock)
            {
                ++_current;
            }

            return _current < Width * Height;
        }

        public void Reset()
        {
            lock (_lock)
            {
                _current = PIXEL_OFFSET - 1f;
            }
        }

        public void Dispose()
        {
            //
        }

        public IEnumerator<RTPoint> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }
    }
}
