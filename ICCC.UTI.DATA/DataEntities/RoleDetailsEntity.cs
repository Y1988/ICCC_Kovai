using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataEntities
{
    public class RoleDetailsEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int IsActive { get; set; }
      
    }
}
