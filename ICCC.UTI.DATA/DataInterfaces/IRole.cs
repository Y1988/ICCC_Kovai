using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IRole
    {
        public List<RoleDetailsEntity> GetAllRoles(string ConnectionString);
        public string SaveRole(RoleDetailsEntity model, string ConnectionString);
        public string DeleteRole(int id, string ConnectionString);
    }
}
