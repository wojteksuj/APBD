using APBD;
using APBD.Resources;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APBD.Tests;

[TestClass]
[TestSubject(typeof(PersonalComputer))]
public class PersonalComputerTest
{

    [TestMethod]
        public void ConstructorTest()
        {
            Device pc = new PersonalComputer(1, "MyPC", false, "Windows");
            Assert.AreEqual(1, pc.id);
            Assert.AreEqual("MyPC", pc.Name);
            Assert.AreEqual(false, pc.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyPC, isTurnedOn: False, System: Windows", pc.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(EmptySystemException))]
        public void NullSystemExceptionTest()
        {
            Device pc = new PersonalComputer(1, "MyPC", false, null);
            pc.turnOn();
        }

        [TestMethod]
        public void TurnOnTest()
        {
            Device pc = new PersonalComputer(1, "MyPC", false, "Windows");
            pc.turnOn();
            Assert.AreEqual(true, pc.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyPC, isTurnedOn: True, System: Windows", pc.ToString());
        }

        [TestMethod]
        public void TurnOffTest()
        {
            Device pc = new PersonalComputer(1, "MyPC", true, "Windows");
            pc.turnOff();
            Assert.AreEqual(false, pc.IsTurnedOn);
            Assert.AreEqual("Id: 1 name: MyPC, isTurnedOn: False, System: Windows", pc.ToString());
        }
}