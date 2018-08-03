using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GuestListTestApp.Models;

namespace GuestListTestApp.Providers
{
    public class GuestManager
    {
        public List<GuestViewModel> GuestByFilter(FilterMode filterMode, string filter)
        {
            List<GuestViewModel> guestViewModels;

            using (var context = new ApplicationDbContext())
            {
                guestViewModels = GetAllGuests(context).Where(u=>
                                  (filterMode == FilterMode.AllGuests
                                  ? u.Status == GuestStatus.Coming || u.Status == GuestStatus.NotComing || u.Status == GuestStatus.Wait : 
                                  filterMode == FilterMode.ComingGuests ? u.Status == GuestStatus.Coming : u.Status == GuestStatus.NotComing) 
                                  && (u.Name.Contains(filter) || u.Email.Contains(filter) || u.PhoneNumber.Contains(filter)))
                    .Select(
                        u =>
                            new GuestViewModel
                            {
                                Id = u.Id,
                                Email = u.Email,
                                FIO = u.Name,
                                Phone = u.PhoneNumber,
                                Status = u.Status
                            }).ToList();
            }
            return guestViewModels;
        }

        private static IQueryable<Guest> GetAllGuests(ApplicationDbContext context)
        {
            var role = context.Roles.First(r => r.Name == Roles.Guest);

            var allGuests = context.Guests.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            return allGuests;
        }

        public void MarkGuestAsComing(string id)
        {
            ChangeUserStatus(id, GuestStatus.Coming);
        }

        public void MarkGuestAsNotComing(string id)
        {
            ChangeUserStatus(id, GuestStatus.NotComing);
        }

        private static void ChangeUserStatus(string id, GuestStatus status)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Guests.First(u => u.Id == id);

                user.Status = status;
                context.SaveChanges();
            }
        }

        public void FinishEvent()
        {
            using (var context = new ApplicationDbContext())
            {
                var waitingGuests = GetAllGuests(context).Where(u=>u.Status == GuestStatus.Wait).ToList();

                foreach (var guest in waitingGuests)
                {
                    guest.Status = GuestStatus.NotComing;
                }
                
                context.SaveChanges();
            }
        }
    }
}