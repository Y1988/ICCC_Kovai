using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Translators
{
    public static class UserTranslator
    {
        public static UserDetailsEntity TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new UserDetailsEntity();
            if (reader.IsColumnExists("UserId"))
                item.UserId = SqlHelper.GetNullableInt32(reader, "UserId");

            if (reader.IsColumnExists("FirstName"))
                item.FirstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("LastName"))
                item.LastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("UserName"))
                item.UserName = SqlHelper.GetNullableString(reader, "UserName");

            if (reader.IsColumnExists("Password"))
                item.Password = SqlHelper.GetNullableString(reader, "Password");

            if (reader.IsColumnExists("EMAIL"))
                item.EMAIL = SqlHelper.GetNullableString(reader, "EMAIL");

            if (reader.IsColumnExists("MobileNo"))
                item.MobileNo = SqlHelper.GetNullableString(reader, "MobileNo");

            if (reader.IsColumnExists("KYCID"))
                item.KYCID = SqlHelper.GetNullableString(reader, "KYCID");

            if (reader.IsColumnExists("DepartMentID"))
                item.DepartMentID = SqlHelper.GetNullableInt32(reader, "DepartMentID");

            if (reader.IsColumnExists("IGovt"))
                item.IGovt = SqlHelper.GetNullableInt32(reader, "IGovt");

            if (reader.IsColumnExists("IsVendor"))
                item.IsVendor = SqlHelper.GetNullableInt32(reader, "IsVendor");

            if (reader.IsColumnExists("AccessPrivillage"))
                item.AccessPrivillage = SqlHelper.GetNullableInt32(reader, "AccessPrivillage");

            if (reader.IsColumnExists("Zone"))
                item.Zone = SqlHelper.GetNullableString(reader, "Zone");

            if (reader.IsColumnExists("Ward"))
                item.Ward = SqlHelper.GetNullableString(reader, "Ward");

            if (reader.IsColumnExists("State"))
                item.State = SqlHelper.GetNullableString(reader, "State");

            if (reader.IsColumnExists("District"))
                item.District = SqlHelper.GetNullableString(reader, "District");

            if (reader.IsColumnExists("PinCode"))
                item.PinCode = SqlHelper.GetNullableString(reader, "PinCode");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");
            if (reader.IsColumnExists("RoleId"))
                item.RoleId = SqlHelper.GetNullableInt32(reader, "RoleId");


            if (reader.IsColumnExists("KYCName"))
                item.KYCName = SqlHelper.GetNullableString(reader, "KYCName");

            if (reader.IsColumnExists("RoleName"))
                item.RoleName = SqlHelper.GetNullableString(reader, "RoleName");

            if (reader.IsColumnExists("CreatedBy"))
                item.CreatedBy = SqlHelper.GetNullableString(reader, "CreatedBy");

            if (reader.IsColumnExists("Reportingto"))
                item.ReportingTo = SqlHelper.GetNullableString(reader, "Reportingto");


            return item;
        }

        public static List<UserDetailsEntity> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<UserDetailsEntity>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }
        
    }
}
