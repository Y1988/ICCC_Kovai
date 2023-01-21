using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Translators;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Repositories
{
    public class DepartmentDbClient : IDepartment
    {
       
        public List<DepartmentDetailsEntity> GetAllDepartments(string ConnectionString)
        {

            return SqlHelper.ExtecuteProcedureReturnData<List<DepartmentDetailsEntity>>(ConnectionString,
                "GetDepartments", r => r.TranslateAsDepartmentsList());
        }

        public string SaveDepartment(DepartmentDetailsEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@DepartmentId",model.DepartmentID),
                new SqlParameter("@DepartmentName",model.DepartmentName),
                new SqlParameter("@IsActive",model.IsActive),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveDepartments", param);
            return (string)outParam.Value;
        }

        public string DeleteDepartment(int id, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@DepartmentId",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteDepartments", param);
            return (string)outParam.Value;
        }
    }
}
