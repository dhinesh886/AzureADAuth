using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureAD_Auth.Controllers
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string client_app_key { get; set; }
        public string client_secret { get; set; }
    }
}
