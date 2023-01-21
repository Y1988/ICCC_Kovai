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
using System.Text.Json;

namespace ICCC.UTI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public MenuController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }
        [HttpGet]
        [Route("GetMenuDetails")]
        public IActionResult GetMenuDetails()
        {
            try
            {
                _logger.LogInfo("Function GetMenuDetails Started");
                var data = DbClientFactory<MenuCoreServices>.Instance.GetMenuDetails(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetMenuDetails End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("SaveMenu")]
        public IActionResult SaveMenu([FromBody] MenuCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function SaveMenu Started");
                var msg = new Message<MenuCoreEntity>();
                var data = DbClientFactory<MenuCoreServices>.Instance.SaveMenu(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.MenuId == 0)
                        msg.ReturnMessage = "Menu saved successfully";
                    else
                        msg.ReturnMessage = "Menu updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Menu already exists";
                }
                
                _logger.LogInfo("Function SaveMenu End");
                return Ok(msg);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("DeleteMenu")]
        public IActionResult DeleteMenu([FromBody] int MenuId)
        {
            try
            {

                _logger.LogInfo("Function DeleteMenu Started");
                var msg = new Message<MenuEntity>();
                var data = DbClientFactory<MenuCoreServices>.Instance.DeleteMenu(MenuId, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Menu Deleted";
                }
                else if (data == "ICCC203")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Invalid record";
                }
                _logger.LogInfo("Function DeleteMenu End");
                return Ok(msg);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        
    }
}
