using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authentication.SignIn.Models
{
    public class StaticUsersStore
    {
        public static List<User> Users = new List<User>
        {
            new User { Username = "admin", Password = "admin123" },
            new User { Username = "ashish", Password = "pass123" },
            new User { Username = "testuser", Password = "test123" }
        };
    }
}
