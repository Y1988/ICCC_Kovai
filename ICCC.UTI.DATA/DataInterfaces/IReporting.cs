using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IReporting
    {
        public List<ReportingDetailsEntity> GetAllReportings(string ConnectionString);
        public string SaveReporting(ReportingDetailsEntity model, string ConnectionString);
        public string DeleteReporting(int id, string ConnectionString);
    }
}
