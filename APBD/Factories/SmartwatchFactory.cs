namespace APBD.Factories;

public class SmartwatchFactory : DeviceFactory
{
    public Device CreateDevice(string id, string name, bool isOn, int battery)
    { 
        return new Smartwatch(id, name , isOn, battery);
    }
}