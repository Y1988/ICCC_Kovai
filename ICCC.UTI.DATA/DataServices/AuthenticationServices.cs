using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ICCC.UTI.DATA.DataServices
{
    public class AuthenticationServices : IAuthentication
    {
        private IAppSettings _appSettings;
        public AuthenticationServices(IAppSettings appSettings)
        {
            _appSettings = appSettings;

        }
        public async Task<UserDetailsEntity> AuthenticateAsync(UserDetailsEntity Entity)
        {
            try
            {
                DataTable Dt = new DataTable();
                using (SqlConnection sql = new SqlConnection(_appSettings.DBConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = new SqlCommand("SP_AuthenticateUser", sql);
                        da.SelectCommand.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Entity.UserName;
                        da.SelectCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = Entity.Password;
                        da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                            Dt = ds.Tables[0];
                    }
                }
                return MapToValue(Dt);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private UserDetailsEntity MapToValue(DataTable Dt)
        {
            try
            {
                UserDetailsEntity entity = new UserDetailsEntity();
                if (Dt.Rows.Count == 0)
                    return entity = null;
                entity.UserId = Convert.ToInt32(Dt.Rows[0]["UserID"]);
                entity.FirstName = Dt.Rows[0]["FirstName"].ToString();
                entity.LastName = Dt.Rows[0]["LastName"].ToString();
                entity.UserName = Dt.Rows[0]["UserName"].ToString();
                entity.Password = Dt.Rows[0]["Password"].ToString();
                entity.EMAIL = Dt.Rows[0]["EMAIL"].ToString();
                entity.MobileNo = Dt.Rows[0]["MobileNo"].ToString();
                entity.KYCID = Dt.Rows[0]["KYCID"].ToString();
                entity.DepartMentID = Convert.ToInt32(Dt.Rows[0]["DepartMentID"]);
                entity.IGovt = Convert.ToInt32(Dt.Rows[0]["IGovt"]);
                entity.IsVendor = Convert.ToInt32(Dt.Rows[0]["IsVendor"]);
                entity.AccessPrivillage = Convert.ToInt32(Dt.Rows[0]["AccessPrivillage"]);
                entity.Zone = Dt.Rows[0]["Zone"].ToString();
                entity.Ward = Dt.Rows[0]["Ward"].ToString();
                entity.State = Dt.Rows[0]["State"].ToString();
                entity.District = Dt.Rows[0]["District"].ToString();
                entity.PinCode = Dt.Rows[0]["PinCode"].ToString();
                entity.IsActive = Convert.ToInt32(Dt.Rows[0]["IsActive"]);
                entity.RoleId = Convert.ToInt32(Dt.Rows[0]["RoleId"]);
                entity.RoleName = Dt.Rows[0]["RoleName"].ToString(); ;
                entity.TokenKey = _appSettings.JwtTokenKey();
                return entity;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
