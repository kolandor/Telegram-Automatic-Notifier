using System;
using System.Threading;
using TelegramBotSay.Common;
using TelegramBotSay.Core;
using TelegramBotSay.Interfaces;

namespace TelegramBotSay.Models
{
    public class MainWindowModel : BaseNotifyPropertyChanged, IMainWindowModel
    {
        private DateTime _nexTimeSending;
        private string _messageToSend;
        private string _textInEdit;
        private string _recpient;

        public MainWindowModel()
        {
            if (!Settings.Load(this))
            {
                _nexTimeSending = DateTime.Now;
                _messageToSend = "No message";
                _recpient = "No recpient";
            }

            //Linking commands with methods
            CommandSaveClick = new Command(ButtonSaveClick_OnClick);
            CommandSendNowClick = new Command(ButtonSendNowClick_OnClick);
            CommandChangeRecepientClick = new Command(ButtonChangeRecepientClick_OnClick);

            //A timer that checks the need to send a message.
            TimerCallback tm = new TimerCallback(TimerSendingCheck);
            Timer timer = new Timer(tm, null, 0, 60000);
        }

        //Next date auto send message
        public DateTime NexTimeSending
        {
            get
            {
                return _nexTimeSending;
            }
            set
            {
                SetValue(ref _nexTimeSending, value);
                Settings.Save(this);
            }
        }

        //The message that will be sent to the user automatically or when you click "send now"
        public string MessageToSend
        {
            get
            {
                return _messageToSend;
            }
            set
            {
                SetValue(ref _messageToSend, value);
                Settings.Save(this);
            }
        }

        //Text in the message editing window
        public string TextInEdit
        {
            get { return _textInEdit; }
            set { SetValue(ref _textInEdit, value); }
        }

        //Message recipient
        public string Rrecpient
        {
            get
            {
                return _recpient;
            }
            set
            {
                SetValue(ref _recpient, value);
                Settings.Save(this);
            }
        }
        

        public Command CommandSaveClick { get; set; }

        public Command CommandSendNowClick { get; set; }

        public Command CommandChangeRecepientClick { get; set; }


        private void ButtonSaveClick_OnClick(object sender)
        {
            MessageToSend = TextInEdit;
        }

        private void TimerSendingCheck(object sender)
        {
            if (DateTime.Now > NexTimeSending)
            {
                ButtonSendNowClick_OnClick(null);
            }
        }

        private void ButtonSendNowClick_OnClick(object sender)
        {
            TelegramSendingCore.SendMessage(Rrecpient, MessageToSend);

            NexTimeSending = RandonDate.GetNewRandonTime();
        }

        private void ButtonChangeRecepientClick_OnClick(object sender)
        {
            WindowInputDialog windowInputDialogChangeRecepient =
                new WindowInputDialog("Input new recepient name", "Change");

            windowInputDialogChangeRecepient.ShowDialog();

            if (!string.IsNullOrEmpty(windowInputDialogChangeRecepient.InputModel.TextEdit))
            {
                Rrecpient = windowInputDialogChangeRecepient.InputModel.TextEdit;
            }
        }
    }
}
