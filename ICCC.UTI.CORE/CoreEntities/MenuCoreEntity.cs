using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreEntities
{
    public class MenuCoreEntity
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuPath { get; set; }
        public int MenuParentID { get; set; }
        public int IsActive { get; set; }
    }
}
