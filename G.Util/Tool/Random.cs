using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Tool
{
    public static class Ran
    {
        public static int[] GetRandomArray(int length, int minvalue, int maxvalue)
        {
            int[] arr = new int[length];
            Random ran = new Random();
            for (var i = 0; i < length; ++i)
            {
                while (true)
                {
                    var c = ran.Next(minvalue, maxvalue + 1);
                    if (!arr.Contains(c))
                    {
                        arr[i] = c;
                        break;
                    }
                }
            }

            for (int i = 0; i < length; ++i)
            {
                arr[i] = arr[i] - 1;
            }

            return arr;
        }
    }
}
