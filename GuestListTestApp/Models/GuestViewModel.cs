using System.ComponentModel.DataAnnotations;
using GuestListTestApp.Attributes;


namespace GuestListTestApp.Models
{
    public class GuestViewModel
    {
        public string Id { get; set; }

        [Required]
        [LocalizedDisplayName("GuestRegistrationViewModel_FIO", NameResourceType = typeof (Resources.Resources))]
        public string FIO { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [LocalizedDisplayName("GuestRegistrationViewModel_Phone", NameResourceType = typeof (Resources.Resources))]
        public string Phone { get; set; }

        public GuestStatus Status { get; set; }

        [LocalizedDisplayName("AgreeWithMessage", NameResourceType = typeof(Resources.Resources))]
        public bool IsAggree { get; set; }
    }


    public enum GuestStatus
    {
        Wait = 1,
        Coming = 2,
        NotComing = 3
    }
    public enum FilterMode
    {
        AllGuests = 1,
        ComingGuests = 2,
        NotComingGuests = 3
    }
}