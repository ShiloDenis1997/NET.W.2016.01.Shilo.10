using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Logic
{
    public class TimerOutEventArgs : EventArgs
    {
        public TimerOutEventArgs(int timeout)
        {
            Timeout = timeout;
        }

        public int Timeout { get; private set; }
    }
}
