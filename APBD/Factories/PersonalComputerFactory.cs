namespace APBD.Factories;

public class PersonalComputerFactory
{
    public Device CreateDevice(string id, string name = null, bool? isTurnedOn = null, int? battery = null,
        string system = null, string ipAddress = null, string networkName = null)
    {
        return new PersonalComputer(id, name, isTurnedOn.Value, system);
    }
}