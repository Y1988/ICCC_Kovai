using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IUser
    {
        public List<UserDetailsEntity> GetAllUsers(string ConnectionString);
        public string SaveUser(UserDetailsEntity model, string ConnectionString);
        public string DeleteUser(int id, string ConnectionString);
        string MappingUserandRole(MappingRoleAndUserEntity model, string ConnectionString);
    }
}
