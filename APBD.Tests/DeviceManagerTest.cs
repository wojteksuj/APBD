using System;
using System.Collections.Generic;
using System.IO;
using APBD;
using APBD.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests
{
    [TestClass]
    public class DeviceManagerTest
    {
        private string CreateTempFile(string[] lines)
        {
            string filePath = Path.GetTempFileName();
            File.WriteAllLines(filePath, lines);
            return filePath;
        }

        private DeviceManager InitializeDeviceManager(string[] fileLines)
        {
            var factories = new Dictionary<string, DeviceFactory>
            {
                { "SW", new SmartwatchFactory() },
                { "P", new PersonalComputerFactory() },
                { "ED", new EmbeddedDeviceFactory() }
            };
            var deviceFileManager = new DeviceFileManager(CreateTempFile(fileLines));
            return new DeviceManager(deviceFileManager, factories);
        }

        [TestMethod]
        public void AddDeviceTest()
        {
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);
            Device smartwatch = new Smartwatch("SW-1", "Watch", true, 90);

            deviceManager.AddDevice(smartwatch);

            Assert.IsTrue(deviceManager.devices.Contains(smartwatch));
        }

        [TestMethod]
        public void LimitDevicesTest()
        {
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            for (int i = 0; i < 20; i++)
            {
                deviceManager.AddDevice(new Smartwatch($"SW-{i}", $"WatchNo.{i}", true, 80));
            }

            Assert.AreEqual(15, deviceManager.devices.Count);  
        }

        [TestMethod]
        public void RemoveDeviceTest()
        {
            Device smartwatch = new Smartwatch("SW-1", "Watch", true, 90);
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(smartwatch);
            deviceManager.RemoveDevice(smartwatch);

            Assert.IsFalse(deviceManager.devices.Contains(smartwatch));
        }

        [TestMethod]
        public void TurnOnDeviceTest()
        {
            Device smartwatch = new Smartwatch("SW-1", "Watch", false, 90);
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(smartwatch);
            deviceManager.TurnOnDevice(smartwatch);

            Assert.IsTrue(smartwatch.IsTurnedOn);
        }

        [TestMethod]
        public void TurnOffDeviceTest()
        {
            Device smartwatch = new Smartwatch("SW-1", "Watch", true, 90);
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(smartwatch);
            deviceManager.TurnOffDevice(smartwatch);

            Assert.IsFalse(smartwatch.IsTurnedOn);
        }

        [TestMethod]
        public void EditDeviceBatteryTest()
        {
            Smartwatch smartwatch = new Smartwatch("SW-1", "Watch", true, 90);
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(smartwatch);
            deviceManager.EditDeviceData(smartwatch, battery: 80);

            Assert.AreEqual(80, smartwatch.battery);  
        }

        [TestMethod]
        public void EditDeviceDataSystemTest()
        {
            PersonalComputer pc = new PersonalComputer("P-1", "PC", true, "Windows");
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(pc);
            deviceManager.EditDeviceData(pc, newSystem: "Linux");

            Assert.AreEqual("Linux", pc.system);  
        }

        [TestMethod]
        public void EditDeviceDataIpAddressTest()
        {
            EmbeddedDevices embeddedDevice = new EmbeddedDevices("ED-1", "ED", true, "192.168.1.1", "MD Ltd.");
            DeviceManager deviceManager = InitializeDeviceManager(new string[0]);

            deviceManager.AddDevice(embeddedDevice);
            deviceManager.EditDeviceData(embeddedDevice, newIpAddress: "192.168.1.2");

            Assert.AreEqual("192.168.1.2", embeddedDevice.IpAddress);  
        }

        [TestMethod]
        public void SaveDataToFileTest()
        {
            var factories = new Dictionary<string, DeviceFactory>
            {
                { "SW", new SmartwatchFactory() },
                { "P", new PersonalComputerFactory() },
                { "ED", new EmbeddedDeviceFactory() }
            };

            
            DeviceFileManager deviceFileManager = new DeviceFileManager(CreateTempFile(new string[0]));
            DeviceManager deviceManager = new DeviceManager(deviceFileManager, factories);

            
            Device smartwatch = new Smartwatch("SW-1", "Watch", true, 90);
            Device pc = new PersonalComputer("P-1", "PC", true, "Windows");
            Device embeddedDevice = new EmbeddedDevices("ED-1", "ED", true, "192.168.1.1", "MD Ltd.");

            deviceManager.AddDevice(smartwatch);
            deviceManager.AddDevice(pc);
            deviceManager.AddDevice(embeddedDevice);
            
            deviceManager.SaveDataToFile();

            
            string[] savedLines = File.ReadAllLines(deviceFileManager.getFilePath());
            Assert.AreEqual(3, savedLines.Length);  
        }
    }
}
