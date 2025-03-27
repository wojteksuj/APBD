namespace APBD.Factories
{
    public interface DeviceFactory
    {
        Device CreateDevice(string id, string name, bool isTurnedOn, int? battery = null, 
            string system = null, string ipAddress = null, string networkName = null);
    }

    public class SmartwatchFactory : DeviceFactory
    {
        public Device CreateDevice(string id, string name, bool isTurnedOn, int? battery = null, 
            string system = null, string ipAddress = null, string networkName = null)
        {
            return new Smartwatch(id, name, isTurnedOn, battery.Value);
        }
    }

    public class PersonalComputerFactory : DeviceFactory
    {
        public Device CreateDevice(string id, string name, bool isTurnedOn, int? battery = null, 
            string system = null, string ipAddress = null, string networkName = null)
        {
            return new PersonalComputer(id, name, isTurnedOn, system);
        }
    }

    public class EmbeddedDeviceFactory : DeviceFactory
    {
        public Device CreateDevice(string id, string name, bool isTurnedOn, int? battery = null, 
            string system = null, string ipAddress = null, string networkName = null)
        {
            return new EmbeddedDevices(id, name, true, ipAddress , networkName);
        }
    }
}