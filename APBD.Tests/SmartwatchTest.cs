using System;
using APBD;
using APBD.Resources;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests;

[TestClass]
[TestSubject(typeof(Smartwatch))]
public class SmartwatchTest
{
[TestMethod]
        public void ConstructorTest()
        {
            Device watch = new Smartwatch("1", "MyWatch", false, 50);
            
            Assert.AreEqual("1", watch.id);
            Assert.AreEqual("MyWatch", watch.Name);
            Assert.AreEqual(false, watch.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyWatch, isTurnedOn: False, battery: 50%", watch.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConstructorExceptionTest()
        {
            Device watch = new Smartwatch("1", "MyWatch", false, 150);
        }
        

        [TestMethod]
        [ExpectedException(typeof(EmptyBatteryException))]
        public void EmptyBatteryExceptionTest()
        {
            Device watch = new Smartwatch("1", "MyWatch", false, 5);
            watch.turnOn();
        }

        [TestMethod]
        public void TurnOnTest()
        {
            Device watch = new Smartwatch("1", "MyWatch", false, 50);
            watch.turnOn();
            Assert.AreEqual(true, watch.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyWatch, isTurnedOn: True, battery: 40%", watch.ToString());
        }

        [TestMethod]
        public void TurnOffTest()
        {
            Device watch = new Smartwatch("1", "MyWatch", true, 50);
            watch.turnOff();
            Assert.AreEqual(false, watch.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyWatch, isTurnedOn: False, battery: 50%", watch.ToString());
        }
    
}