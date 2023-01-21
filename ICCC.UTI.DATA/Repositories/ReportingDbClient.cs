using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Translators;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Repositories
{
    public class ReportingDbClient : IReporting
    {
       
        public List<ReportingDetailsEntity> GetAllReportings(string ConnectionString)
        {

            return SqlHelper.ExtecuteProcedureReturnData<List<ReportingDetailsEntity>>(ConnectionString,
                "GetReportingDetails", r => r.TranslateAsReportingsList());
        }

        public string SaveReporting(ReportingDetailsEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@ReportingId",model.ReportingID),
                new SqlParameter("@ReportingName",model.ReportingName),
                new SqlParameter("@IsActive",model.IsActive),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveReporting", param);
            return (string)outParam.Value;
        }

        public string DeleteReporting(int id, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@ReportingId",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteReporting", param);
            return (string)outParam.Value;
        }
    }
}
