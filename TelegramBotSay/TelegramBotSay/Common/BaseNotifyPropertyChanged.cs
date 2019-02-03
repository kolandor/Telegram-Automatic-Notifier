using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TelegramBotSay.Common
{
    /// <summary>
    /// Class of binding of model and view (MVVM Pattern)
    /// All classes of models inherited from this class will be able to organize two-way binding.
    /// </summary>
    public abstract class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs((propertyName)));
        }

        protected bool SetValue<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }
    }
}
