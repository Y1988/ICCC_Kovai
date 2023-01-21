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
    public class MenuDbClient : IMenu
    {

        public List<MenuEntity> GetMenuDetails(string ConnectionString)
        {
            //SqlParameter[] param = {
            //    new SqlParameter("@RoleId",Id),
            //};
            return SqlHelper.ExtecuteProcedureReturnData<List<MenuEntity>>(ConnectionString,
                "Sp_GetMenuDetails", r => r.TranslateAsMenuList());
        }

        public string SaveMenu(MenuEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@MenuId",model.MenuId),
                new SqlParameter("@MenuName",model.MenuName),
                new SqlParameter("@MenuParentId",model.MenuParentID),
                new SqlParameter("@MenuPath",model.MenuPath),
                new SqlParameter("@IsActive",model.IsActive),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveMenu", param);
            return (string)outParam.Value;
        }

        public string DeleteMenu(int id, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@MenuId",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteMenu", param);
            return (string)outParam.Value;
        }
    }


}

