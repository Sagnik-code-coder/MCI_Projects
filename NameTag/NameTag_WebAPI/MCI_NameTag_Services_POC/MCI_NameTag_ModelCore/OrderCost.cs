using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCI_NameTag_ModelCore
{
    public class OrderCost
    {
        [JsonProperty]
        public int OrderID { get; set; }
        [JsonProperty]
        public decimal Cost { get; set; }
    }
}
