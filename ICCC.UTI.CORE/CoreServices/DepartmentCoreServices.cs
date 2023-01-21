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
    public class DepartmentCoreServices : IDepartmentCore
    {
        private IAppSettings _appSettings;


        public List<DepartmentDetailsCoreEntity> GetAllDepartments(string ConnectionString)
        {
            var Data = TranslateCoreDepartmentsList(DbClientFactory<DepartmentDbClient>.Instance.GetAllDepartments(ConnectionString));
            return Data;

        }

        public string SaveDepartment(DepartmentDetailsCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<DepartmentDbClient>.Instance.SaveDepartment(TranslateDepartmentsList(model), ConnectionString);
            return data;

        }

        public string DeleteDepartment(int id, string ConnectionString)
        {
            var data = DbClientFactory<DepartmentDbClient>.Instance.DeleteDepartment(id, ConnectionString);
            return data;

        }

        public static List<DepartmentDetailsCoreEntity> TranslateCoreDepartmentsList(List<DepartmentDetailsEntity> DepartmentDetails)
        {
            List<DepartmentDetailsCoreEntity> List = new List<DepartmentDetailsCoreEntity>();
            foreach (DepartmentDetailsEntity Department in DepartmentDetails)
            {
                List.Add(new DepartmentDetailsCoreEntity
                {
                    DepartmentID = Department.DepartmentID,
                    DepartmentName = Department.DepartmentName,
                    IsActive = Department.IsActive,

                });
            }
            return List;



        }
        public static DepartmentDetailsEntity TranslateDepartmentsList(DepartmentDetailsCoreEntity DepartmentDetails)
        {
            DepartmentDetailsEntity entity = new DepartmentDetailsEntity();

            entity.DepartmentID = DepartmentDetails.DepartmentID;
            entity.DepartmentName = DepartmentDetails.DepartmentName;
            entity.IsActive = DepartmentDetails.IsActive;

            return entity;

        }




    }
}

