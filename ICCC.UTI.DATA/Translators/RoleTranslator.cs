using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Translators
{
    public static class RoleTranslator
    {
        public static RoleDetailsEntity TranslateAsRole(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new RoleDetailsEntity();
            if (reader.IsColumnExists("RoleId"))
                item.RoleID = SqlHelper.GetNullableInt32(reader, "RoleId");

            
            if (reader.IsColumnExists("RoleName"))
                item.RoleName = SqlHelper.GetNullableString(reader, "RoleName");

            if (reader.IsColumnExists("RoleDescription"))
                item.RoleDescription = SqlHelper.GetNullableString(reader, "RoleDescription");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");


            return item;
        }

        public static List<RoleDetailsEntity> TranslateAsRolesList(this SqlDataReader reader)
        {
            var list = new List<RoleDetailsEntity>();
            while (reader.Read())
            {
                list.Add(TranslateAsRole(reader, true));
            }
            return list;
        }
    }
}
