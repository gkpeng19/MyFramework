using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GOMFrameWork
{
    public class CommonResult
    {
        public long ResultID { get; set; }
        public string Message { get; set; }
    }

    public class CommonResult<T>  where T : new()
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
    }
}
