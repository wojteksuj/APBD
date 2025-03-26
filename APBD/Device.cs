namespace APBD;

public class Device
{
    public string id;
    public string Name;
    public bool IsTurnedOn;
    
    public virtual void turnOn(){}

    public virtual void turnOff(){}
    
    
}