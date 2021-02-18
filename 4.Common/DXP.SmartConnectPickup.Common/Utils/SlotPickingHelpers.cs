using System;

namespace DXP.SmartConnectPickup.Common.Utils
{
    public static class SlotPickingHelpers
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero || dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                return dateTime;
            }

            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
    }
}
