using System;

namespace SocialNet.Servises
{
    public class SmsMessageSender : IMessageSender
    {
        public void Send()
        {
            Console.WriteLine("Sent by Sms");
        }
    }
}