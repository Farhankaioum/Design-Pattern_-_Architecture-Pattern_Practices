using Microsoft.AspNetCore.Identity;
using SecureAPIUsingJWT.Entities;
using System.Collections.Generic;

namespace SecureAPIUsingJWT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
