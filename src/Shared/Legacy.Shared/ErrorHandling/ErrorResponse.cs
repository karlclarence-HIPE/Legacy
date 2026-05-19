namespace Legacy.Shared.ErrorHandling;

public sealed record ErrorResponse(string Code, int ErrorType, string Message);
