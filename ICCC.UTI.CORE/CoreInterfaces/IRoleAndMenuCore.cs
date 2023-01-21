using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IRoleAndMenuCore
    {
         List<MenuItems> GetRoleAndMenuDetails(int Id, string ConnectionString);
        //string SaveRoleAndMenu(RoleAndMenuCoreEntity model, string ConnectionString);
        //string DeleteRoleAndMenu(int id, string ConnectionString);

    }
}
