using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.Coreinterface;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ICCC.UTI.CORE.CoreServices
{
    public class AuthenticationCoreServices : IAuthenticationCore
    {
        private IAuthentication _authentication;
        public AuthenticationCoreServices(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        public async Task<UserDetailsCoreEntity> AuthenticateCoreAsync(string Username, string Password)
        {
            try
            {
                UserDetailsEntity request = new UserDetailsEntity();
                request.UserName = Username;
                request.Password = Password;
                var Response = MapToValue(await _authentication.AuthenticateAsync(request));
                return Response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private UserDetailsCoreEntity MapToValue(UserDetailsEntity entity)
        {
            UserDetailsCoreEntity CoreEntity = new UserDetailsCoreEntity();
            //If User not Found
            if (entity == null)
                return CoreEntity = null;

            //If User is Found
            var tokenhandler = new JwtSecurityTokenHandler();
            var Key = Encoding.ASCII.GetBytes(entity.TokenKey);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, entity.UserId.ToString()),
                    new Claim(ClaimTypes.Version, "V3.1"),
                    new Claim(ClaimTypes.MobilePhone, entity.MobileNo.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),


            };
            var token=tokenhandler.CreateToken(tokendescriptor);
            CoreEntity.TokenKey = tokenhandler.WriteToken(token);
            CoreEntity.UserId = entity.UserId;
            CoreEntity.FirstName = entity.FirstName;
            CoreEntity.LastName = entity.LastName;
            CoreEntity.UserName = entity.UserName;
            CoreEntity.Password = null;
            CoreEntity.EMAIL = entity.EMAIL;
            CoreEntity.MobileNo = entity.MobileNo;
            CoreEntity.KYCID = entity.KYCID; ;
            CoreEntity.DepartMentID = entity.DepartMentID;
            CoreEntity.IGovt = entity.IGovt;
            CoreEntity.IsVendor = entity.IsVendor;
            CoreEntity.AccessPrivillage = entity.AccessPrivillage;
            CoreEntity.Zone = entity.Zone;
            CoreEntity.Ward = entity.Ward;
            CoreEntity.State = entity.State;
            CoreEntity.District = entity.District;
            CoreEntity.PinCode = entity.PinCode;
            CoreEntity.IsActive = entity.IsActive;
            CoreEntity.RoleId= entity.RoleId;
            CoreEntity.RoleName= entity.RoleName;
            return CoreEntity;

        }
    }
}
