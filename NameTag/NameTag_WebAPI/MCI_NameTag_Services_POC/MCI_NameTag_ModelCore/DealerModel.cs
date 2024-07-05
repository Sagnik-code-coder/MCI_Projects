using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_ModelCore
{
    public class DealerModel
    {
        [JsonProperty]
        [Key]
        public string DealerCode { get; set; }
        [JsonProperty]
        public string Region { get; set; }
        [JsonProperty]
        public string District { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public string Address { get; set; }
        [JsonProperty]
        public string DealerCity { get; set; }
        [JsonProperty]
        public string DealerProvince { get; set; }
        [JsonProperty]
        public string DealerPostalCode { get; set; }
    }
}
