using LPManagement.BusinessLogic;
using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LPManagement.UI.Controllers
{
    /// <summary>
    /// LaunchPad management controller.
    /// </summary>
    public class LPManagementController : Controller
    {
        #region Action methods
        /// <summary>
        /// Index action method
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails == null)
            {
                return RedirectToAction("Index", "Account");
            }

            var launchPadDetailsModel = GetLaunchPadDetails(Location.All, Quarter.All, Status.All, DateTime.Today.Year);

            var locations = Enum.GetValues(typeof(Location)).Cast<Location>().ToList();
            var locationModels = locations.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Location.All)
            });
            ViewBag.Locations = locationModels;

            var status = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            var statusModels = status.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Status.All)
            });
            ViewBag.Status = statusModels;

            var quarter = Enum.GetValues(typeof(Quarter)).Cast<Quarter>().ToList();
            var quarterModels = quarter.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Quarter.All)
            });
            ViewBag.Quarter = quarterModels;

            return View(launchPadDetailsModel);
        }

        /// <summary>
        /// Filters LaunchPad details based on filter criteria.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <returns>ActionResult</returns>
        public ActionResult LPDetails(Location location, Quarter quarter, Status status, int financialYear)
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails == null)
            {
                return RedirectToAction("Index", "Account");
            }

            var launchPadDetailsModel = GetLaunchPadDetails(location, quarter, status, financialYear);
            return PartialView(launchPadDetailsModel);
        }
        #endregion

        #region Private methods
        private List<LaunchPadDetailsModel> GetLaunchPadDetails(Location location, Quarter quarter, Status status, int financialYear)
        {
            var lPManagementBusinessLogic = new LPManagementBusinessLogic();
            var launchPadDetails = lPManagementBusinessLogic.GetLauchPadDetails(location, quarter, status, financialYear).ToList();
            var launchPadDetailsModel = launchPadDetails.Select(x => new LaunchPadDetailsModel()
            {
                LaunchPadCode = x.LaunchPadCode,
                Location = x.Location,
                Status = x.Status,
                Quarter = x.Quarter,
                Technology = x.Technology,
                FinancialYear = x.FinancialYear,
                NoOfTrainees = x.NoOfTrainees,
                NoOfAllocation = x.NoOfAllocation
            }).ToList();

            return launchPadDetailsModel;
        } 
        #endregion
    }
}