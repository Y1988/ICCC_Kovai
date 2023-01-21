using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Translators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace ICCC.UTI.DATA.Repositories
{
    public class UserDbClient : IUser
    {

        public List<UserDetailsEntity> GetAllUsers(string ConnectionString)
        {

            return SqlHelper.ExtecuteProcedureReturnData<List<UserDetailsEntity>>(ConnectionString,
                "GetUsers", r => r.TranslateAsUsersList());
        }

        public string SaveUser(UserDetailsEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@FirstName",model.FirstName),
                new SqlParameter("@LastName",model.LastName),
               // new SqlParameter("@UserName",model.UserId==0?"ICCC_"+GenerateRandomNumber():model.UserName),
                new SqlParameter("@Password",model.Password==null?"1234":model.Password),
                new SqlParameter("@EMAIL",model.EMAIL),
                new SqlParameter("@MobileNo",model.MobileNo),
                new SqlParameter("@KYCID",model.KYCID==null?"":model.KYCID),
                new SqlParameter("@KYCName",model.KYCName==null?"":model.KYCName),
                new SqlParameter("@DepartMentID",model.DepartMentID),
                new SqlParameter("@IGovt",model.IGovt),
                new SqlParameter("@IsVendor",model.IsVendor),
                new SqlParameter("@AccessPrivillage",model.AccessPrivillage),
                new SqlParameter("@Zone",model.Zone),
                new SqlParameter("@Ward",model.Ward),
                new SqlParameter("@State",model.State),
                new SqlParameter("@District",model.District),
                new SqlParameter("@PinCode",model.PinCode),
                new SqlParameter("@IsActive",model.IsActive),
                new SqlParameter("@RoleId",model.RoleId),
                new SqlParameter("@CreatedBy",model.CreatedBy),
                new SqlParameter("@ReportingTo",model.ReportingTo),

                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveUser", param);
            return (string)outParam.Value;
        }

        public string DeleteUser(int id, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@UserId",id),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "DeleteUser", param);
            return (string)outParam.Value;
        }

        public string MappingUserandRole(MappingRoleAndUserEntity model, string ConnectionString)
        {
            var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
            {
                Direction = ParameterDirection.Output
            };
            SqlParameter[] param = {
                new SqlParameter("@RoleAndUserId",model.RoleAndUserId),
                new SqlParameter("@UserId",model.UserId),
                new SqlParameter("@RoleId",model.RoleId),
                outParam
            };
            SqlHelper.ExecuteProcedureReturnString(ConnectionString, "SaveRoleAndUser", param);
            return (string)outParam.Value;
        }

        protected string GenerateRandomNumber()
        {
            string number = "";
            Random random = new Random();
            int n = random.Next(0, 100000);
            number += n.ToString("D5");
            return number;
        }
    }
}
