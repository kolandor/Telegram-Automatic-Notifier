using System;

namespace TelegramBotSay.Interfaces
{
    public interface IMainWindowModel
    {
        DateTime NexTimeSending { get; set; }

        string MessageToSend { get; set; }

        string TextInEdit { get; set; }

        string TextRrecpient { get; set; }

        RelayCommand CommandSaveClick { get; set; }

        RelayCommand CommandSendNowClick { get; set; }

        RelayCommand CommandChangeRecepientClick { get; set; }
    }
}