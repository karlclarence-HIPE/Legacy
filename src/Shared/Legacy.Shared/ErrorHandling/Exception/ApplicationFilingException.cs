namespace Legacy.Shared.ErrorHandling.Exception;

public class ApplicationFilingException : System.Exception
{
    public ApplicationFilingException()
    {
    }

    public ApplicationFilingException(string message) : base (message)
    {
    }
}
