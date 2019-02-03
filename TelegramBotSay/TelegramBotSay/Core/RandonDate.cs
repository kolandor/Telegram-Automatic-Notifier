using System;

namespace TelegramBotSay.Core
{
    /// <summary>
    /// Class to generate date
    /// </summary>
    public static class RandonDate
    {
        /// <summary>
        /// Generate date relative to current date.
        /// The method generates a date according:
        /// to the algorithm today + (1-2 days) and between 10 am and 3 pm
        /// </summary>
        /// <returns></returns>
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