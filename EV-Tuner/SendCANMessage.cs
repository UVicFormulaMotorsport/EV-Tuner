using Peak.Can.Basic;
using Peak.Can.Basic.BackwardCompatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TPCANHandle = System.UInt16;
using TPCANBitrateFD = System.String;
using TPCANTimestampFD = System.UInt64;

namespace EV_Tuner
{
    class SendCANMessage
    {
        Form1 mainform;
        private TPCANHandle m_PcanHandle;

        /*public static void Initialize()
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

        }*/

        private static void ReadMessage()
        {

        }

        private static void OnMessageAvailable(object sender, MessageAvailableEventArgs e)
        {
            Console.WriteLine("Message Recieved" + e.ToString());
        }

        public static void ReadExample()
        {
            PcanChannel channel = PcanChannel.Usb01;

            PcanStatus result = Api.Initialize(channel, Bitrate.Pcan250);
            if (result != PcanStatus.OK)
            {
                // An error occurred
                //
                Api.GetErrorText(result, out var errorText);
                Console.WriteLine(errorText);
            }
            else
            {
                Console.WriteLine($"The hardware represented by the handle {channel} was successfully initialized.");
                Console.WriteLine("The reception queue will be read out after 1 second...");
                System.Threading.Thread.Sleep(1000);
                PcanMessage msg;
                do
                {
                    result = Api.Read(channel, out msg);
                    if (result == PcanStatus.OK)
                    {
                        // Process the received message
                        //
                        Console.WriteLine(msg.ToString());
                        ProcessMessage(msg);
                    }
                    else
                    {
                        if ((result & PcanStatus.ReceiveQueueEmpty) != PcanStatus.ReceiveQueueEmpty)
                        {
                            // An unexpected error occurred
                            //
                            Api.GetErrorText(result, out var errorText);
                            Console.WriteLine("Reading process canceled due to unexpected error: " + errorText);
                            break;
                        }
                    }

                } while ((result & PcanStatus.ReceiveQueueEmpty) != PcanStatus.ReceiveQueueEmpty);


                result = Api.Uninitialize(channel);
                if (result != PcanStatus.OK)
                {
                    // An error occurred
                    //
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                }
                else
                {
                    Console.WriteLine($"The hardware represented by the handle {channel} was successfully finalized.");
                }
            }
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
