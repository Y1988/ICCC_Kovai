using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreInterfaces;
using ICCC.UTI.DATA.DataInterfaces;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ICCC.UTI.CORE.CoreServices
{
    public class RoleAndMenuCoreServices : IRoleAndMenuCore
    {
        private IAppSettings _appSettings;


        public List<MenuItems> GetRoleAndMenuDetails(int Id, string ConnectionString)
        {
            var Data = DbClientFactory<RoleAndMenuDbClient>.Instance.GetRoleAndMenuDetails(Id, ConnectionString);
            var list = TranslateCoreRoleAndMenuList(Data, Data.First());
            return (List<MenuItems>)list;


        }

        //public string SaveRoleAndMenu(RoleAndMenuCoreEntity model, string ConnectionString)
        //{


        //    var data = DbClientFactory<RoleAndMenuDbClient>.Instance.SaveRoleAndMenu(TranslateRoleAndMenuList(model), ConnectionString);
        //    return data;



        //}

        //public string DeleteRoleAndMenu(int id, string ConnectionString)
        //{
        //    var data = DbClientFactory<RoleAndMenuDbClient>.Instance.DeleteRoleAndMenu(id, ConnectionString);
        //    return data;

        //}
        //public static List<RoleAndMenuCoreEntity> TranslateCoreRoleAndMenuList(IList<RoleAndMenuEntity> MenuDetails)
        //{
        //    List<RoleAndMenuCoreEntity> ParenList = new List<RoleAndMenuCoreEntity>();


        //    foreach (RoleAndMenuEntity Menu in MenuDetails)
        //    {

        //        ParenList.Add(new RoleAndMenuCoreEntity
        //        {
        //            // SubMenu = TranslateCoreMenuList(Menu.Menus),
        //            RoleAndMenuId = Menu.RoleAndMenuId,
        //            RoleName = Menu.RoleName,
        //            RoleId = Menu.RoleId,
        //            //MenuId = Menu.MenuId,
        //            //MenuName = Menu.MenuName,
        //            //MenuParentID = Menu.MenuParentID,
        //            //MenuPath = Menu.MenuPath,
        //            //IsActive = Menu.IsActive,

        //        });

        //    }
        //    return ParenList;


        //}

        public static IList<MenuItems> TranslateCoreRoleAndMenuList(IEnumerable<RoleAndMenuEntity> collection, RoleAndMenuEntity rootitem)

        {
            IList<MenuItems> lst = new List<MenuItems>();
            foreach (RoleAndMenuEntity c in collection.Where(c => c.MenuParentID == 0))
            {
                lst.Add(new MenuItems
                {
                    RoleAndMenuId = c.RoleAndMenuId,
                    RoleName = c.RoleName,
                    RoleId = c.RoleId,
                    MenuId = c.MenuId,
                    MenuName = c.MenuName,
                    MenuParentID = c.MenuParentID,
                    MenuPath = c.MenuPath,
                    IsActive = c.IsActive,
                    SubScreens = TranslateCSubscreensMenuList(collection, c)
                });
            }
            return lst;
        }

        public static IList<MenuItems> TranslateCSubscreensMenuList(IEnumerable<RoleAndMenuEntity> collection, RoleAndMenuEntity rootitem)

        {
            IList<MenuItems> lst = new List<MenuItems>();
            foreach (RoleAndMenuEntity c in collection.Where(c => c.MenuParentID == rootitem.MenuId))
            {
                lst.Add(new MenuItems
                {
                    RoleAndMenuId = c.RoleAndMenuId,
                    RoleName = c.RoleName,
                    RoleId = c.RoleId,
                    MenuId = c.MenuId,
                    MenuName = c.MenuName,
                    MenuParentID = c.MenuParentID,
                    MenuPath = c.MenuPath,
                    IsActive = c.IsActive,
                    SubScreens = TranslateCSubscreensMenuList(collection, c)
                });
            }
            return lst.Count == 0 ? null : lst;
        }
        public static RoleAndMenuEntity TranslateRoleAndMenuList(RoleAndMenuCoreEntity menu)
        {
            RoleAndMenuEntity entity = new RoleAndMenuEntity();
            entity.RoleAndMenuId = menu.RoleAndMenuId;
            entity.RoleName = menu.RoleName;
            entity.RoleId = menu.RoleId;
            //entity.MenuId = menu.MenuId;
            //entity.MenuName = menu.MenuName;
            //entity.MenuParentID = menu.MenuParentID;
            //entity.MenuPath = menu.MenuPath;
            //entity.IsActive = menu.IsActive;

            return entity;

        }
        //public static List<SubMenu> TranslateCoreMenuList(IList<Menus> Menus)
        //{
        //    List<SubMenu> List = new List<SubMenu>();

        //    foreach (Menus Menu in Menus)
        //    {

        //        List.Add(new SubMenu
        //        {

        //            MenuId = Menu.MenuId,
        //            MenuName = Menu.MenuName,
        //            MenuParentID = Menu.MenuParentID,
        //            MenuPath = Menu.MenuPath,
        //            IsActive = Menu.IsActive,
        //            RoleId = Menu.RoleId


        //        });

        //    }
        //    return List;



        //}
        //public static MenuEntity TranslateMenuList(MenuCoreEntity menu)
        //{
        //    MenuEntity entity = new MenuEntity();

        //    entity.MenuId = menu.MenuId;
        //    entity.MenuName = menu.MenuName;
        //    entity.MenuParentID = menu.MenuParentID;
        //    entity.MenuPath = menu.MenuPath;
        //    entity.IsActive = menu.IsActive;


        //    return entity;

        //}




    }
}

