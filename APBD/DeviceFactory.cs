namespace APBD;

public interface DeviceFactory
{
    

    public Device CreateDevice(string id, string name = null, bool? isTurnedOn = null, int? battery = null,
        string? system = null, string? ipAddress = null, string? networkName = null);
    
    
}