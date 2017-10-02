using LPManagement.BusinessLogic;
using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPManagement.UI.Controllers
{
    public class LPManagementController : Controller
    {
        // GET: LPManagement
        public ActionResult Index()
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails == null)
            {
                return RedirectToAction("Index", "Account");
            }

            var launchPadDetailsModel = GetLaunchPadDetails(Location.CHN, Quarter.Q1, Status.Completed, DateTime.Today.Year);

            var locations = Enum.GetValues(typeof(Location)).Cast<Location>().ToList();
            locations.Remove(Location.None);
            var locationModels = locations.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Location.CHN)
            });
            ViewBag.Locations = locationModels;

            var status = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();            
            var statusModels = status.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
            });
            ViewBag.Status = statusModels;

            var quarter = Enum.GetValues(typeof(Quarter)).Cast<Quarter>().ToList();
            var quarterModels = status.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
            });
            ViewBag.Quarter = quarterModels;

            return View(launchPadDetailsModel);
        }

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
    }
}