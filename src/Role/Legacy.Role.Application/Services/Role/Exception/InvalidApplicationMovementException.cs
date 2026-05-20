namespace Legacy.Role.Application.Services.Role.Exception;

public class InvalidApplicationMovementException : System.Exception
{
    public InvalidApplicationMovementException(string message) 
        : base(message)
    {
    }
}
