using System;
using System.IO;
using Newtonsoft.Json;
using TelegramBotSay.Interfaces;
using TelegramBotSay.Common;

namespace TelegramBotSay.Core
{
    /// <summary>
    /// Class engaged in the preservation and loading of application settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// linker subclass for application settings
        /// </summary>
        private class AppSettings
        {
            public DateTime NexTimeSending { get; set; }
            public string TextRrecpient { get; set; }
            public string MessageToSend { get; set; }

            /// <summary>
            /// Method of pulling the necessary data from the model
            /// </summary>
            /// <param name="mainWindowModel"></param>
            /// <returns></returns>
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

        /// <summary>
        /// Saving data from model to file
        /// </summary>
        /// <param name="mainWindowModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Loading data from file to model
        /// </summary>
        /// <param name="mainWindowModel"></param>
        /// <returns></returns>
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