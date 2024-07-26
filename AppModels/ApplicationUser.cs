using Microsoft.AspNetCore.Identity;

namespace RoadTrafficManagement.AppModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        //public ICollection<IdentityUserClaim<string>> Claims { get; set; }
        //public ICollection<IdentityUserLogin<string>> Logins { get; set; }
        //public ICollection<IdentityUserToken<string>> Tokens { get; set; }
        //public ICollection<IdentityUserRole<string>> UserRoles { get; set; }
    }

}
