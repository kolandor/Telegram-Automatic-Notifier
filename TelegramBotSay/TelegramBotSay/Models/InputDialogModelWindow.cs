using System;
using TelegramBotSay.Common;
using TelegramBotSay.Interfaces;

namespace TelegramBotSay.Models
{
    public class InputDialogModelWindow : BaseNotifyPropertyChanged, IInputDialogModeIWindow
    {
        private string _textEdit;
        private readonly string _textPlaceholderText;
        private readonly string _textButtonText;
        private Action _colseWindows;

        public InputDialogModelWindow(string textPlaceholderText, string textButtonText, Action colseWindows)
        {
            _textPlaceholderText = textPlaceholderText;
            _textButtonText = textButtonText;
            _colseWindows = colseWindows;
            CommandCloseWindowClick = new Command(ButtonCloseeWindow_OnClick);
        }
        
        public string TextEdit
        {
            get { return _textEdit; }
            set { SetValue(ref _textEdit, value); }
        }

        public string TextPlaceholderText
        {
            get { return _textPlaceholderText; }
        }

        public string TextButtonText
        {
            get { return _textButtonText; }
        }

        public Command CommandCloseWindowClick { get; set; }
        
        private void ButtonCloseeWindow_OnClick(object sender)
        {
            _colseWindows();
        }
    }
}
