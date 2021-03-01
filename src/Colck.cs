using System;

namespace VirtualTixClock
{
    internal class Clock
    {
        public static char GetTimeChar(string timeType, int index)
        {
            var date = DateTime.Now.ToString(timeType);
            return date[index];
        }
    }
}