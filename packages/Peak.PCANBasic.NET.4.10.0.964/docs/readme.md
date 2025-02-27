# PCAN-Basic.NET Assembly

`PCAN-Basic.NET` is an interface for accessing and using the native PCAN-Basic API in .NET applications. The assembly is written using .NET Standard 2.0 which makes it suitable for developing under Windows and Linux. 

The underlying native library (`PCANBasic.dll` on Windows and `libpcanbasic.so` on Linux), is used to do CAN communication through plug-and-play devices from **PEAK-System Technik GmbH** like *PCAN-USB FD*, and *PCAN-PCI Express*, among others. 

**Note:** the native library files are not part of the `PCAN-Basic.NET` package. See next block for installation instructions. 

## Documentation
For full online API documentation see [docs.peak-system.com/API/PCAN-Basic.Net](https://docs.peak-system.com/API/PCAN-Basic.Net/). Alternatively, the documentation is available as a CHM file. It can be found within the *"**NuGet**"* folder of the PCAN-Basic package, and can also be  downloaded directly using [this link](https://www.peak-system.com/produktcd/Develop/PC%20interfaces/Windows/PCAN-Basic%20API/NuGet/PCANBasic.NET_en.chm).

## Getting Started
### Windows: 
For using `PCAN-Basic.NET` you need:

1. to have the Windows device drivers installed, by using the [PEAK-Drivers Setup](https://www.peak-system.com/quick/DrvSetup),
2. to have any plug-and-play device of the [PCAN series](https://www.peak-system.com/PC-Interfaces.196.0.html?&L=1) attached to your PC, and
3. to have the PCAN-Basic API installed, by using the [PEAK-Drivers Setup](https://www.peak-system.com/quick/DrvSetup) (**recommended**). Alternatively, you can download a [package](https://www.peak-system.com/fileadmin/media/files/pcan-basic.zip) containing all needed files and setting instructions.

**PCAN-Gateway devices:** If you want to communicate to PCAN-Gateways devices using PCAN-Basic, then you need to additionally install the feature "Virtual PCAN-Gateway" within the [PEAK-Drivers Setup](https://www.peak-system.com/quick/DrvSetup). Note that this is only supported on Windows systems.

### For Linux: 
For using `PCAN-Basic.NET` you need:
1. to have the character-based Linux driver (chardev) installed, by compiling the source code yourself. Instructions are found in the [PEAK-System Linux site](http://www.peak-system.com/fileadmin/media/linux/index.htm).
2. to have any plug-and-play device of the [PCAN series](https://www.peak-system.com/PC-Interfaces.196.0.html?&L=1) attached to your PC, and
3. to have the PCAN-Basic API installed, by downloading and compiling the [source files](https://www.peak-system.com/quick/BasicLinux). See instructions in the readme.txt file enclosed in the package.

## Usage

Reference the following namespace from the file where you want to use PCAN-Basic functionality:

C#:

        using Peak.Can.Basic;

VB: 

        Imports Peak.Can.Basic

C++/CLI:

        using namespace Peak::Can::Basic;

The namespace contains type enumerations, structures and classes for PCAN-Basic API accessing. Use the class **Api** to access API methods and constants:

C#:

    PcanStatus result; 
    result = Api.Initialize(PcanChannel.Usb01, Bitrate.Pcan500);

VB:

    Dim result As PcanStatus
    result = Api.Initialize(PcanChannel.Usb01, Bitrate.Pcan500)

C++/CLI:

    using namespace Peak::Can::Basic;
    result = Api::Initialize(channelUsed, Bitrate::Pcan500);


Use the class **Worker** for event driven reading and writing

C#:

    Worker myWorker = new Worker();
    PcanMessage message = new PcanMessage(0x100, MessageType.Standard, 3, new byte[] { 1, 2, 3 }, false);
    Broadcast broadcast = new Broadcast(message, 100);

    myWorker.MessageAvailable += OnMessageAvailable;
    if (myWorker.AddBroadcast(ref broadcast))
        Console.WriteLine($"Broadcast {broadcast.Index}' configured successfully.");

    myWorker.Start();
    Console.WriteLine("The Worker was activated successfully.");    

VB:

    Dim myWorker As Worker = New Worker()
    Dim message As PcanMessage = New PcanMessage(&H100, MessageType.Standard, 3, New Byte() {1, 2, 3}, False)
    Dim broadcast As Broadcast = New Broadcast(message, 100)

    AddHandler myWorker.MessageAvailable, AddressOf OnMessageAvailable    
    If myWorker.AddBroadcast(broadcast) Then
        Console.WriteLine($"Broadcast '{broadcast.Index}' configured successfully.")        
    End If

    myWorker.Start()
    Console.WriteLine("The Worker object was activated successfully.")

C++/CLI:

    Worker^ myWorker = gcnew Worker();
    PcanMessage^ message = gcnew PcanMessage(0x100, MessageType::Standard, 3, gcnew array<Byte> { 1, 2, 3 }, false);
    Broadcast^ broadcast = gcnew Broadcast(message, 100); 

    myWorker->MessageAvailable += gcnew System::EventHandler(&OnMessageAvailable);    
    if (myWorker->AddBroadcast(broadcast))
        Console::WriteLine("Broadcast '{0}' configured successfully.", broadcast->Index); 
    
    myWorker->Start(false, false, false);
    Console::WriteLine("The Worker object was activated successfully.");   



## Need Support?

If you need further information or help, you can contact us using the following channels:
*   Documentation: [Online documentation](https://docs.peak-system.com/API/PCAN-Basic.Net/)
*   Community: [PCAN-Basic Forum](http://www.peak-system.com/forum/viewforum.php?f=41)
*   Phone:  +49 6151 / 8173-20
*   Fax:    +49 6151 / 8173-29
*   E-mail: support@peak-system.com	


PEAK-System team