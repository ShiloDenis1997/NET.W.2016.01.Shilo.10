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
        /// <see cref="TimerOut"/> raises when time, setted in <see cref="SetTimer"/> is out
        /// </summary>
        public event EventHandler TimerOut = delegate {};

        /// <summary>
        /// Sets timeout, after which <see cref="TimerOut"/> event will be raised
        /// </summary>
        public void SetTimer(int milisecondsTimeout)
        {
            Thread.Sleep(milisecondsTimeout);
            OnTimerOut(this, new EventArgs());
        }

        /// <summary>
        /// Method raises <see cref="TimerOut"/> event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTimerOut(object sender, EventArgs e) => TimerOut(sender, e);
    }
}
