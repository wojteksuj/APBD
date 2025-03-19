namespace APBD.Resources;

public class EmptySystemException : SystemException
{
    public EmptySystemException()
        : base("The system is not installed."){}
}