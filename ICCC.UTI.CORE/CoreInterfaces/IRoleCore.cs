using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IRoleCore
    {
         List<RoleDetailsCoreEntity> GetAllRoles(string ConnectionString);
         string SaveRole(RoleDetailsCoreEntity model, string ConnectionString);
         string DeleteRole(int id, string ConnectionString);
    }
}
