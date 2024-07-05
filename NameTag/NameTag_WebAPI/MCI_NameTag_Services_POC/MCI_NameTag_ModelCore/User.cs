using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MCI_NameTag_ModelCore
{
    public class User : BaseEntity
    {
        [JsonProperty]
        public int UserID { get; set; }
        [JsonProperty]
        public int DealarCode { get; set; }
        [JsonProperty]
        public string DealerName { get; set; }
        [JsonProperty]
        public string UserType { get; set; }
        [JsonProperty]
        public string UserName { get; set; }
        [JsonProperty]
        public string UserRole { get; set; }
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string LastName { get; set; }
        [JsonProperty]
        public string UserEmail { get; set; }
        [JsonProperty]
        public bool IsDealer { get; set; }
        public string SiteUrl { get; set; }
        //[JsonProperty]
        //public string EmployeeName { get; set; }
        //[JsonProperty]
        //public string JobTitle { get; set; }
        //[JsonProperty]
        //public string AccountName { get; set; }
        //[JsonProperty]
        //public string DepartmentName { get; set; }
    }
}
