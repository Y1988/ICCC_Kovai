using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ICCC.UTI.DATA.Translators
{
    public static class RoleAndMenuTranslator
    {
        public static RoleAndMenuEntity TranslateAsRoleAndMenu(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new RoleAndMenuEntity();

            if (reader.IsColumnExists("Id"))
                item.RoleAndMenuId = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("RoleId"))
                item.RoleId = SqlHelper.GetNullableInt32(reader, "RoleId");

            if (reader.IsColumnExists("RoleName"))
                item.RoleName = SqlHelper.GetNullableString(reader, "RoleName");
            if (reader.IsColumnExists("MenuID"))
                item.MenuId = SqlHelper.GetNullableInt32(reader, "MenuID");

            if (reader.IsColumnExists("MenuName"))
                item.MenuName = SqlHelper.GetNullableString(reader, "MenuName");

            if (reader.IsColumnExists("MenuPath"))
                item.MenuPath = SqlHelper.GetNullableString(reader, "MenuPath");

            if (reader.IsColumnExists("MenuParentID"))
                item.MenuParentID = SqlHelper.GetNullableInt32(reader, "MenuParentID");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");

            // item.ParentMenu = new List<ParentMenu> { TranslateAsPMenu(reader, true) };



            return item;
        }

        //public static ParentMenu TranslateAsPMenu(SqlDataReader reader, bool isList = false)
        //{
        //    if (!isList)
        //    {
        //        if (!reader.HasRows)
        //            return null;
        //        reader.Read();
        //    }
        //    var item = new ParentMenu();

        //    if (reader.IsColumnExists("RoleId"))
        //        item.RoleId = SqlHelper.GetNullableInt32(reader, "RoleId");

        //    if (reader.IsColumnExists("MenuID"))
        //        item.MenuId = SqlHelper.GetNullableInt32(reader, "MenuID");

        //    if (reader.IsColumnExists("MenuName"))
        //        item.MenuName = SqlHelper.GetNullableString(reader, "MenuName");

        //    if (reader.IsColumnExists("MenuPath"))
        //        item.MenuPath = SqlHelper.GetNullableString(reader, "MenuPath");

        //    if (reader.IsColumnExists("MenuParentID"))
        //        item.MenuParentID = SqlHelper.GetNullableInt32(reader, "MenuParentID");

        //    if (reader.IsColumnExists("IsActive"))
        //        item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");

        //    item.SubMenu = new List<SubMenu> { TranslateAsSMenu(reader, true) };

        //    return item;
        //}

        //public static SubMenu TranslateAsSMenu(SqlDataReader reader, bool isList = false)
        //{
        //    if (!isList)
        //    {
        //        if (!reader.HasRows)
        //            return null;
        //        reader.Read();
        //    }
        //    var item = new SubMenu();
        //    if (reader.IsColumnExists("MenuID") && reader.IsColumnExists("MenuParentID"))
        //    {
        //        if (SqlHelper.GetNullableInt32(reader, "MenuID") == SqlHelper.GetNullableInt32(reader, "MenuParentID"))
        //        {
        //            if (reader.IsColumnExists("MenuID"))
        //                item.MenuId = SqlHelper.GetNullableInt32(reader, "MenuID");

        //            if (reader.IsColumnExists("MenuName"))
        //                item.MenuName = SqlHelper.GetNullableString(reader, "MenuName");

        //            if (reader.IsColumnExists("MenuPath"))
        //                item.MenuPath = SqlHelper.GetNullableString(reader, "MenuPath");

        //            if (reader.IsColumnExists("MenuParentID"))
        //                item.MenuParentID = SqlHelper.GetNullableInt32(reader, "MenuParentID");

        //            if (reader.IsColumnExists("IsActive"))
        //                item.IsActive = SqlHelper.GetNullableInt32(reader, "IsActive");


                    
        //        }
        //    }
        //    return item;
        //}

        public static List<RoleAndMenuEntity> TranslateAsRoleAndMenuList(this SqlDataReader reader)
        {
            var list = new List<RoleAndMenuEntity>();

            while (reader.Read())
            {
                list.Add(TranslateAsRoleAndMenu(reader, true));

            }
            return list;
        }

    }
}
