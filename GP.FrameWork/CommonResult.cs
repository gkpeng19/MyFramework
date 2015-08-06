using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GP.FrameWork
{
    public class CommonResult
    {
        public long ResultID { get; set; }
        public string Message { get; set; }
    }

    public class CommonResult<T>:CommonResult,IEnumerable<T> where T : new()
    {
        List<T> _data = null;
        public List<T> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<T>();
                }
                return _data;
            }
            set
            {
                _data = value;
            }
        }

        public object Tag { get; set; }

        public int PageCount { get; set; }
        public int PageIndex { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public T this[int index]
        {
            get
            {
                return Data[index];
            }
        }
    }

    public class CommonData<T> : CommonResult where T : new()
    {
        public T Data { get; set; }
    }
}
