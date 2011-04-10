using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstAltairis.Models;

namespace CodeFirstAltairis.Controllers
{   
    public class UsersController : Controller
    {
		private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new UserRepository(), new RoleRepository())
        {
        }

        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
			this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        //
        // GET: /Users/
        [Authorize]
        public ViewResult Index()
        {
            return View(userRepository.AllIncluding(user => user.Roles));
        }

        //
        // GET: /Users/Details/5
        [Authorize]
        public ViewResult Details(string id) {
            return View(userRepository.Find(id));
        }

        //
        // GET: /Users/Delete/5
        [AuthorizeByRole(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Delete/5
        [AuthorizeByRole(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }

        //
        // GET: /Users/AddRole/5
        [AuthorizeByRole(Roles = "Administrator")]
        public ActionResult AddRole(string id) {
            var user = userRepository.Find(id);
            var roles = roleRepository.All;
            ViewBag.PossibleRoles = roles;
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/AddRole/5
        [AuthorizeByRole(Roles = "Administrator")]
        [HttpPost, ActionName("AddRole")]
        public ActionResult AddRole(string id, string roleName) {
            userRepository.AddRole(id, roleName);
            userRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

