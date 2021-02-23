using System;

namespace VirtualTixClock
{
    class Clock
    {
        public static char GetTimeChar(string timeType, int index)
        {
            var date = DateTime.Now.ToString(timeType);
            return date[index];
        }
    }
}