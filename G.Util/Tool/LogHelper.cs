using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G.Util.Tool
{
    public static class LogHelper
    {
        public static log4net.ILog Logger
        {
            get
            {
                StackTrace stack = new StackTrace();
                return log4net.LogManager.GetLogger(stack.GetFrame(1).GetMethod().DeclaringType);
            }
        }
    }
}
