using System;
using TelegramBotSay.Common;
using TelegramBotSay.Interfaces;

namespace TelegramBotSay.Models
{
    public class WindowInputDialogModel : BaseNotifyPropertyChanged, IWindowInputDialogModel
    {
        private string _textEdit;
        private readonly string _textPlaceholderText;
        private readonly string _textButtonText;
        private Action _colseWindows;

        public WindowInputDialogModel(string textPlaceholderText, string textButtonText, Action colseWindows)
        {
            _textPlaceholderText = textPlaceholderText;
            _textButtonText = textButtonText;
            _colseWindows = colseWindows;
            CommandCloseWindowClick = new RelayCommand(ButtonCloseeWindow_OnClick);
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

        public RelayCommand CommandCloseWindowClick { get; set; }
        
        private void ButtonCloseeWindow_OnClick(object sender)
        {
            _colseWindows();
        }
    }
}
