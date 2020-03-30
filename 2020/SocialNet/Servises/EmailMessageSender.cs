using System;

namespace SocialNet.Servises
{
    public class EmailMessageSender : IMessageSender
    {
        public void Send()
        {
            Console.WriteLine("Sent by Email");
        }
    }
}