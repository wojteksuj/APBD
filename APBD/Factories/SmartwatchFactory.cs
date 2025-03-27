namespace APBD.Factories;

public class SmartwatchFactory : DeviceFactory
{
    public Device CreateDevice(string id, string name = null, bool? isTurnedOn = null, int? battery = null,
        string system = null, string ipAddress = null, string networkName = null)
    {
        return new Smartwatch(id, name, isTurnedOn.Value, battery.Value);
    }
}