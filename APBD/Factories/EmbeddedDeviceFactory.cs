namespace APBD.Factories;

public class EmbeddedDeviceFactory : DeviceFactory
{
    public Device CreateDevice(string id, string name = null, bool? isTurnedOn = null, int? battery = null,
        string system = null, string ipAddress = null, string networkName = null)
    {
        return new EmbeddedDevices(id, name, isTurnedOn.Value, ipAddress, networkName);
    }
}