using System;
using TelegramBotSay.Common;
using TelegramBotSay.Core;

namespace TelegramBotSay.Models
{
    public class MainWindowModel : BaseNotifyPropertyChanged
    {
        private DateTime _nexTimeSending;
        private string _messageToSend;
        private string _textInEdit;
        private string _textRrecpient;

        public MainWindowModel()
        {
            _nexTimeSending = DateTime.Now;
            _messageToSend = "Test";
            _textRrecpient = "Test Rrecpient";
            CommandSaveClick = new RelayCommand(ButtonSaveClick_OnClick);
            CommandSendNowClick = new RelayCommand(ButtonSendNowClick_OnClick);
            CommandChangeRecepientClick = new RelayCommand(ButtonChangeRecepientClick_OnClick);
        }

        public DateTime NexTimeSending
        {
            get { return _nexTimeSending; }
            set { SetValue(ref _nexTimeSending, value); }
        }

        public string MessageToSend
        {
            get { return _messageToSend; }
            set { SetValue(ref _messageToSend, value); }
        }

        public string TextInEdit
        {
            get { return _textInEdit; }
            set { SetValue(ref _textInEdit, value); }
        }

        public string TextRrecpient
        {
            get { return _textRrecpient; }
            set { SetValue(ref _textRrecpient, value); }
        }

        public RelayCommand CommandSaveClick { get; set; }

        public RelayCommand CommandSendNowClick { get; set; }

        public RelayCommand CommandChangeRecepientClick { get; set; }


        private void ButtonSaveClick_OnClick(object sender)
        {
            MessageToSend = TextInEdit;
        }

        private void ButtonSendNowClick_OnClick(object sender)
        {
             TelegramSendingCore.SendMessage(TextRrecpient, MessageToSend);
        }

        private void ButtonChangeRecepientClick_OnClick(object sender)
        {
            WindowInputDialog windowInputDialogChangeRecepient =
                new WindowInputDialog("Input new recepient name", "Change");

            windowInputDialogChangeRecepient.ShowDialog();

            TextRrecpient = windowInputDialogChangeRecepient.InputModel.TextEdit;
        }
    }
}
