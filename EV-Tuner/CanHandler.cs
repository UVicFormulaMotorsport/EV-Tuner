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
using System.Windows.Forms;

namespace EV_Tuner
{
    class CanHandler
    {
        Form1 mainform;
        private TPCANHandle m_PcanHandle;

        public static void Initialize()
        {
            PcanChannel channel = PcanChannel.Usb01;
            PcanStatus result = Api.Initialize(channel, Bitrate.Pcan250);
            if (result != PcanStatus.OK)
            {
                // An error occurred
                Api.GetErrorText(result, out var errorText);
                Console.WriteLine(errorText);
            }
            else
            {
                // A success message on connection is shown.
                Console.WriteLine($"The hardware represented by the handle {channel} was successfully initialized.");
                PcanMessage msg = new PcanMessage()
                {
                    ID = 0x520,
                    DLC = 1,
                    MsgType = MessageType.Standard,
                    Data = new byte[] { 0x01 }
                };

                result = Api.Write(channel, msg);
                System.Threading.Thread.Sleep(1);
                if (result != PcanStatus.OK)
                {
                    // An error occurred
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                    Console.WriteLine($"Application terminated.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Message was successfully sent.");
                }
                result = Api.Read(channel, out msg);
                if (result == PcanStatus.OK)
                {
                    // Process the received message
                    //
                    if (true)
                    {
                        Console.WriteLine(msg.ToString());
                        string msgText = $"Data: ";
                        for (int i = 0; i < msg.Length; i++)
                        {
                            msgText += $"{msg.Data[i]} ";
                        }
                        string idText = $"ID: ";
                        if ((msg.MsgType & MessageType.Extended) == MessageType.Extended)
                        {
                            idText += $"{msg.ID:X8}";
                        }
                        else
                        {
                            idText += $"{msg.ID:X4}";
                        }
                        Form1.Instance.changeStatus(idText, msgText);
                        ProcessMessage(msg);
                    }
                }

                result = Api.Uninitialize(channel);
                if (result != PcanStatus.OK)
                {
                    // An error occurred
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                }
                else
                {
                    Console.WriteLine($"The hardware represented by the handle {channel} was successfully finalized.");
                }
            }
            //Read message as soon as connect to VCU
            Console.WriteLine("Get motor settings");
        }

        private static void ReadMessage()
        {
            PcanChannel channel = PcanChannel.Usb01;
            PcanStatus result = Api.Read(channel, out PcanMessage msg);
            Task.Run(() =>
            {
                while (result == PcanStatus.OK)
                {
                    ProcessMessage(msg);
                }
                // An error occurred
                if (result != PcanStatus.OK)
                {
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                    System.Threading.Thread.Sleep(10);
                }

                System.Threading.Thread.Sleep(1);
            });
        }

        private static void ProcessMessage(PcanMessage msg)
        {
            switch (msg.ID)
            {
                case 0x520:
                    // Extract 32-bit value from CAN message data
                    uint bitFieldValue = BitConverter.ToUInt32(msg.Data, 0);
                    
                    VCUBitField bitField = new VCUBitField
                    {
                        RawValue = bitFieldValue,
                        Timestamp = DateTime.Now
                    };
                    BitFieldReceived?.Invoke(bitField);
                    break;
                    
                default:
                    // Handle other CAN messages as needed
                    ProcessOtherMessage(msg);
                    break;
            }
        }

        public static void SendMessage()
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
                // A success message on connection is shown.
                //
                Console.WriteLine($"The hardware represented by the handle {channel} was successfully initialized.");

                // Define the message to send
                //
                PcanMessage msg = new PcanMessage()
                {
                    ID = 0x64,
                    DLC = 1,
                    MsgType = MessageType.Standard,
                    Data = new byte[] { 0x01 }
                };

                for (byte i = 1; i <= 10; i++)
                {
                    result = Api.Write(channel, msg);
                    System.Threading.Thread.Sleep(1);
                    if (result != PcanStatus.OK)
                    {
                        // An error occurred
                        //
                        Api.GetErrorText(result, out var errorText);
                        Console.WriteLine(errorText);
                        Console.WriteLine($"Application terminated.");
                        return;
                    }
                    else
                    {
                        // A success message on reset is shown.
                        //
                        Console.WriteLine($"Message {i} was successfully sent.");
                    }
                }

                // Give the driver some time to send the messages...
                //
                System.Threading.Thread.Sleep(50);

                // The connection to the hardware is finalized when it is no longer needed
                //
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
                        if(true)
                        {
                            Console.WriteLine(msg.ToString());
                            ProcessMessage(msg);
                        }
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
        private static void ProcessOtherMessage(PcanMessage msg)
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
            //Form1.Instance.changeStatus(msgText);
        }
    }
}
