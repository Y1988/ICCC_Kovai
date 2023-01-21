using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Translators;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Repositories
{
    public class RoleDbClient : IRole
    {
       
        public List<RoleDetailsEntity> GetAllRoles(string ConnectionString)
        {

            return SqlHelper.ExtecuteProcedureReturnData<List<RoleDetailsEntity>>(ConnectionString,
                "GetRoles", r => r.TranslateAsRolesList());
        }

        public string SaveRole(RoleDetailsEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@RoleId",model.RoleID),
                new SqlParameter("@RoleName",model.RoleName),
                new SqlParameter("@RoleDesc",model.RoleDescription),
                new SqlParameter("@IsActive",model.IsActive),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveRoles", param);
            return (string)outParam.Value;
        }

        public string DeleteRole(int id, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@RoleId",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteRoles", param);
            return (string)outParam.Value;
        }
    }
}
