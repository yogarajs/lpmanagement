using LPManagement.BusinessLogic;
using LPManagement.Common;
using LPManagement.UI.Models;
using System.Web.Mvc;

namespace LPManagement.UI.Controllers
{
    /// <summary>
    /// Account controller class.
    /// </summary>
    public class AccountController : Controller
    {
        #region Action methods
        /// <summary>
        /// Index action method
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails != null)
            {
                return RedirectToAction("Index", "LPManagement");
            }
            return View();
        }

        /// <summary>
        /// Login action methods
        /// </summary>
        /// <param name="accountModel">The account model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Login(AccountModel accountModel)
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails != null)
            {
                return RedirectToAction("Index", "LPManagement");
            }
            else
            {
                var accountBusinessLogic = new AccountBusinessLogic();
                var user = accountBusinessLogic.AuthenticateUser(accountModel.UserName, accountModel.Password);
                if (user == null)
                {
                    ViewBag.Message = "Username not exists or User name/password is not correct";
                    return RedirectToAction("Index", "Account");
                }
                Session["userDetails"] = user;
                return RedirectToAction("Index", "LPManagement");
            }
        } 
        #endregion
    }
}