using Microsoft.AspNetCore.Mvc;

namespace Manager.Controllers
{
    [Area("Project")]
    public class Project : Controller
    {
        const string
        _PAGE_PROJECT_CREATE = "Views/Pages/Project/Create.cshtml",
        _PAGE_PROJECT_LIST = "Views/Pages/Project/List.cshtml",
        _PAGE_PROJECT_DELETE = "Views/Pages/Project/Delete.cshtml",
        _PAGE_PROJECT_SHOW = "Views/Pages/Project/Show.cshtml",
        _PAGE_PROJECT_EDIT = "Views/Pages/Project/Edit.cshtml",
        _PAGE_PROJECT_EDIT_REQUIREMENTS = "Views/Pages/Project/EditRequirements.cshtml",
        _PAGE_PROJECT_EDIT_ARCHITECTURE = "Views/Pages/Project/EditArchitecture.cshtml",
        _PAGE_PROJECT_EDIT_IMPLEMENTATION = "Views/Pages/Project/EditImplementation.cshtml",
        _PAGE_PROJECT_EDIT_TESTING = "Views/Pages/Project/EditTesting.cshtml";

        [Route("project/create")]
        public IActionResult Create()
        {
            var cookies = new Utils.Cookies(Request, Response);

            if (cookies.Find())
            {
                ViewBag.ViewErrors = new Models.Errors.Project.Create();

                return View(_PAGE_PROJECT_CREATE);
            }
            else return Redirect("/auth/login");
        }

        [HttpPost]
        [Route("project/create")]
        public IActionResult Create(string name, int priceHour)
        {
            var cookies = new Utils.Cookies(Request, Response);

            if (
                (name != null)
                && Utils.Verification.IsGoodText(name)
                && (new Utils.DB.RuleHandle()).Projects.Add(new Models.Project(name, priceHour))
                )
            {
                return Redirect("/project/list");
            }
            else
            {
                var viewErrors = new Models.Errors.Project.Create();

                if ((name != null) && !Utils.Verification.IsGoodText(name))
                    viewErrors.Name = true;

                ViewBag.ViewErrors = viewErrors;

                return View(_PAGE_PROJECT_CREATE);
            }
        }

        [Route("project/list")]
        public IActionResult List()
        {
            var cookies = new Utils.Cookies(Request, Response);

            ViewBag.Projects = (new Utils.DB.RuleHandle()).Projects.GetListShort();
            ViewBag.IsAdmin = (new Utils.DB.RuleHandle()).Accounts.IsAdmin(cookies.Get());
            ViewBag.CurrentProject = (new Utils.DB.RuleHandle()).Projects.GetListShort().SelectMany(x => (new Utils.DB.RuleHandle()).Persons.GetList(new Models.Project(x.ID))).ToList().Find(x => x.Account?.ID == cookies.Get().ID);

            return View(_PAGE_PROJECT_LIST);
        }

        [Route("project/{id?}/delete")]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                (new Utils.DB.RuleHandle()).Persons.GetList(new Models.Project(id.Value)).ForEach(x => (new Utils.DB.RuleHandle()).Persons.Remove(new Models.Person(new Models.Account(x.Account?.ID), new Models.Project(x.Project?.ID))));

                if ((new Utils.DB.RuleHandle()).Projects.Remove(new Models.Project(id.Value)))
                    return Redirect("/project/list");
                else return Redirect($"/project/{id}/edit");
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/show")]
        public IActionResult Show(int? id)
        {
            if (id != null)
            {
                var cookies = new Utils.Cookies(Request, Response);
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                temp.Persons = (new Utils.DB.RuleHandle()).Persons.GetList(new Models.Project(temp.ID)).ToList();

                ViewBag.Project = temp;
                ViewBag.IsAdmin = (new Utils.DB.RuleHandle()).Accounts.IsAdmin(cookies.Get());

                if (!ViewBag.IsAdmin)
                {
                    ViewBag.Person = (new Utils.DB.RuleHandle()).Persons.GetPerson(cookies.Get());

                    if (ViewBag.Person.Project.ID != temp.ID)
                        ViewBag.Person = null;
                }

                return View(_PAGE_PROJECT_SHOW);
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/edit")]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                ViewBag.ErrorView = new Models.Errors.Project.Create();
                ViewBag.Project = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                return View(_PAGE_PROJECT_EDIT);
            }
            else return Redirect("/project/list");
        }

        [HttpPost]
        [Route("project/{id?}/edit")]
        public IActionResult Edit(int? id, string? name, int? priceHour)
        {
            if (
                (id != null)
                && (name != null)
                && (priceHour != null)
                && (Utils.Verification.IsGoodText(name))
            )
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                (new Utils.DB.RuleHandle()).Projects.Update(temp, new Models.Project(name, priceHour.Value, temp.TypeRequirementsHours, temp.TypeArchitectureHours, temp.TypeImplementationHours, temp.TypeTestingHours));

                return Redirect($"/project/{id}/edit");
            }
            else if (
                (id != null)
                && (name != null)
                && (priceHour != null)
            )
            {
                var viewErrors = new Models.Errors.Project.Create();

                if (Utils.Verification.IsGoodText(name))
                    viewErrors.Name = true;

                ViewBag.ViewErrors = viewErrors;
                ViewBag.Project = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                return View(_PAGE_PROJECT_EDIT);
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/edit/requirements")]
        public IActionResult EditRequirements(int? id)
        {
            if (id != null)
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));
                var assignedAccounts = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeRequirements == 1).Select(y => y.Account).ToList();
                var unassignedAccounts = (new Utils.DB.RuleHandle()).Accounts.GetList().Where(x => !x.IsAdmin && ((new Utils.DB.RuleHandle()).Persons.GetList().Where(y => x.ID == y.Account?.ID).ToList().Count() == 0)).ToList();

                ViewBag.Project = temp;
                ViewBag.AssignedAccounts = assignedAccounts;
                ViewBag.UnassignedAccounts = unassignedAccounts;

                return View(_PAGE_PROJECT_EDIT_REQUIREMENTS);
            }
            else return Redirect("/project/list");
        }

        [HttpPost]
        [Route("project/{id?}/edit/requirements")]
        public IActionResult EditRequirements(int? id, int? typeRequirementsHours)
        {
            if ((id != null) && (typeRequirementsHours != null))
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                temp.Persons = (new Utils.DB.RuleHandle()).Persons.GetList(new Models.Project(temp.ID)).Where(x => x.TypeRequirements == 1).ToList();

                (new Utils.DB.RuleHandle()).Projects.Update(temp, new Models.Project(temp.Name, temp.PriceHour, typeRequirementsHours.Value, temp.TypeArchitectureHours, temp.TypeImplementationHours, temp.TypeTestingHours));

                ViewBag.Project = temp;

                return Redirect($"/project/{id}/edit/requirements");
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/edit/architecture")]
        public IActionResult EditArchitecture(int? id)
        {
            ViewBag.ErrorLogin = new Models.Errors.Account.Login();

            if (id != null)
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));
                var assignedAccounts = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeArchitecture == 1).Select(y => y.Account).ToList();
                var unassignedAccounts = (new Utils.DB.RuleHandle()).Accounts.GetList().Where(x => !x.IsAdmin && ((new Utils.DB.RuleHandle()).Persons.GetList().Where(y => x.ID == y.Account?.ID).ToList().Count() == 0)).ToList();

                ViewBag.Project = temp;
                ViewBag.AssignedAccounts = assignedAccounts;
                ViewBag.UnassignedAccounts = unassignedAccounts;

                return View(_PAGE_PROJECT_EDIT_ARCHITECTURE);
            }
            else return Redirect("/project/list");
        }

        [HttpPost]
        [Route("project/{id?}/edit/architecture")]
        public IActionResult EditArchitecture(int? id, int? typeArchitectureHours)
        {
            if ((id != null) && (typeArchitectureHours != null))
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                temp.Persons = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeRequirements == 1).ToList();

                (new Utils.DB.RuleHandle()).Projects.Update(temp, new Models.Project(temp.Name, temp.PriceHour, temp.TypeRequirementsHours, typeArchitectureHours.Value, temp.TypeImplementationHours, temp.TypeTestingHours));

                ViewBag.Project = temp;

                return Redirect($"/project/{id}/edit/architecture");
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/edit/implementation")]
        public IActionResult EditImplementation(int? id)
        {
            ViewBag.ErrorLogin = new Models.Errors.Account.Login();

            if (id != null)
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));
                var assignedAccounts = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeImplementation == 1).Select(y => y.Account).ToList();
                var unassignedAccounts = (new Utils.DB.RuleHandle()).Accounts.GetList().Where(x => !x.IsAdmin && ((new Utils.DB.RuleHandle()).Persons.GetList().Where(y => x.ID == y.Account?.ID).ToList().Count() == 0)).ToList();

                ViewBag.Project = temp;
                ViewBag.AssignedAccounts = assignedAccounts;
                ViewBag.UnassignedAccounts = unassignedAccounts;

                return View(_PAGE_PROJECT_EDIT_IMPLEMENTATION);
            }
            else return Redirect("/project/list");
        }

        [HttpPost]
        [Route("project/{id?}/edit/implementation")]
        public IActionResult EditImplementation(int? id, int? typeImplementationHours)
        {
            if ((id != null) && (typeImplementationHours != null))
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                temp.Persons = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeRequirements == 1).ToList();

                (new Utils.DB.RuleHandle()).Projects.Update(temp, new Models.Project(temp.Name, temp.PriceHour, temp.TypeRequirementsHours, temp.TypeArchitectureHours, typeImplementationHours.Value, temp.TypeTestingHours));

                ViewBag.Project = temp;

                return Redirect($"/project/{id}/edit/implementation");
            }
            else return Redirect("/project/list");
        }

        [Route("project/{id?}/edit/testing")]
        public IActionResult EditTesting(int? id)
        {
            ViewBag.ErrorLogin = new Models.Errors.Account.Login();

            if (id != null)
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));
                var assignedAccounts = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeTesting == 1).Select(y => y.Account).ToList();
                var unassignedAccounts = (new Utils.DB.RuleHandle()).Accounts.GetList().Where(x => !x.IsAdmin && ((new Utils.DB.RuleHandle()).Persons.GetList().Where(y => x.ID == y.Account?.ID).ToList().Count() == 0)).ToList();

                ViewBag.Project = temp;
                ViewBag.AssignedAccounts = assignedAccounts;
                ViewBag.UnassignedAccounts = unassignedAccounts;

                return View(_PAGE_PROJECT_EDIT_TESTING);
            }
            else return Redirect("/project/list");
        }

        [HttpPost]
        [Route("project/{id?}/edit/testing")]
        public IActionResult EditTesting(int? id, int? typeTestingHours)
        {
            if ((id != null) && (typeTestingHours != null))
            {
                var temp = (new Utils.DB.RuleHandle()).Projects.GetProject(new Models.Project(id.Value));

                temp.Persons = (new Utils.DB.RuleHandle()).Persons.GetList(temp).Where(x => x.TypeRequirements == 1).ToList();

                (new Utils.DB.RuleHandle()).Projects.Update(temp, new Models.Project(temp.Name, temp.PriceHour, temp.TypeRequirementsHours, temp.TypeArchitectureHours, temp.TypeImplementationHours, typeTestingHours.Value));

                ViewBag.Project = temp;

                return Redirect($"/project/{id}/edit/testing");
            }
            else return Redirect("/project/list");
        }
    }
}
