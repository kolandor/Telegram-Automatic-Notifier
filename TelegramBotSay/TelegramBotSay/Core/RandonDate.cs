using System;

namespace TelegramBotSay.Core
{
    public static class RandonDate
    {
        public static DateTime GetNewRandonTime()
        {
            Random rnd = new Random();

            int dayPlus = rnd.Next(1, 3);

            int hour = rnd.Next(10, 16);

            DateTime now = DateTime.Now.AddDays(dayPlus);
            
            return new DateTime(now.Year, now.Month, now.Day, hour, 0,0);
        }
    }
}