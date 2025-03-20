using System.Security.AccessControl;
using APBD.Resources;

namespace APBD;

public class Smartwatch : Device, IPowerNotifier
{
    public int battery;

    public Smartwatch(int id, string name, bool isTurnedOn, int battery)
    {   
        this.id = id;
        Name = name;
        IsTurnedOn = isTurnedOn;
        if(battery < 0 | battery > 100) throw new ArgumentOutOfRangeException("battery", battery, "Battery must be between 0 and 100");
        if(battery < 20) LowBatteryNotification();
        this.battery = battery;
    }

    public void LowBatteryNotification()
    {
        Console.WriteLine("Low battery!");
    }

    public override string ToString()
    {
        return "Id: " + id + " name: " + Name + ", isTurnedOn: " + IsTurnedOn + ", battery: " + battery + "%";
    }

    public override void turnOn()
    {
        if (battery < 11) throw new EmptyBatteryException();
        else
        {
            battery = battery - 10;
            if(battery < 20) LowBatteryNotification();
            IsTurnedOn = true;
        }
    }

    public override void turnOff()
    {
        Console.WriteLine("Smartwatch is turning off...");
        IsTurnedOn = false;
    }
}