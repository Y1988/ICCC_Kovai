using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IUserCore
    {
         List<UserDetailsCoreEntity> GetAllUsers(string ConnectionString);
         string SaveUser(UserDetailsCoreEntity model, string ConnectionString);
         string DeleteUser(int id, string ConnectionString);

        string MappingUserandRole(MappingRoleAndUserCoreEntity model, string ConnectionString);
    }
}
