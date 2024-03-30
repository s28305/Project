namespace LegacyApp
{
    public interface ICreditData
    {
       bool HasCreditLimit { get; }
       int CreditLimit { get;}
    }
}