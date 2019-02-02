namespace TelegramBotSay.Interfaces
{
    public interface IInputDialogModeIWindow
    {
        string TextEdit { get; set; }

        string TextPlaceholderText { get; }

        string TextButtonText { get; }

        Command CommandCloseWindowClick { get; set; }
    }
}