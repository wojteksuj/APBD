using System.Net;
using System.Text.RegularExpressions;
using APBD.Resources;

namespace APBD;

public class EmbeddedDevices : Device
{
    public string IpAddress;
    public string NetworkName;

    public EmbeddedDevices(int id, string Name, bool isTurnedOn, string ipAddress, string networkName)
    {
        if(Regex.IsMatch(ipAddress,@"^([0-9]{1,3}\.){3}[0-9]{1,3}$")) this.IpAddress = ipAddress;
        else throw new ArgumentException("Invalid IP Address");
        this.id = id;
        this.Name = Name;
        this.IsTurnedOn = isTurnedOn;
        NetworkName = networkName;
    }
    
    public override string ToString()
    {
        return "Id: " + id + " name: " + Name + ", isTurnedOn: " + IsTurnedOn + ", ipAdress: " + IpAddress + ", networkName: " + NetworkName;
    }

    public override void turnOn()
    {
        connect();
        IsTurnedOn = true;
    }

    public override void turnOff()
    {
        Console.WriteLine("Device is turning off...");
        IsTurnedOn = false;
    }

    public void connect()
    {
        if (!NetworkName.Contains("MD Ltd.")) throw new ConnectionException();
    }
}