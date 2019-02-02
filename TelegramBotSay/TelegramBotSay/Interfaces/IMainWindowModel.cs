using System;

namespace TelegramBotSay.Interfaces
{
    public interface IMainWindowModel
    {
        DateTime NexTimeSending { get; set; }

        string MessageToSend { get; set; }

        string TextInEdit { get; set; }

        string Rrecpient { get; set; }

        Command CommandSaveClick { get; set; }

        Command CommandSendNowClick { get; set; }

        Command CommandChangeRecepientClick { get; set; }
    }
}