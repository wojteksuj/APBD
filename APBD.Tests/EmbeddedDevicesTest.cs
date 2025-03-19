using System;
using APBD;
using APBD.Resources;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests;

[TestClass]
[TestSubject(typeof(EmbeddedDevices))]
public class EmbeddedDevicesTest
{

    [TestMethod]
        public void ConstructorTest()
        {
            Device device = new EmbeddedDevices(1, "Device1", false, "192.168.0.1", "MD Ltd. Network");
            Assert.AreEqual(1, device.id);
            Assert.AreEqual("Device1", device.Name);
            Assert.AreEqual(false, device.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: Device1, isTurnedOn: False, ipAdress: 192.168.0.1, networkName: MD Ltd. Network", device.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidIpExceptionTest()
        {
            Device device = new EmbeddedDevices(1, "Device1", false, "wrongIp!!", "MD Ltd. Network");
            
        }

        [TestMethod]
        [ExpectedException(typeof(ConnectionException))]
        public void ConnectionExceptionTest()
        {
            Device device = new EmbeddedDevices(1, "Device1", false, "192.168.0.1", "Wrong Network");
            device.turnOn();
        }

        [TestMethod]
        public void TurnOnTest()
        {
            Device device = new EmbeddedDevices(1, "Device1", false, "192.168.0.1", "MD Ltd. Network");
            device.turnOn();
            Assert.AreEqual(true, device.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: Device1, isTurnedOn: True, ipAdress: 192.168.0.1, networkName: MD Ltd. Network", device.ToString());
        }

        [TestMethod]
        public void TurnOffTest()
        {
            Device device = new EmbeddedDevices(1, "Device1", true, "192.168.0.1", "MD Ltd. Network");
            device.turnOff();
            Assert.AreEqual(false, device.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: Device1, isTurnedOn: False, ipAdress: 192.168.0.1, networkName: MD Ltd. Network", device.ToString());
        }
}