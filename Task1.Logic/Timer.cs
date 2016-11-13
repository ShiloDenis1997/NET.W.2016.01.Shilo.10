using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1.Logic
{
    /// <summary>
    /// Class representes timer logic which can notify all 
    /// subscribers of <see cref="TimerOut"/> event after timeout
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// <see cref="TimerOut"/> raises when time, setted in <see cref="SetTimer"/> is out.
        /// <see cref="TimerOutEventArgs"/> eventArgs will contain info about last timer duration
        /// </summary>
        public event EventHandler<TimerOutEventArgs> TimerOut = delegate {};

        /// <summary>
        /// Sets timeout, after which <see cref="TimerOut"/> event will be raised.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Throws if 
        /// <paramref name="milisecondsTimeout"/> is negative</exception>
        public void SetTimer(int milisecondsTimeout)
        {
            if (milisecondsTimeout < 0)
                throw new ArgumentOutOfRangeException($"{nameof(milisecondsTimeout)} is less than zero");
            Thread.Sleep(milisecondsTimeout);
            OnTimerOut(this, new TimerOutEventArgs(milisecondsTimeout));
        }

        /// <summary>
        /// Method raises <see cref="TimerOut"/> event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTimerOut(object sender, TimerOutEventArgs e) 
            => TimerOut(sender, e);
    }
}
