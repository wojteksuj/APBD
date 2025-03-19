namespace APBD.Resources;

public class ConnectionException :Exception
{
    public ConnectionException()
        : base("Wrong network name, connection unsuccessful."){}
}