using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IRoleAndMenu
    {
        List<RoleAndMenuEntity> GetRoleAndMenuDetails(int id, string ConnectionString);
        //string SaveRoleAndMenu(RoleAndMenuEntity model, string ConnectionString);
        //string DeleteRoleAndMenu(int id, string ConnectionString);
    }
}
