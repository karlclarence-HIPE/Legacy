using Legacy.Shared.Base;

namespace Legacy.Shared.ErrorHandling.Exception;

public class BusinessRuleValidationException : System.Exception
{
    public BusinessRuleValidationException()
    {
    }

    public BusinessRuleValidationException(IBusinessRule rule) : base(rule.Message)
    {
    }
}