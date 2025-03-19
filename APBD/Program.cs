using APBD;

DeviceManager deviceManager = new DeviceManager("input.txt");
deviceManager.showAllDevices();
Console.WriteLine(deviceManager.devices[0].ToString());