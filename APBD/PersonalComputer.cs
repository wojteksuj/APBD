namespace APBD;

public class PersonalComputer : Device
{
    String system = null;

    public PersonalComputer(int id, string name, bool isTurnedOn, String system)
    {
        this.id = id;
        this.Name = name;
        this.IsTurnedOn = isTurnedOn;
        this.system = system;
    }
    
    
    
}