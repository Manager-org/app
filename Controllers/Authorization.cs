using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [Area("Authorization")]
    public class Authorization : Controller
    {
        const string
        _PAGE_AUTHORIZATION_LOGIN = "Views/Pages/Authorization/Login.cshtml",
        _PAGE_AUTHORIZATION_REGISTRATION = "Views/Pages/Authorization/Registration.cshtml";

        [Route("auth/login")]
        public IActionResult Login()
        {
            var cookies = new Utils.Cookies(Request, Response);

            ViewBag.ViewErrors = new Models.Errors.Account.Login();

            if (cookies.Find())
                return Redirect("/project/list");

            return View(_PAGE_AUTHORIZATION_LOGIN);
        }

        [HttpPost]
        [Route("auth/login")]
        public IActionResult Login(Models.Account account)
        {
            var cookies = new Utils.Cookies(Request, Response);

            if (
                (
                    (account.Username != null)
                    && (account.Password != null)
                )
                && Utils.Verification.IsGoodText(account.Username)
                && Utils.Verification.IsGoodText(account.Password)
                && (account.Password.ToList().Count() >= 3)
                && (new Utils.DB.RuleHandle()).Accounts.Exists(new Models.Account(account.Username, account.Password))
            )
            {
                var temp = (new Utils.DB.RuleHandle()).Accounts.GetAccount(account);

                temp.Password = account.Password;

                if (cookies.Find())
                    cookies.Unset();

                cookies.Set(temp);

                if (
                    temp.IsAdmin
                    || ((new Utils.DB.RuleHandle()).Persons.ExistsPerson(temp))
                )
                    return Redirect("/project/list");

                return Redirect("/person/wait");
            }
            else
            {
                var viewErrors = new Models.Errors.Account.Login();

                if (
                    (account.Username == null)
                    || (account.Username == "")
                    || !Utils.Verification.IsGoodText(account.Username)
                )
                    viewErrors.Username = true;

                if (
                    (account.Password == null)
                    || (account.Password == "")
                    || !Utils.Verification.IsGoodText(account.Password)
                )
                    viewErrors.Password = true;

                viewErrors.IsError = true;

                ViewBag.ViewErrors = viewErrors;

                return View(_PAGE_AUTHORIZATION_LOGIN);
            }
        }

        [Route("auth/logout")]
        public IActionResult Logout()
        {
            var cookies = new Utils.Cookies(Request, Response);

            cookies.Unset();

            return Redirect("/auth/login");
        }

        [Route("auth/registration")]
        public IActionResult Registration()
        {
            ViewBag.ViewErrors = new Models.Errors.Account.Registration();

            return View(_PAGE_AUTHORIZATION_REGISTRATION);
        }

        [HttpPost]
        [Route("auth/registration")]
        public IActionResult Registration(Models.Account account)
        {
            if (
                (
                    (account.Username != null)
                    && (account.Password != null)
                    && (account.Name != null)
                    && (account.Surname != null)
                    && (account.MiddleName != null)
                )
                && Utils.Verification.IsGoodText(account.Username)
                && Utils.Verification.IsGoodText(account.Password)
                && Utils.Verification.IsGoodText(account.Name)
                && Utils.Verification.IsGoodText(account.Surname)
                && Utils.Verification.IsGoodText(account.MiddleName)
                && (account.Password.ToList().Count() >= 3)
                && !(new Utils.DB.RuleHandle()).Accounts.Exists(new Models.Account(account.Username))
                && (new Utils.DB.RuleHandle()).Accounts.Add(account)
            )
                return Redirect("/auth/login");
            else
            {
                var viewErrors = new Models.Errors.Account.Registration();

                if (
                    (account.Username == null)
                    || (account.Username == "")
                    || !Utils.Verification.IsGoodText(account.Username)
                )
                    viewErrors.Username = true;

                if (
                    (account.Password == null)
                    || (account.Password == "")
                    || !Utils.Verification.IsGoodText(account.Password)
                )
                    viewErrors.Password = true;

                if (
                    (account.Name == null)
                    || (account.Password == "")
                    || !Utils.Verification.IsGoodText(account.Name)
                )
                    viewErrors.Name = true;

                if (
                    (account.Surname == null)
                    || (account.Surname == "")
                    || !Utils.Verification.IsGoodText(account.Surname)
                )
                    viewErrors.Surname = true;

                if (
                    (account.MiddleName == null)
                    || (account.MiddleName == "")
                    || !Utils.Verification.IsGoodText(account.MiddleName)
                )
                    viewErrors.MiddleName = true;

                viewErrors.IsError = true;

                ViewBag.ViewErrors = viewErrors;

                return View(_PAGE_AUTHORIZATION_REGISTRATION);
            }
        }
    }
}
