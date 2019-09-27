using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TipezeNyumbaWebApplication.Controllers
{

    public class TipezeNyumbaUserManagementController : Controller
    {
        TipezeNyumbaClientService.TipezeNyumbaServiceClient client = new TipezeNyumbaClientService.TipezeNyumbaServiceClient("BasicHttpBinding_ITipezeNyumbaService");
        // GET: TipezeNyumbaUserManagement
        public ActionResult Index()
        {
            List<TipezeNyumbaClientService.UserDetails> getUsers = client.GetUsers().ToList();
            return View(getUsers);
        }

        // GET: TipezeNyumbaUserManagement/Details/5
        public ActionResult Details(int id)
        {
            
            return View();
        }

        // GET: TipezeNyumbaUserManagement/Create
        public ActionResult Create()
        {
            LoadUserDropdowns();
            return View();
        }

        private void LoadUserDropdowns()
        {
            List<TipezeNyumbaClientService.UserType> userRoles = client.GetUserRoles().ToList();
            List<SelectListItem> userRolesSelectListItem = new List<SelectListItem>();
            foreach (var eachUserRole in userRoles)
            {
                userRolesSelectListItem.Add(new SelectListItem { Value = eachUserRole.userTypeID.ToString(), Text = eachUserRole.userRoleForUser });
            }
            ViewBag.userRoleForUser = userRolesSelectListItem;
            List<TipezeNyumbaClientService.UserSubscriptionType> userSubscriptionTypes = client.GetUserSubscriptionTypes().ToList();
            List<SelectListItem> userSubscriptionTypesSelectListItem = new List<SelectListItem>();
            foreach (var eachSubscriptionType in userSubscriptionTypes)
            {
                userSubscriptionTypesSelectListItem.Add(new SelectListItem { Value = eachSubscriptionType.subscriptionTypeID.ToString(), Text = eachSubscriptionType.subscription });
            }
            ViewBag.userSubscriptionType = userSubscriptionTypesSelectListItem;
        }

        // POST: TipezeNyumbaUserManagement/Create
        [HttpPost]
        public ActionResult Create(TipezeNyumbaClientService.UserDetailsToAddOrUpdate newUser)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    bool ConfirmUserIsCreated = client.AddANewUser(newUser);
                    if(ConfirmUserIsCreated == true)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        LoadUserDropdowns();
                        return View(newUser);
                    }
                }
                catch
                {
                    LoadUserDropdowns();
                    return View(newUser);
                }
            }
            else
            {
                LoadUserDropdowns();
                return View(newUser);
            }
            
        }

        // GET: TipezeNyumbaUserManagement/Edit/5
        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: TipezeNyumbaUserManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipezeNyumbaUserManagement/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TipezeNyumbaUserManagement/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
