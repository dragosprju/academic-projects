using System;
using System.Messaging;
namespace Sender
{
    class SenderMain
    {
        static void Main(string[] args)
        {
            MessageQueue mq;
            // Does the queue already exist?
            if (MessageQueue.Exists(@".\private$\NewPrivateQ"))
            { // Yes, then create an object representing the queue
                mq = new MessageQueue(@".\private$\NewPrivateQ");
            }
            else
            { // No, create the queue and cache the returned object
                mq = MessageQueue.Create(@".\private$\NewPrivateQ");
            }
            // Now use queue to send messages ...
            mq.Send("The body of the message", "A message label");
            // Close and delete the queue
            mq.Close();
            //MessageQueue.Delete(@".\private$\NewPrivateQ");
            Console.ReadLine();
        }
    }
}