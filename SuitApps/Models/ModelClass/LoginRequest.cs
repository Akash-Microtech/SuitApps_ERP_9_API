using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class LoginRequest
    {
        public string CompanyId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        /*public string MacId { get; set; }*/

        internal static void Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }
    }
}