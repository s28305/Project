namespace LegacyApp
{
    //Dependency inversion principle allows creating more flexible code and different implementations of retrieving client's data
    public interface IClientRepository
    {
        Client GetById(int clientId);
    }
}