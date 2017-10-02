using LPManagement.Common;
using LPManagement.DataAccess;
using System.Linq;

namespace LPManagement.BusinessLogic
{
    public class AccountBusinessLogic
    {
        public User AuthenticateUser(string userName, string password)
        {
            var accountDataService = new AccountDataService();
            var userDetails = accountDataService.GetUserDetails();
            return userDetails.FirstOrDefault(user => user.UserName == userName && user.Password == password);
        }
    }
}
