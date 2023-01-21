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
    public class RolesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public RolesController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            try
            {
                _logger.LogInfo("Function GetAllRoles Started");
                var data = DbClientFactory<RoleCoreServices>.Instance.GetAllRoles(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetAllRoles End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("SaveRole")]
        public IActionResult SaveRole([FromBody] RoleDetailsCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function SaveRole Started");
                var msg = new Message<RoleDetailsCoreEntity>();
                var data = DbClientFactory<RoleCoreServices>.Instance.SaveRole(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.RoleID == 0)
                        msg.ReturnMessage = "Role saved successfully";
                    else
                        msg.ReturnMessage = "Role updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Role already exists";
                }
                _logger.LogInfo("Function SaveRole End");
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("DeleteRole")]
        public IActionResult DeleteRole([FromBody] int RoleId)
        {
            try
            {

                _logger.LogInfo("Function DeleteRole Started");
                var msg = new Message<RoleDetailsEntity>();
                var data = DbClientFactory<RoleCoreServices>.Instance.DeleteRole(RoleId, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Role Deleted";
                }
                else if (data == "ICCC203")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Invalid record";
                }
                _logger.LogInfo("Function DeleteRole End");
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
