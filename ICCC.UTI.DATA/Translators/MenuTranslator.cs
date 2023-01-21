using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICCC.UTI.DATA.Translators
{
    public static class MenuTranslator
    {
        public static MenuEntity TranslateAsMenu(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new MenuEntity();
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
           

            return item;
        }

        public static List<MenuEntity> TranslateAsMenuList(this SqlDataReader reader)
        {
            var list = new List<MenuEntity>();
            while (reader.Read())
            {
                list.Add(TranslateAsMenu(reader, true));
            }
            return list;
        }
    }
}
