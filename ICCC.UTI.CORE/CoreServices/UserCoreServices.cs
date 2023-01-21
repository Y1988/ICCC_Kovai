using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreInterfaces;
using ICCC.UTI.DATA.Repositories;
using System.Linq;
using ICCC.UTI.CORE.CoreEntities;
using System.Reflection;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataServices;

namespace ICCC.UTI.CORE.CoreServices
{
    public class UserCoreServices : IUserCore
    {
        private IAppSettings _appSettings;


        public List<UserDetailsCoreEntity> GetAllUsers(string ConnectionString)
        {
            var Data = TranslateCoreUsersList(DbClientFactory<UserDbClient>.Instance.GetAllUsers(ConnectionString));
            return Data;

        }

        public string MappingUserandRole(MappingRoleAndUserCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<UserDbClient>.Instance.MappingUserandRole(TranslateUsersandRoleList(model), ConnectionString);
            return data;

        }

        public string DeleteUser(int id, string ConnectionString)
        {
            var data = DbClientFactory<UserDbClient>.Instance.DeleteUser(id, ConnectionString);
            return data;

        }

        public string SaveUser(UserDetailsCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<UserDbClient>.Instance.SaveUser(TranslateUsersList(model), ConnectionString);
            return data;

        }

        public static List<UserDetailsCoreEntity> TranslateCoreUsersList(List<UserDetailsEntity> userDetails)
        {
            List<UserDetailsCoreEntity> List = new List<UserDetailsCoreEntity>();
            foreach (UserDetailsEntity user in userDetails)
            {
                List.Add(new UserDetailsCoreEntity
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    EMAIL = user.EMAIL,
                    MobileNo = user.MobileNo,
                    KYCID = user.KYCID,
                    DepartMentID = user.DepartMentID,
                    IGovt = user.IGovt,
                    IsVendor = user.IsVendor,
                    AccessPrivillage = user.AccessPrivillage,
                    Zone = user.Zone,
                    Ward = user.Ward,
                    State = user.State,
                    District = user.District,
                    PinCode = user.PinCode,
                    IsActive = user.IsActive,
                    RoleId= user.RoleId,
                    RoleName= user.RoleName,
                    CreatedBy= user.CreatedBy,
                    KYCName= user.KYCName,
                    ReportingTo= user.ReportingTo,

                });
            }
            return List;



        }

        public static List<MappingRoleAndUserCoreEntity> TranslateCoreUsersandRoleList(List<MappingRoleAndUserEntity> userDetails)
        {
            List<MappingRoleAndUserCoreEntity> List = new List<MappingRoleAndUserCoreEntity>();
            foreach (MappingRoleAndUserEntity user in userDetails)
            {
                List.Add(new MappingRoleAndUserCoreEntity
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    RoleAndUserId= user.RoleAndUserId,

                });
            }
            return List;



        }
        public static UserDetailsEntity TranslateUsersList(UserDetailsCoreEntity userDetails)
        {
            UserDetailsEntity entity = new UserDetailsEntity();

            entity.UserId = userDetails.UserId;
            entity.UserName = userDetails.UserName;
            entity.FirstName = userDetails.FirstName;
            entity.LastName = userDetails.LastName;
            entity.Password = userDetails.Password;
            entity.EMAIL = userDetails.EMAIL;
            entity.MobileNo = userDetails.MobileNo;
            entity.KYCID = userDetails.KYCID;
            entity.DepartMentID = userDetails.DepartMentID;
            entity.IGovt = userDetails.IGovt;
            entity.IsVendor = userDetails.IsVendor;
            entity.AccessPrivillage = userDetails.AccessPrivillage;
            entity.Zone = userDetails.Zone;
            entity.Ward = userDetails.Ward;
            entity.State = userDetails.State;
            entity.District = userDetails.District;
            entity.PinCode = userDetails.PinCode;
            entity.IsActive = userDetails.IsActive;
            entity.RoleId= userDetails.RoleId;
            entity.KYCName= userDetails.KYCName;
            entity.CreatedBy= userDetails.CreatedBy;
            entity.ReportingTo= userDetails.ReportingTo;

            return entity;

        }

        public static MappingRoleAndUserEntity TranslateUsersandRoleList(MappingRoleAndUserCoreEntity userDetails)
        {
            MappingRoleAndUserEntity entity = new MappingRoleAndUserEntity();

            entity.UserId = userDetails.UserId;
            entity.RoleId = userDetails.RoleId;
            entity.RoleAndUserId = userDetails.RoleAndUserId;

            return entity;

        }




    }
}

