using System.Windows;
using TelegramBotSay.Models;

namespace TelegramBotSay
{
    /// <summary>
    /// Interaction logic for InputDialogWindow.xaml
    /// </summary>
    public partial class WindowInputDialog : Window
    {
        public InputDialogModelWindow InputModel { get; private set; }

        /// <summary>
        /// Dialog box to request data from the user
        /// </summary>
        /// <param name="textPlaceholderText"></param>
        /// <param name="textButtonText"></param>
        public WindowInputDialog(string textPlaceholderText, string textButtonText)
        {
            InitializeComponent();

            //bind model and view
            InputModel = new InputDialogModelWindow(textPlaceholderText, textButtonText, Close);
            this.DataContext = InputModel;
            TextBoxInput.Focus();
        }
    }
}
