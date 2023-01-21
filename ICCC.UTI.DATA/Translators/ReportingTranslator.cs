using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Translators
{
    public static class ReportingTranslator
    {
        public static ReportingDetailsEntity TranslateAsReporting(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new ReportingDetailsEntity();
            if (reader.IsColumnExists("ReportingId"))
                item.ReportingID = SqlHelper.GetNullableInt32(reader, "ReportingId");

            
            if (reader.IsColumnExists("ReportingName"))
                item.ReportingName = SqlHelper.GetNullableString(reader, "ReportingName");


            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");


            return item;
        }

        public static List<ReportingDetailsEntity> TranslateAsReportingsList(this SqlDataReader reader)
        {
            var list = new List<ReportingDetailsEntity>();
            while (reader.Read())
            {
                list.Add(TranslateAsReporting(reader, true));
            }
            return list;
        }
    }
}
