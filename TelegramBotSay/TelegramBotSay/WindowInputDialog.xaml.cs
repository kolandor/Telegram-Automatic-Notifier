using System.Windows;
using TelegramBotSay.Models;

namespace TelegramBotSay
{
    /// <summary>
    /// Interaction logic for WindowInputDialog.xaml
    /// </summary>
    public partial class WindowInputDialog : Window
    {
        public WindowInputDialogModel InputModel { get; private set; }

        public WindowInputDialog(string textPlaceholderText, string textButtonText)
        {
            InitializeComponent();
            InputModel = new WindowInputDialogModel(textPlaceholderText, textButtonText, Close);
            this.DataContext = InputModel;
        }
    }
}
