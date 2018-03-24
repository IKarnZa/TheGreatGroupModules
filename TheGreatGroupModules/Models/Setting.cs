using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Models
{
    public class Setting
    {
    }


    public class ListItems
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }

    public class Zone
    {
        public int ZoneID { get; set; }
        public string ZoneCode { get; set; }
        public string ZoneName{ get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }

    }
}