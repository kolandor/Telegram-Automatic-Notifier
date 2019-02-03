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
            //Loading application settings
            if (!Settings.Load(this))
            {
                //If the settings file is not found or is not available, the content is populated with default values.
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

        /// <summary>
        /// Next date auto send message
        /// </summary>
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

        /// <summary>
        /// The message that will be sent to the user automatically or when you click "send now"
        /// </summary>
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

        /// <summary>
        /// Text in the message editing window
        /// </summary>
        public string TextInEdit
        {
            get { return _textInEdit; }
            set { SetValue(ref _textInEdit, value); }
        }

        /// <summary>
        /// Message recipient
        /// </summary>
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

        /// <summary>
        /// Immediate Send Command
        /// </summary>
        public Command CommandSaveClick { get; set; }

        /// <summary>
        /// Immediate Send Command
        /// </summary>
        public Command CommandSendNowClick { get; set; }

        //Command to call a dialog box to change the message recipient
        public Command CommandChangeRecepientClick { get; set; }


        /// <summary>
        /// Saving a new message text for sending
        /// </summary>
        /// <param name="sender"></param>
        private void ButtonSaveClick_OnClick(object sender)
        {
            MessageToSend = TextInEdit;
        }

        /// <summary>
        /// Timer method
        /// </summary>
        /// <param name="sender"></param>
        private void TimerSendingCheck(object sender)
        {
            //Once a minute checks the need to send a message if it's time to send
            if (DateTime.Now > NexTimeSending)
            {
                ButtonSendNowClick_OnClick(null);
            }
        }

        /// <summary>
        /// The method of sending a message right now
        /// </summary>
        /// <param name="sender"></param>
        private void ButtonSendNowClick_OnClick(object sender)
        {
            //Send messages, accepts the recipient's name and message text
            TelegramSendingCore.SendMessage(Rrecpient, MessageToSend);

            //Generate a new time for automatically sending the message next time.
            NexTimeSending = RandonDate.GetNewRandonTime();
        }

        /// <summary>
        /// Change recipient
        /// </summary>
        /// <param name="sender"></param>
        private void ButtonChangeRecepientClick_OnClick(object sender)
        {
            //Call the dialog box for entering the recipient's nickname
            WindowInputDialog windowInputDialogChangeRecepient =
                new WindowInputDialog("Input new recepient name", "Change");

            windowInputDialogChangeRecepient.ShowDialog();

            if (!string.IsNullOrEmpty(windowInputDialogChangeRecepient.InputModel.TextEdit))
            {
                //If the data was entered we change the value of the recipient field to the values from the dialog
                Rrecpient = windowInputDialogChangeRecepient.InputModel.TextEdit;
            }
        }
    }
}
