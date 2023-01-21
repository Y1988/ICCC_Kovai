using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IMenu
    {
        List<MenuEntity> GetMenuDetails(string ConnectionString);
        string SaveMenu(MenuEntity model, string ConnectionString);
        string DeleteMenu(int id, string ConnectionString);


    }
}
