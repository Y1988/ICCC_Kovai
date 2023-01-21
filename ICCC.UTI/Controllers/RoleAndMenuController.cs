using CICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreServices;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.LOGGER;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ICCC.UTI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleAndMenuController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public RoleAndMenuController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }
        [HttpPost]
        [Route("GetRoleAndMenuDetails")]
        public IActionResult GetRoleAndMenuDetails(MenuItems menu)
        {
            try
            {
                _logger.LogInfo("Function GetRoleAndMenuDetails Started");
                var data = DbClientFactory<RoleAndMenuCoreServices>.Instance.GetRoleAndMenuDetails(menu.RoleId, appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetRoleAndMenuDetails End");
                return Ok(data);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        //[HttpPost]
        //[Route("SaveMenu")]
        //public IActionResult SaveRoleAndMenu([FromBody]RoleAndMenuCoreEntity model)
        //{
        //    try
        //    {
        //        _logger.LogInfo("Function SaveRoleAndMenu Started");
        //        var msg = new Message<RoleAndMenuCoreEntity>();
        //        var data = DbClientFactory<RoleAndMenuCoreServices>.Instance.SaveRoleAndMenu(model, appSettings.Value.DBConnectionString);
        //        if (data == "ICCC200")
        //        {
        //            msg.IsSuccess = true;
        //            if (model.Menus[0].MenuId == 0)
        //                msg.ReturnMessage = "RoleAndMenu saved successfully";
        //            else
        //                msg.ReturnMessage = "RoleAndMenu updated successfully";
        //        }
        //        else if (data == "ICCC201")
        //        {
        //            msg.IsSuccess = false;
        //            msg.ReturnMessage = "RoleAndMenu already exists";
        //        }

        //        _logger.LogInfo("Function SaveRoleAndMenu End");
        //        return Ok(msg);
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex.Message.ToString());
        //        return StatusCode(500, "Internel Server Error");
        //    }
        //}

        //[HttpPost]
        //[Route("DeleteMenu")]
        //public IActionResult DeleteRoleAndMenu([FromBody] int RoleAndMenuId)
        //{
        //    try
        //    {

        //        _logger.LogInfo("Function DeleteRoleAndMenu Started");
        //        var msg = new Message<RoleAndMenuCoreEntity>();
        //        var data = DbClientFactory<RoleAndMenuCoreServices>.Instance.DeleteRoleAndMenu(RoleAndMenuId, appSettings.Value.DBConnectionString);
        //        if (data == "ICCC200")
        //        {
        //            msg.IsSuccess = true;
        //            msg.ReturnMessage = "RoleAndMenu Deleted";
        //        }
        //        else if (data == "ICCC203")
        //        {
        //            msg.IsSuccess = false;
        //            msg.ReturnMessage = "Invalid record";
        //        }
        //        _logger.LogInfo("Function DeleteRoleAndMenu End");
        //        return Ok(msg);
        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex.Message.ToString());
        //        return StatusCode(500, "Internel Server Error");
        //    }
        //}

        
    }
}
