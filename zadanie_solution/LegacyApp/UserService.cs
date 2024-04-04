using System;

namespace LegacyApp
{
    public class UserService
    {
        private static readonly IClientRepository ClientRepository = new ClientRepository();
        private static readonly DisposableObject DisposableObject = new DisposableObject();
        private static readonly IUserCreditService UserCreditService = new UserCreditService(DisposableObject);

        public static bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            if (CheckInputValidity(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }
            
            var client = ClientRepository.GetById(clientId);
            
            var user = new User(new CreditData())
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            user.CreditData = client.Type switch
            {
                "VeryImportantClient" => new CreditData { HasCreditLimit = false },
                "ImportantClient" => SetCreditData(user, 2),
                _ => SetCreditData(user)
            };
            

            if (CheckCreditLimit(user.CreditData))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        public static bool CheckCreditLimit(CreditData creditData)
        {
            return creditData.HasCreditLimit && creditData.CreditLimit < 500;
        }

        private static int SetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month 
                || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age;
        }

        private static bool CheckInputValidity(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return string.IsNullOrEmpty(firstName) ||
                   string.IsNullOrEmpty(lastName) ||
                   !email.Contains("@") ||
                   !email.Contains(".") || 
                   SetAge(dateOfBirth) < 21;
        }

        private static CreditData SetCreditData(User user, int multiplier = 1)
        {
            var creditLimit = UserCreditService.GetCreditLimit(user.LastName) * multiplier;
            
            return new CreditData { HasCreditLimit = true, CreditLimit = creditLimit };
        }
        
    }
}
