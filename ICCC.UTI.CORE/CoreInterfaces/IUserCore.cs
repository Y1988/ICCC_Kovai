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
        List<UserDetailsCoreEntity> GetActiveUsers(string ConnectionString);
        List<UserDetailsCoreEntity> GetInActiveUsers(string ConnectionString);

        string SaveUser(UserDetailsCoreEntity model, string ConnectionString);
        List<UserDetailsCoreEntity> ActivateUser(int UserID, string ConnectionString,out string Returncode);
        string DeleteUser(int id, string ConnectionString);

        string MappingUserandRole(MappingRoleAndUserCoreEntity model, string ConnectionString);
    }
}
