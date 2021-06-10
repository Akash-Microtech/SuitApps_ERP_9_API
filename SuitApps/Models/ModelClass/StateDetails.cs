using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuitApps.Models.ModelClass
{
    public class StateDetails
    {
        public string StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string StateType { get; set; }
        public string StateParent { get; set; }
        public string level { get; set; }
        public string Scategory { get; set; }
    }
}