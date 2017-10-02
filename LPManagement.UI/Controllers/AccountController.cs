using LPManagement.BusinessLogic;
using LPManagement.Common;
using LPManagement.Common.Enums;
using LPManagement.UI.Models;
using System.Web.Mvc;

namespace LPManagement.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            var userDetails = Session["userDetails"] as User;
            if (userDetails != null)
            {
                return RedirectToAction("Index", "LPManagement");
            }
            return View();
        }

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
    }
}