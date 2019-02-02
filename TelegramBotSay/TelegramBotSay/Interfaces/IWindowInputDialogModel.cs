namespace TelegramBotSay.Interfaces
{
    public interface IWindowInputDialogModel
    {
        string TextEdit { get; set; }

        string TextPlaceholderText { get; }

        string TextButtonText { get; }

        RelayCommand CommandCloseWindowClick { get; set; }
    }
}