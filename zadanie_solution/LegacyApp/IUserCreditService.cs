namespace LegacyApp
{
    //Dependency inversion principle allows creating different ways of interacting with users' credit data
    public interface IUserCreditService
    {
        int GetCreditLimit(string lastName);
    }
}