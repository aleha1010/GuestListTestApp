using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuestListTestApp.Models;
using GuestListTestApp.Providers;

namespace GuestListTestApp.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly GuestManager _guestManager;

        public AdminController()
        {
            _guestManager = new GuestManager();
        }

        // GET: Admin
        public ActionResult GuestList()
        {
            return View();
        }

        public PartialViewResult GetGuests(string filter, FilterMode filterMode)
        {
            return PartialView("_GuestList", _guestManager.GuestByFilter(filterMode, filter));
        }
        
        [HttpPost]
        public JsonResult GuestComing(string id)
        {
            _guestManager.MarkGuestAsComing(id);
            return Json(new {success = true});
        }

        [HttpPost]
        public JsonResult GuestNotComing(string id)
        {
            _guestManager.MarkGuestAsNotComing(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult FinishEvent()
        {
            _guestManager.FinishEvent();
            return Json(new { success = true });
        }
    }
}