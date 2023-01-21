using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IUser
    {
         List<UserDetailsEntity> GetAllUsers(string ConnectionString);
         List<UserDetailsEntity> GetActiveUsers(string ConnectionString);
         List<UserDetailsEntity> GetInActiveUsers(string ConnectionString);
         string SaveUser(UserDetailsEntity model, string ConnectionString);
         List<UserDetailsEntity> ActivateUser(int UserID, string ConnectionString, out string Returncode);
         string DeleteUser(int id, string ConnectionString);
        string MappingUserandRole(MappingRoleAndUserEntity model, string ConnectionString);
    }
}
