﻿using APBD;

DeviceManager deviceManager = new DeviceManager("input.txt");

deviceManager.showAllDevices();

Console.WriteLine("----------Adding new device!----------");
Device newDevice = new Smartwatch(2, "Apple Watch 4", false, 90);
deviceManager.addDevice(newDevice);
deviceManager.showAllDevices();

Console.WriteLine("----------Removing new device!----------");
deviceManager.removeDevice(newDevice);
deviceManager.showAllDevices();

Console.WriteLine("Current device:");
Console.WriteLine(deviceManager.devices[0].ToString());
deviceManager.devices[0].turnOff();
Console.WriteLine(deviceManager.devices[0].ToString());
Console.WriteLine("Turning it off again with lower % of battery:");
deviceManager.devices[0].turnOn();
Console.WriteLine(deviceManager.devices[0].ToString());