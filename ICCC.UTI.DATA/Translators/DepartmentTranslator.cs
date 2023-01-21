using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Translators
{
    public static class DepartmentTranslator
    {
        public static DepartmentDetailsEntity TranslateAsDepartment(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new DepartmentDetailsEntity();
            if (reader.IsColumnExists("DepartmentId"))
                item.DepartmentID = SqlHelper.GetNullableInt32(reader, "DepartmentId");

            
            if (reader.IsColumnExists("DepartmentName"))
                item.DepartmentName = SqlHelper.GetNullableString(reader, "DepartmentName");


            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");


            return item;
        }

        public static List<DepartmentDetailsEntity> TranslateAsDepartmentsList(this SqlDataReader reader)
        {
            var list = new List<DepartmentDetailsEntity>();
            while (reader.Read())
            {
                list.Add(TranslateAsDepartment(reader, true));
            }
            return list;
        }
    }
}
