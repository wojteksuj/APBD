using System;
using System.IO;
using APBD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests;

[TestClass]
public class DeviceManagerTest
{
        private string CreateTempFile(string[] lines)
        {
            string filePath = Path.GetTempFileName();
            File.WriteAllLines(filePath, lines);
            return filePath;
        }

        [TestMethod]
        public void AddDeviceTest()
        {
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            Device smartwatch = new Smartwatch(1, "Watch", true, 90);
            
            deviceManager.addDevice(smartwatch);
            
            Assert.IsTrue(deviceManager.devices.Contains(smartwatch));
        }

        [TestMethod]
        public void LimitDevicesTest()
        {
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            
            for (int i = 0; i < 20; i++)
            {
                deviceManager.addDevice(new Smartwatch(i, "WatchNo." + i, true, 80));
            }
            Assert.IsTrue(deviceManager.devices.Count == 15);
        }

        [TestMethod]
        public void RemoveDeviceTest()
        {
            Device smartwatch = new Smartwatch(1, "Watch", true, 90);
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(smartwatch);
            
            deviceManager.removeDevice(smartwatch);
            Assert.IsFalse(deviceManager.devices.Contains(smartwatch));
        }

        [TestMethod]
        public void TurnOnDeviceTest()
        {
            Device smartwatch = new Smartwatch(1, "Watch", false, 90);
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(smartwatch);
            
            deviceManager.turnOnDevice(smartwatch);
            Assert.IsTrue(smartwatch.IsTurnedOn);
        }

        [TestMethod]
        public void TurnOffDeviceTest()
        {
            Device smartwatch = new Smartwatch(1, "Watch", true, 90);
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(smartwatch);
            
            deviceManager.turnOffDevice(smartwatch);
            Assert.IsFalse(smartwatch.IsTurnedOn);
        }

        [TestMethod]
        public void EditDeviceBatteryTest()
        {
            Smartwatch smartwatch = new Smartwatch(1, "Watch", true, 90);
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(smartwatch);
            
            deviceManager.editDeviceData(smartwatch, battery: 80);
            
            Assert.AreEqual(80, smartwatch.battery);
        }

        [TestMethod]
        public void EditDeviceDataSystemTest()
        {
            PersonalComputer pc = new PersonalComputer(1, "PC", true, "Windows");
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(pc);
            deviceManager.editDeviceData(pc, newSystem: "Linux");
            
            Assert.AreEqual("Linux", pc.system);
        }

        [TestMethod]
        public void EditDeviceDataIpAddressTest()
        {
            EmbeddedDevices embeddedDevice = new EmbeddedDevices(1, "ED", true, "192.168.1.1", "MD Ltd.");
            DeviceManager deviceManager = new DeviceManager(CreateTempFile(new string[0]));
            deviceManager.addDevice(embeddedDevice);
            deviceManager.editDeviceData(embeddedDevice, newIpAddress: "192.168.1.2");
            
            Assert.AreEqual("192.168.1.2", embeddedDevice.IpAddress);
        }

        [TestMethod]
        public void SaveDataToFileTest()
        {
            var tempFilePath = CreateTempFile(new string[0]);
            DeviceManager deviceManager = new DeviceManager(tempFilePath);
            Device smartwatch = new Smartwatch(1, "Watch", true, 90);
            Device  pc = new PersonalComputer(2, "PC", true, "Windows");
            Device  embeddedDevice = new EmbeddedDevices(3, "ED", true, "192.168.1.1", "MD Ltd.");

            deviceManager.addDevice(smartwatch);
            deviceManager.addDevice(pc);
            deviceManager.addDevice(embeddedDevice);
            
            deviceManager.saveDataToFile(tempFilePath);
            
            string[] savedLines = File.ReadAllLines(tempFilePath);
            Assert.AreEqual(3, savedLines.Length); 
        }
}
    
    
    
    
