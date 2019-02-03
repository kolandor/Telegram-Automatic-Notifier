using System;
using TelegramBotSay.Common;
using TelegramBotSay.Interfaces;

namespace TelegramBotSay.Models
{
    /// <summary>
    /// Data storage and processing model associated with the data entry dialog
    /// </summary>
    public class InputDialogModelWindow : BaseNotifyPropertyChanged, IInputDialogModeIWindow
    {
        private string _textEdit;
        private readonly string _textPlaceholderText;
        private readonly string _textButtonText;
        private Action _buttonAction;

        /// <summary>
        /// The constructor accepts the placeholder text, the text on the button
        /// and the action that will occur when the button is pressed.
        /// </summary>
        /// <param name="textPlaceholderText"></param>
        /// <param name="textButtonText"></param>
        /// <param name="buttonAction"></param>
        public InputDialogModelWindow(string textPlaceholderText, string textButtonText, Action buttonAction)
        {
            _textPlaceholderText = textPlaceholderText;
            _textButtonText = textButtonText;
            _buttonAction = buttonAction;
            CommandCloseWindowClick = new Command(ButtonCloseeWindow_OnClick);
        }

        /// <summary>
        /// Text in the input box
        /// </summary>
        public string TextEdit
        {
            get { return _textEdit; }
            set { SetValue(ref _textEdit, value); }
        }

        /// <summary>
        /// Placeholder text
        /// </summary>
        public string TextPlaceholderText
        {
            get { return _textPlaceholderText; }
        }

        /// <summary>
        /// Botton text
        /// </summary>
        public string TextButtonText
        {
            get { return _textButtonText; }
        }

        /// <summary>
        /// Button processing handler
        /// </summary>
        public Command CommandCloseWindowClick { get; set; }
        
        private void ButtonCloseeWindow_OnClick(object sender)
        {
            _buttonAction();
        }
    }
}
