namespace APBD;

public interface DeviceFactory
{
    //Device CreateDevice(string id, string name, bool isOn);

    public Device CreateDevice(string id, string name = null, bool? isTurnedOn = null, int? battery = null,
        string system = null, string ipAddress = null, string networkName = null);

}