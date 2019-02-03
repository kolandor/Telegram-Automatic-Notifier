using System.Windows;
using TelegramBotSay.Core;
using TelegramBotSay.Models;

namespace TelegramBotSay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowModel MainWindowModel { get; private set; }

        public MainWindow()
        {
            //Telegram init
            TelegramSendingCore.Initial();

            InitializeComponent();
            
            //bind model and view
            MainWindowModel = new MainWindowModel();
            this.DataContext = MainWindowModel;

        }
    }
}
