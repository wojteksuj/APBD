namespace APBD;

public interface DeviceFactory
{
    Device CreateDevice(string id, string name, bool isOn);
}