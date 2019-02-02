using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotSay.Common;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TLSharp.Core;

namespace TelegramBotSay.Core
{
    public static class TelegramSendingCore
    {
        private static TelegramClient _client;
        private static TLUser _user;
        public static string _hash;
        public static string _phone;

        public async static Task Initial()
        {
            _client = new TelegramClient(DevConstants.API_ID, DevConstants.API_HASH);
            
            _client.ConnectAsync();

            if (!_client.IsUserAuthorized())
            {
                WindowInputDialog inputDialogPhone = new WindowInputDialog("Please input your phone number", "Send autorize message");

                inputDialogPhone.ShowDialog();

                _phone = inputDialogPhone.InputModel.TextEdit;
                

                if (string.IsNullOrEmpty(_phone))
                {
                    throw new Exception("Wrong phone");
                }

                _client.SendCodeRequestAsync(_phone).ContinueWith(hash => { _hash = hash.Result; });

                WindowInputDialog inputDialogAuthMsg =
                    new WindowInputDialog("Please input telegram authorize message", "OK");

                inputDialogAuthMsg.ShowDialog();

                string authMsg = inputDialogAuthMsg.InputModel.TextEdit;

                if (string.IsNullOrEmpty(authMsg))
                {
                    throw new Exception("Wrong authorize message");
                }

                _client.MakeAuthAsync(_phone, _hash, authMsg).ContinueWith(user => { _user = user.Result; });
            }
        }


        public async static Task SendMessage(string recepientUserName, string message)
        {
            //get available contacts
            TLContacts result = await _client.GetContactsAsync();

            //find recipient in contacts
            TLUser user = result.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Username == recepientUserName);

            //send message
            _client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, message);
        }
    }
}
