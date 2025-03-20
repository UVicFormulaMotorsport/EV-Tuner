using Peak.Can.Basic;
using Peak.Can.Basic.BackwardCompatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace EV_Tuner
{
    class SendCANMessage
    {
        Form1 mainform;

        public static void Initialize()
        {
            PcanChannel channel = PcanChannel.Usb01;
            PcanStatus result = Api.Initialize(channel, Bitrate.Pcan250);

            if (result == PcanStatus.OK)
            {
                Form1.Instance.changeStatus("CAN Connection Established");
            }
            else
            {
                Form1.Instance.changeStatus("CAN Connection Error");
                Console.WriteLine("Error");
            }

        }
        public static void SendMessage(PcanStatus resultPass, PcanChannel channelPass)
        {
            PcanMessage msg = new PcanMessage()
            {
                ID = 064,
                DLC = 8,
                MsgType = MessageType.Standard,
                Data = new byte[] { 8, 7, 6, 5 } 
            };

            resultPass = Api.Write(channelPass, msg);
            Console.WriteLine(resultPass);

        }

        public static void ReadMessage()
        {
            Worker myWorker = new Worker();
            myWorker.MessageAvailable += OnMessageAvailable;

            myWorker.Start();
            myWorker.AllowEchoFrames = true;
        }

        private static void OnMessageAvailable(object sender, MessageAvailableEventArgs e)
        {
            Console.WriteLine("Message Recieved");
        }

        public static void Test(PcanMessage msg, ulong timestamp)
        {
            Form1.Instance.changeStatus(msg.ToString());
        }
        

        // Formats a CAN frame as string and writes it  to the console output
        //
        private static void ProcessMessage(PcanMessage msg)
        {
            string msgText = $"Type: {msg.MsgType} | ";
            if ((msg.MsgType & MessageType.Extended) == MessageType.Extended)
                msgText += $"ID: {msg.ID:X8} | ";
            else
                msgText += $"ID: {msg.ID:X4} | ";
            msgText += $"Length: {msg.Length} | ";
            msgText += $"Data: ";
            for (int i = 0; i < msg.Length; i++)
                msgText += $"{msg.Data[i]} ";

            Console.WriteLine(msgText);
        }
    }
}
