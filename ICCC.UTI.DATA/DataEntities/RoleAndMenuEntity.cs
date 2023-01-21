using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreEntities
{
    public class RoleAndMenuEntity
    {

        public int RoleAndMenuId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int MenuParentID { get; set; }
        public int IsActive { get; set; }
        // public IList<ParentMenu> ParentMenu { get; set; }
    }
    //public class ParentMenu
    //{
    //    public int MenuId { get; set; }
    //    public string MenuName { get; set; }
    //    public string MenuPath { get; set; }
    //    public int MenuParentID { get; set; }
    //    public int IsActive { get; set; }
    //    public int RoleId { get; set; }
    //    public IList<SubMenu> SubMenu { get; set; }
    //}
    //public class SubMenu
    //{
    //    public int MenuId { get; set; }
    //    public string MenuName { get; set; }
    //    public string MenuPath { get; set; }
    //    public int MenuParentID { get; set; }
    //    public int IsActive { get; set; }
    //}

}
   
       

