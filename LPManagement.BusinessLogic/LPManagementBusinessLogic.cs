using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace LPManagement.BusinessLogic
{
    /// <summary>
    /// LaunchPad management business logic class.
    /// </summary>
    public class LPManagementBusinessLogic
    {
        /// <summary>
        /// Get launch pad details for the given filter criteria.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <returns>List of LaunchPadDetails.</returns>
        public IEnumerable<LaunchPadDetails> GetLauchPadDetails(Location location, Quarter quarter, Status status, int financialYear)
        {
            var lpManagementDataService = new LPManagementDataService();
            var launchPadDetails = lpManagementDataService.GetLaunchPadDetails()
                                    .ToList()
                                    .Where(x=> x.FinancialYear == financialYear);

            if (location != Location.All)
            {
                launchPadDetails = launchPadDetails.Where(x => x.Location == location).ToList();
            }

            if (quarter != Quarter.All)
            {
                launchPadDetails = launchPadDetails.Where(x => x.Quarter == quarter).ToList();
            }

            if (status != Status.All)
            {
                launchPadDetails = launchPadDetails.Where(x => x.Status == status).ToList();
            }
                                    
            return launchPadDetails;
        }
    }
}
