using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [Area("Person")]
    public class Person : Controller
    {
        const string
        _PAGE_PERSON_LIST = "Views/Pages/Person/List.cshtml",
        _PAGE_PERSON_WAIT = "Views/Pages/Person/Wait.cshtml";

        [Route("person/list")]
        public IActionResult List()
        {
            var cookies = new Utils.Cookies(Request, Response);

            ViewBag.Persons = (new Utils.DB.RuleHandle()).Projects.GetListShort().SelectMany(x => (new Utils.DB.RuleHandle()).Persons.GetList(new Models.Project(x.ID))).ToList();
            ViewBag.Personal = cookies.Get();

            return View(_PAGE_PERSON_LIST);
        }

        [Route("person/wait")]
        public IActionResult Wait()
        {
            var cookies = new Utils.Cookies(Request, Response);
            var account = (new Utils.DB.RuleHandle()).Accounts.GetAccount(cookies.Get());

            if (
                    (account.IsAdmin)
                    || (new Utils.DB.RuleHandle()).Persons.ExistsPerson(account)
                )
                return Redirect("/project/list");

            return View(_PAGE_PERSON_WAIT);
        }

        [Route("person/delete")]
        public IActionResult Delete()
        {
            var cookies = new Utils.Cookies(Request, Response);

            if ((new Utils.DB.RuleHandle()).Accounts.Remove(new Models.Account(cookies.Get().ID)))
            {
                cookies.Unset();

                return Redirect("/");
            }
            else return Redirect("/project/list");
        }

        [HttpGet]
        [Route("person/assign")]
        public IActionResult Assign(int idAccount, int idProject, bool typeRequirements, bool typeArchitecture, bool typeImplementation, bool typeTesting)
        {
            var account = (new Utils.DB.RuleHandle()).Accounts.GetAccount(new Models.Account(idAccount));
            var project = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(idProject));

            if ((new Utils.DB.RuleHandle()).Persons.Add(new Models.Person(typeRequirements ? 1 : 0, typeArchitecture ? 1 : 0, typeImplementation ? 1 : 0, typeTesting ? 1 : 0, account, project)))
            {
                if (typeRequirements)
                    return Redirect($"/project/{idProject}/edit/requirements");

                if (typeArchitecture)
                    return Redirect($"/project/{idProject}/edit/architecture");

                if (typeImplementation)
                    return Redirect($"/project/{idProject}/edit/implementation");

                if (typeTesting)
                    return Redirect($"/project/{idProject}/edit/testing");

                return Redirect($"/project/{idProject}/edit");
            }
            else throw new Exception("Произошла ошибка при добавлении рабочего");
        }

        [HttpGet]
        [Route("person/unassign")]
        public IActionResult Unassign(int idAccount, int idProject, bool typeRequirements, bool typeArchitecture, bool typeImplementation, bool typeTesting)
        {
            var account = (new Utils.DB.RuleHandle()).Accounts.GetAccount(new Models.Account(idAccount));
            var project = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(idProject));

            if ((new Utils.DB.RuleHandle()).Persons.Remove(new Models.Person(account, project)))
            {
                if (typeRequirements)
                    return Redirect($"/project/{idProject}/edit/requirements");

                if (typeArchitecture)
                    return Redirect($"/project/{idProject}/edit/architecture");

                if (typeImplementation)
                    return Redirect($"/project/{idProject}/edit/implementation");

                if (typeTesting)
                    return Redirect($"/project/{idProject}/edit/testing");

                return Redirect($"/project/{idProject}/edit");
            }
            else throw new Exception("Произошла ошибка при добавлении рабочего");
        }
    }
}
