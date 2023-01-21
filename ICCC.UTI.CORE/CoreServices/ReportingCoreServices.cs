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
    public class ReportingCoreServices : IReportingCore
    {
        private IAppSettings _appSettings;


        public List<ReportingDetailsCoreEntity> GetAllReportings(string ConnectionString)
        {
            var Data = TranslateCoreReportingsList(DbClientFactory<ReportingDbClient>.Instance.GetAllReportings(ConnectionString));
            return Data;

        }

        public string SaveReporting(ReportingDetailsCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<ReportingDbClient>.Instance.SaveReporting(TranslateReportingsList(model), ConnectionString);
            return data;

        }

        public string DeleteReporting(int id, string ConnectionString)
        {
            var data = DbClientFactory<ReportingDbClient>.Instance.DeleteReporting(id, ConnectionString);
            return data;

        }

        public static List<ReportingDetailsCoreEntity> TranslateCoreReportingsList(List<ReportingDetailsEntity> ReportingDetails)
        {
            List<ReportingDetailsCoreEntity> List = new List<ReportingDetailsCoreEntity>();
            foreach (ReportingDetailsEntity Reporting in ReportingDetails)
            {
                List.Add(new ReportingDetailsCoreEntity
                {
                    ReportingID = Reporting.ReportingID,
                    ReportingName = Reporting.ReportingName,
                    IsActive = Reporting.IsActive,

                });
            }
            return List;



        }
        public static ReportingDetailsEntity TranslateReportingsList(ReportingDetailsCoreEntity ReportingDetails)
        {
            ReportingDetailsEntity entity = new ReportingDetailsEntity();

            entity.ReportingID = ReportingDetails.ReportingID;
            entity.ReportingName = ReportingDetails.ReportingName;
            entity.IsActive = ReportingDetails.IsActive;

            return entity;

        }




    }
}

