namespace APBD;

public class Device
{
    public int id;
    public string Name;
    public bool IsTurnedOn;
    
    public virtual void turnOn(){}

    public virtual void turnOff(){}
    
    
}