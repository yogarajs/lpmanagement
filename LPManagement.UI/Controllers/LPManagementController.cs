using LPManagement.BusinessLogic;
using LPManagement.Common.Enums;
using LPManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LPManagement.UI.Controllers
{
    /// <summary>
    /// LaunchPad management controller.
    /// </summary>
    public class LPManagementController : Controller
    {
        #region Action methods
        #region GET
        /// <summary>
        /// Index action method
        /// </summary>
        /// <returns>Home view.</returns>
        public ActionResult Index()
        {
            //var userDetails = Session["userDetails"] as User;
            //if (userDetails == null)
            //{
            //    return RedirectToAction("Index", "Account");
            //}

            return View();
        }

        /// <summary>
        /// Upload file action method
        /// </summary>
        /// <returns>View to upload file.</returns>
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }

        /// <summary>
        /// Filters LaunchPad details based on filter criteria.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <param name="practice">Practice.</param>
        /// <returns>Dashoboard view</returns>
        [HttpGet]
        public ActionResult Dashboard(Location location = Location.All, Status status = Status.All, int financialYear = 2017, string practice = "All")
        {
            //var userDetails = Session["userDetails"] as User;
            //if (userDetails == null)
            //{
            //    return RedirectToAction("Index", "Account");
            //}           

            var launchPadDetailsModel = GetLaunchPadDetails(location, status, financialYear, practice);
            return View(launchPadDetailsModel);
        }

        /// <summary>
        /// Shows launch pad detail.
        /// </summary>
        /// <param name="location">Location</param>
        /// <param name="quarter">Quarter</param>
        /// <param name="status">Status</param>
        /// <param name="financialYear">Financial year</param>
        /// <param name="practice">Practice.</param>
        /// <returns>Dashboard content view</returns>
        [HttpGet]
        public ActionResult DashboardContent(Location location = Location.All, Status status = Status.All, int financialYear = 2017, string practice = "All")
        {
            var launchPadDetailsModel = GetLaunchPadDetails(location, status, financialYear, practice);
            return PartialView(launchPadDetailsModel);
        }

        /// <summary>
        /// Shows launch pad detail for a given launch pad code.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult DashboardSummary(string launchPadCode)
        {
            var lPManagementBusinessLogic = new LPManagementBusinessLogic();
            var launchPadSummary = lPManagementBusinessLogic.GetLaunchPadDetailsByLaunchPadCode(launchPadCode);
            var launchPadSummaryModel = new LaunchPadSummaryModel()
            {
                LaunchPadCode = launchPadSummary.LaunchPadCode,
                Status = launchPadSummary.Status,
                TotalNoOfLPCount = launchPadSummary.TotalNoOfLPCount,
                OverallScoreAbove70PercentCount = launchPadSummary.OverallScoreAbove70PercentCount,
                OverallScore60To70PercentCount = launchPadSummary.OverallScore60To70PercentCount,
                OverallScoreBelow60PercentCount = launchPadSummary.OverallScoreBelow60PercentCount
            };

            return PartialView(launchPadSummaryModel);
        }
        #endregion

        #region POST
        /// <summary>
        /// Uploads data file.
        /// </summary>
        /// <param name="file">File to upload</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    file.SaveAs(path);
                    var lPManagementBusinessLogic = new LPManagementBusinessLogic();
                    lPManagementBusinessLogic.ProcessFile(path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        } 
        #endregion
        #endregion

        #region Private methods
        private List<LaunchPadDetailsModel> GetLaunchPadDetails(Location location, Status status, int financialYear, string practice)
        {
            var lPManagementBusinessLogic = new LPManagementBusinessLogic();
            var launchPadDetails = lPManagementBusinessLogic.GetLauchPadDetails(location, Quarter.All, status, financialYear, practice).ToList();
            var launchPadDetailsModel = launchPadDetails.Select(x => new LaunchPadDetailsModel()
            {
                LaunchPadCode = x.LaunchPadCode,
                Location = x.Location,
                FinancialYear = x.FinancialYear,
            }).ToList();

            var locations = Enum.GetValues(typeof(Location)).Cast<Location>().ToList();
            var locationModels = locations.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Location.All)
            });
            ViewBag.Locations = locationModels;

            var statusList = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            var statusModels = statusList.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == Status.All)
            });
            ViewBag.Status = statusModels;

            var financialYearList = new List<int>() { 2012, 2013, 2014, 2015, 2016, 2017, 2018 };
            var financialYearModels = financialYearList.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == DateTime.Today.Year)
            });
            ViewBag.FinancialYear = financialYearModels;

            var practiceList = new List<string>() { "All", "Core", "Core-Citi", "EIM", "QA" };
            var practiceModels = practiceList.Select(x => new SelectListItem
            {
                Text = x.ToString(),
                Value = x.ToString(),
                Selected = (x == "All")
            });
            ViewBag.Practice = practiceModels;

            return launchPadDetailsModel;
        } 
        #endregion
    }
}