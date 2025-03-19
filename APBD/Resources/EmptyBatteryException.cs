namespace APBD.Resources;

public class EmptyBatteryException : Exception
{
    public EmptyBatteryException()
        : base("The battery is empty."){}


}