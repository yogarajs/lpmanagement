using LPManagement.Common;
using LPManagement.DataAccess;
using System.Linq;

namespace LPManagement.BusinessLogic
{
    /// <summary>
    /// Account business logic class.
    /// </summary>
    public class AccountBusinessLogic
    {
        /// <summary>
        /// Authenticates user for the given username and password.
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User object if given username and password are correct else null.</returns>
        public User AuthenticateUser(string userName, string password)
        {
            var accountDataService = new AccountDataService();
            var userDetails = accountDataService.GetUserDetails();
            return userDetails.FirstOrDefault(user => user.UserName == userName && user.Password == password);
        }
    }
}
