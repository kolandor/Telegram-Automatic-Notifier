using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotSay.Common;
using TeleSharp.TL;
using TeleSharp.TL.Contacts;
using TLSharp.Core;

namespace TelegramBotSay.Core
{
    /// <summary>
    /// Telegram function control class
    /// </summary>
    public static class TelegramSendingCore
    {
        private static TelegramClient _client;
        private static TLUser _user;

        /// <summary>
        /// Initialization method, starts the process of connecting to the service telegram
        /// </summary>
        /// <returns></returns>
        public async static Task Initial()
        {
            /*Telegram methods do not always behave predictably.
             Most of them simply block the flow if you set a await on it.*/

            _client = new TelegramClient(DevConstants.API_ID, DevConstants.API_HASH);

            _client.ConnectAsync();

            if (!_client.IsUserAuthorized())
            {
                //Creating a dialogue to enter the user's phone
                WindowInputDialog inputDialogPhone = new WindowInputDialog("Please input your phone number", "Send autorize message");

                inputDialogPhone.ShowDialog();

                string phone = inputDialogPhone.InputModel.TextEdit;


                if (string.IsNullOrEmpty(phone))
                {
                    throw new Exception("Wrong phone");
                }

                string hash = string.Empty;

                /*Due to the fact that all methods are asynchronous,
                 cannot be await and have no synchronous counterparts, 
                 you have to use similar hacks to get data from them*/
                _client.SendCodeRequestAsync(phone).ContinueWith(hashAsync => { hash = hashAsync.Result; });


                WindowInputDialog inputDialogAuthMsg =
                    new WindowInputDialog("Please input telegram authorize message", "OK");

                //Creating a dialogue to enter a temporary password (comes in active telegram clients or SMS)
                inputDialogAuthMsg.ShowDialog();

                string authMsg = inputDialogAuthMsg.InputModel.TextEdit;

                if (string.IsNullOrEmpty(authMsg))
                {
                    throw new Exception("Wrong authorize message");
                }

                //Authorization confirmation method again using hacks
                _client.MakeAuthAsync(phone, hash, authMsg).ContinueWith(user => { _user = user.Result; });
            }
        }

        /// <summary>
        /// Sending a message using the recipient's nickname and message text
        /// </summary>
        /// <param name="recepientUserName"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async static Task SendMessage(string recepientUserName, string message)
        {
            //get user contacts
            TLContacts constacts = await _client.GetContactsAsync();

            //find recipient in contacts
            TLUser user = constacts.Users
                .Where(x => x.GetType() == typeof(TLUser))
                .Cast<TLUser>()
                .FirstOrDefault(x => x.Username == recepientUserName);

            //send message
            _client.SendMessageAsync(new TLInputPeerUser() { UserId = user.Id }, message);
        }
    }
}
