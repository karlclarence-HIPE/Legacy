namespace Legacy.Framework.FileManager.Exceptions;

public class FileManagerValidationException : System.Exception
{
    public FileManagerValidationException(){}

    public FileManagerValidationException(string message)
        :base(message) 
    {
    }
}
