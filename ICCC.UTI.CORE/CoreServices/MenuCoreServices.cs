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
using CICCC.UTI.CORE.CoreEntities;
using System.Reflection;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataServices;

namespace ICCC.UTI.CORE.CoreServices
{
    public class MenuCoreServices : IMenuCore
    {
        private IAppSettings _appSettings;

        
        public List<MenuCoreEntity> GetMenuDetails(string ConnectionString)
        {
            var Data = TranslateCoreMenuList(DbClientFactory<MenuDbClient>.Instance.GetMenuDetails(ConnectionString));
            return Data;

        }

        public string SaveMenu(MenuCoreEntity model, string ConnectionString)
        {
            var data = DbClientFactory<MenuDbClient>.Instance.SaveMenu(TranslateMenuList(model), ConnectionString);
            return data;

        }

        public string DeleteMenu(int id, string ConnectionString)
        {
            var data = DbClientFactory<MenuDbClient>.Instance.DeleteMenu(id, ConnectionString);
            return data;

        }
        public static List<MenuCoreEntity> TranslateCoreMenuList(List<MenuEntity> MenuDetails)
        {
            List<MenuCoreEntity> List = new List<MenuCoreEntity>();
            foreach (MenuEntity Menu in MenuDetails)
            {
                List.Add(new MenuCoreEntity
                {
                    MenuId = Menu.MenuId,
                    MenuName = Menu.MenuName,
                    MenuParentID = Menu.MenuParentID,
                    MenuPath = Menu.MenuPath,
                    IsActive=Menu.IsActive,
                  
                });
            }
            return List;



        }
        public static MenuEntity TranslateMenuList(MenuCoreEntity menu)
        {
            MenuEntity entity = new MenuEntity();

            entity.MenuId = menu.MenuId;
            entity.MenuName = menu.MenuName;
            entity.MenuParentID = menu.MenuParentID;
            entity.MenuPath = menu.MenuPath;
            entity.IsActive = menu.IsActive;
            
            return entity;

        }




    }
}

