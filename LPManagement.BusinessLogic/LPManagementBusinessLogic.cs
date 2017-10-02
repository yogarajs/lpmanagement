using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace LPManagement.BusinessLogic
{
    public class LPManagementBusinessLogic
    {
        public IEnumerable<LaunchPadDetails> GetLauchPadDetails(Location location, Quarter quarter, Status status, int financialYear)
        {
            var lpManagementDataService = new LPManagementDataService();
            var launchPadDetails = lpManagementDataService.GetLaunchPadDetails()
                                    .ToList()
                                    .Where(x => x.Location == location && x.Quarter == quarter && x.Status == status && x.FinancialYear == financialYear);
            return launchPadDetails;
        }
    }
}
