using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.FrameWork.Utils
{
    public class ExStringBuilder : IDisposable
    {
        static object _obj = null;
        static Queue<StringBuilder> _StringBuilderQueue = null;
        static ExStringBuilder()
        {
            _obj = new object();
            _StringBuilderQueue = new Queue<StringBuilder>();
        }

        StringBuilder _sb = null;
        public ExStringBuilder()
        {
            if (_StringBuilderQueue.Count > 0)
            {
                lock (_obj)
                {
                    if (_StringBuilderQueue.Count > 0)
                    {
                        _sb = _StringBuilderQueue.Dequeue();
                    }
                }
            }

            if (_sb == null)
            {
                _sb = new StringBuilder(256);
            }
        }

        public void Dispose()
        {
            _sb.Length = 0;
            lock (_obj)
            {
                _StringBuilderQueue.Enqueue(_sb);
            }
            _sb = null;
        }

        public StringBuilder Append(string value)
        {
            return _sb.Append(value);
        }

        public StringBuilder AppendFormat(string format, object arg0)
        {
            return _sb.AppendFormat(format, arg0);
        }

        public StringBuilder AppendFormat(string format, params object[] args)
        {
            return _sb.AppendFormat(format, args);
        }

        public StringBuilder Insert(int index, string value)
        {
            return _sb.Insert(index, value);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

        public string ToString(int startIndex, int length)
        {
            return _sb.ToString(startIndex, length);
        }

        public override bool Equals(object obj)
        {
            return _sb.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _sb.GetHashCode();
        }
    }
}
