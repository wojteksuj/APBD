namespace APBD;

public class Smartwatch : Device, IPowerNotifier
{
    int battery;

    public Smartwatch(int id, string name, bool isTurnedOn, int battery)
    {   
        this.id = id;
        Name = name;
        IsTurnedOn = isTurnedOn;
        if(battery < 0 || battery > 100) throw new ArgumentOutOfRangeException("battery", battery, "Battery must be between 0 and 100");
        
        this.battery = battery;
    }

    public String toString()
    {
        return "Id: " + id + " name: " + Name + ", isTurnedOn: " + IsTurnedOn + ", battery: " + battery + "%";
    }

    public void LowBatteryNotification()
    {
        throw new NotImplementedException();
    }
}