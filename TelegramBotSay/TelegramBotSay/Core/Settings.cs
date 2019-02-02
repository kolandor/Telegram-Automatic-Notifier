using System;
using System.IO;
using Newtonsoft.Json;
using TelegramBotSay.Interfaces;
using TelegramBotSay.Common;

namespace TelegramBotSay.Core
{
    public static class Settings
    {
        private class AppSettings
        {
            public DateTime NexTimeSending { get; set; }
            public string TextRrecpient { get; set; }
            public string MessageToSend { get; set; }

            public static AppSettings GetAppSettingsFromModel(IMainWindowModel mainWindowModel)
            {
                return new AppSettings()
                {
                    NexTimeSending = mainWindowModel.NexTimeSending,
                    MessageToSend = mainWindowModel.MessageToSend,
                    TextRrecpient = mainWindowModel.Rrecpient
                };
            }
        }

        public static bool Save(IMainWindowModel mainWindowModel)
        {
            try
            {
                AppSettings settings = AppSettings.GetAppSettingsFromModel(mainWindowModel);

                string jasonObject = JsonConvert.SerializeObject(settings);

                File.WriteAllText(DevConstants.SETTINGS_PATH, jasonObject);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool Load(IMainWindowModel mainWindowModel)
        {
            try
            {
                string jasonObject = File.ReadAllText(DevConstants.SETTINGS_PATH);

                AppSettings settings = JsonConvert.DeserializeObject<AppSettings>(jasonObject);

                mainWindowModel.MessageToSend = settings.MessageToSend;
                mainWindowModel.NexTimeSending = settings.NexTimeSending;
                mainWindowModel.Rrecpient = settings.TextRrecpient;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}