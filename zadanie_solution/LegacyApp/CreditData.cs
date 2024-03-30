namespace LegacyApp
{
    public class CreditData: ICreditData
    { 
        public bool HasCreditLimit { get; set; }
        public int CreditLimit { get; set; }
    }
}