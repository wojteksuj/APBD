using APBD.Resources;

namespace APBD;

public class PersonalComputer : Device 
{
    public string system = null;

    public PersonalComputer(int id, string name, bool isTurnedOn, String system)
    {
        this.id = id;
        this.Name = name;
        this.IsTurnedOn = isTurnedOn;
        this.system = system;
    }

    public override string ToString()
    {
        return "Id: " + id + " name: " + Name + ", isTurnedOn: " + IsTurnedOn + ", System: " + system;
    }

    public override void turnOn()
    {
        if (system == null) throw new EmptySystemException();
        IsTurnedOn = true;
    }

    public override void turnOff()
    {
        Console.WriteLine("PC is turning off...");
        IsTurnedOn = false;
    }
}