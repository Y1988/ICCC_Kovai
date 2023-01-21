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
    public class RoleCoreServices : IRoleCore
    {
        private IAppSettings _appSettings;


        public List<RoleDetailsCoreEntity> GetAllRoles(string ConnectionString)
        {
            var Data = TranslateCoreRolesList(DbClientFactory<RoleDbClient>.Instance.GetAllRoles(ConnectionString));
            return Data;

        }

        public string SaveRole(RoleDetailsCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<RoleDbClient>.Instance.SaveRole(TranslateRolesList(model), ConnectionString);
            return data;

        }

        public string DeleteRole(int id, string ConnectionString)
        {
            var data = DbClientFactory<RoleDbClient>.Instance.DeleteRole(id, ConnectionString);
            return data;

        }

        public static List<RoleDetailsCoreEntity> TranslateCoreRolesList(List<RoleDetailsEntity> RoleDetails)
        {
            List<RoleDetailsCoreEntity> List = new List<RoleDetailsCoreEntity>();
            foreach (RoleDetailsEntity Role in RoleDetails)
            {
                List.Add(new RoleDetailsCoreEntity
                {
                    RoleID = Role.RoleID,
                    RoleName = Role.RoleName,
                    IsActive = Role.IsActive,
                    RoleDescription = Role.RoleDescription

                });
            }
            return List;



        }
        public static RoleDetailsEntity TranslateRolesList(RoleDetailsCoreEntity RoleDetails)
        {
            RoleDetailsEntity entity = new RoleDetailsEntity();

            entity.RoleID = RoleDetails.RoleID;
            entity.RoleName = RoleDetails.RoleName;
            entity.IsActive = RoleDetails.IsActive;
            entity.RoleDescription = RoleDetails.RoleDescription;

            return entity;

        }




    }
}

