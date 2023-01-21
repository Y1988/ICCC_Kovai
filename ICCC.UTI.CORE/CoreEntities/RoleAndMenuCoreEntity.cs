using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreEntities
{
    public class RoleAndMenuCoreEntity
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int MenuParentID { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public int RoleAndMenuId { get; set; }
        public string RoleName { get; set; }
      
    }
    public class MenuItems
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int MenuParentID { get; set; }
        public int IsActive { get; set; }
        public int RoleId { get; set; }
        public int RoleAndMenuId { get; set; }
        public string RoleName { get; set; }
        public IList<MenuItems> SubScreens { get; set; }
    }
}

    
