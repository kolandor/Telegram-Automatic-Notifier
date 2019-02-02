using System;

namespace TelegramBotSay.Common
{
    public class Settings
    {
        public DateTime NextTimeSend { get; set; }
        public string MsgToSend { get; set; }
        public string RecepientName { get; set; }
    }
}
