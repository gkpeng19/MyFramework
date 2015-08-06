using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GP.FrameWork.Extension
{
    public static class StringBuilderProvider
    {
        static object _obj = null;
        static Queue<StringBuilder> _StringBuilderQueue = null;
        static StringBuilderProvider()
        {
            _obj = new object();
            _StringBuilderQueue = new Queue<StringBuilder>();
        }

        public static StringBuilder Current
        {
            get
            {
                if (_StringBuilderQueue.Count > 0)
                {
                    lock (_obj)
                    {
                        if (_StringBuilderQueue.Count > 0)
                        {
                            return _StringBuilderQueue.Dequeue();
                        }
                    }
                }
                return new StringBuilder();
            }
        }

        public static void Dispose(this StringBuilder sb)
        {
            sb.Length = 0;
            lock (_obj)
            {
                _StringBuilderQueue.Enqueue(sb);
            }
        }
    }
}
