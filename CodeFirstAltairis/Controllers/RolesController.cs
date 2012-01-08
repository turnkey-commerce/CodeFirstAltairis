using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeFirstAltairis.Models;

namespace CodeFirstAltairis.Controllers
{   
    public class RolesController : Controller
    {
		private readonly IRoleRepository roleRepository;

        public RolesController(IRoleRepository roleRepository)
        {
			this.roleRepository = roleRepository;
        }

        //
        // GET: /Roles/
        [Authorize]
        public ViewResult Index()
        {
            return View(roleRepository.AllIncluding(role => role.Users));
        }

        //
        // GET: /Roles/Create
        [AuthorizeByRole(Roles="Administrator")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Roles/Create
        [AuthorizeByRole(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid) {
                roleRepository.InsertOrUpdate(role);
                roleRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        

        //
        // GET: /Roles/Delete/5
        [AuthorizeByRole(Roles = "Administrator")]
        public ActionResult Delete(string id)
        {
            return View(roleRepository.Find(id));
        }

        //
        // POST: /Roles/Delete/5
        [AuthorizeByRole(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            roleRepository.Delete(id);
            roleRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

