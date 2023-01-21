using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreInterfaces;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Translators;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Repositories
{
    public class RoleAndMenuDbClient : IRoleAndMenu
    {

        public List<RoleAndMenuEntity> GetRoleAndMenuDetails(int Id, string ConnectionString)
        {
            SqlParameter[] param = {
                new SqlParameter("@RoleId",Id),
            };
            return SqlHelper.ExtecuteProcedureReturnData<List<RoleAndMenuEntity>>(ConnectionString,
                "Sp_GetRoleAndMenuDetails", r => r.TranslateAsRoleAndMenuList(), param);
        }

        //public string SaveRoleAndMenu(RoleAndMenuEntity model, string ConnectionString)
        //{
        //    var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    SqlParameter[] param = {
        //        new SqlParameter("@RoleAndMenuId",model.RoleAndMenuId),
        //        new SqlParameter("@MenuId",model.MenuId),
        //        new SqlParameter("@RoleId",model.RoleId),
        //        new SqlParameter("@IsActive",model.IsActive),
        //        outParam
        //    };
        //    SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveRoleAndMenu", param);
        //    return (string)outParam.Value;
        //}

        //public string DeleteRoleAndMenu(int id, string ConnectionString)
        //{
        //    var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    SqlParameter[] param = {
        //        new SqlParameter("@RoleAndMenuId",id),
        //        outParam
        //    };
        //    SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteRoleAndMenu", param);
        //    return (string)outParam.Value;
        //}
    }


}

