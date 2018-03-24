using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheGreatGroupModules.Models
{
    public class Staffs
    {
      public int    StaffID { get; set; }
             public int  StaffRoleID { get; set; }
             public string  StaffCode { get; set; }
              public string StaffPassword { get; set; }
              public string StaffTitleName { get; set; }
            public string   StaffFirstName { get; set; }
            public string   StaffLastName { get; set; }
           public string    StaffAddress1 { get; set; }
          public string     StaffAddress2 { get; set; }
             public int  StaffSubDistrictId { get; set; }
            public int   StaffDistrictId { get; set; }
            public int   StaffProvinceId { get; set; }
           public int    StaffZipCodeId { get; set; }
           public string    StaffTelephone { get; set; }
            public string   StaffEmail { get; set; }
          public int     Activated { get; set; }
         public int Deleted { get; set; }
    }

    public class StaffRole
    {

        public int StaffRoleID { get; set; }
        public string StaffRoleName { get; set; }
        public int Activated { get; set; }
         public int Deleted { get; set; }
    }


    public class StaffLogin
    {
        public int StaffID { get; set; }
        public int StaffRoleID { get; set; }
        public string StaffName { get; set; }
        public string StaffCode { get; set; }
        public string StaffPassword { get; set; }
        public string ImageUrl { get; set; }
        public int Activated { get; set; }
        public int Deleted { get; set; }
    }
}