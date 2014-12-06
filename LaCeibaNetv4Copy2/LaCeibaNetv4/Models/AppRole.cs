using Microsoft.AspNet.Identity.EntityFramework;

namespace LaCeibaNetv4.Models
{
    public class AppRole : IdentityRole
    {

        public AppRole() : base() { }

        public AppRole(string name) : base(name) { }
    }
}
