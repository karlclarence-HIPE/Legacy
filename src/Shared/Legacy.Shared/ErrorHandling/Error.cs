namespace Legacy.Shared.ErrorHandling;
public enum ErrorType
{
    Generic, 
    NotFound,
    Unexpected, 
    Validation,
}

public sealed record Error(string Code, ErrorType ErrorType, string Message);