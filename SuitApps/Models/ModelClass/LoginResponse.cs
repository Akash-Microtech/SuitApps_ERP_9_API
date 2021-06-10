using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class LoginResponse
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserRoleId { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string DS_BillNo { get; set; }
        public string DS_BillNoSeries { get; set; }
        public string VAN { get; set; }
        public string MAC { get; set; }
        public string Message { get; set; }
        public string DS_B2CBillNo { get; set; }
        public string DS_B2CBillNoSeries { get; set; }
        public string DS_B2BBillNo { get; set; }
        public string DS_B2BBillNoSeries { get; set; }
        public byte[] Image { get; set; }
    }
}