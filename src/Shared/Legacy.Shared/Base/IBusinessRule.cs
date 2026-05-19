namespace Legacy.Shared.Base;

public interface IBusinessRule
{
    bool IsBroken();
       
    string Message { get; }
}
