using APBD;
using APBD.Factories;

class Program
{
    static void Main()
    {
        
        var factories = new Dictionary<string, DeviceFactory>
        {
            { "SW", new SmartwatchFactory() },
            { "P", new PersonalComputerFactory() },
            { "ED", new EmbeddedDeviceFactory() }
        };

        // Step 2: Initialize the file manager and device manager
        var fileManager = new DeviceFileManager("input.txt");
        var deviceManager = new DeviceManager(fileManager, factories);

        try
        {
            // Step 3: Show all devices currently in the file
            deviceManager.ShowAllDevices();

            Console.WriteLine("----------Adding new device!----------");

            // Step 4: Create and add a new smartwatch device
            Device newDevice = new Smartwatch("SW-2", "Apple Watch 4", false, 80);
            deviceManager.AddDevice(newDevice);

            // Show updated device list
            deviceManager.ShowAllDevices();

            Console.WriteLine("----------Removing new device!----------");

            // Step 5: Remove the recently added device
            deviceManager.RemoveDevice(newDevice);

            // Show updated device list after removal
            deviceManager.ShowAllDevices();

            // Step 6: Display the first device in the list
            Console.WriteLine("Current device:");
            Console.WriteLine(deviceManager.devices[0].ToString());

            // Step 7: Turn off the first device
            deviceManager.devices[0].turnOff();
            Console.WriteLine(deviceManager.devices[0].ToString());

            Console.WriteLine("Turning it on again with lower % of battery:");
            deviceManager.devices[0].turnOn();
            Console.WriteLine(deviceManager.devices[0].ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("General error: " + e.Message);
        }
    }
}