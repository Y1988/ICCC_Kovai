using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IReportingCore
    {
         List<ReportingDetailsCoreEntity> GetAllReportings(string ConnectionString);
         string SaveReporting(ReportingDetailsCoreEntity model, string ConnectionString);
         string DeleteReporting(int id, string ConnectionString);
    }
}
