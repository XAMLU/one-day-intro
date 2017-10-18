using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace OneDayWorkshop01.Services
{
    public class MessageService
    {
        private static Messenger _messenger;

        static MessageService()
        {
            _messenger = new Messenger();
        }

        public void SubscribeToIsAuthenticatedChanged(object sender, Action<Messages.IsAuthenticatedChangedMessage> action)
        {
            _messenger.Register(sender, action);
        }

        public void SendIsAuthenticatedChanged(bool isAuthenticated)
        {
            _messenger.Send(new Messages.IsAuthenticatedChangedMessage { IsAuthenticated = isAuthenticated });
        }
    }
}
