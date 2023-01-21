using CICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreServices;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataServices;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Repositories;
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
    public class UserDetailsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public UserDetailsController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                _logger.LogInfo("Function GetAllUsers Started");
                var data = DbClientFactory<UserCoreServices>.Instance.GetAllUsers(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetAllUsers End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("SaveUser")]
        public IActionResult SaveUser([FromBody] UserDetailsCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function SaveUser Started");
                var msg = new Message<UserDetailsCoreEntity>();
                var data = DbClientFactory<UserCoreServices>.Instance.SaveUser(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.UserId == 0)
                        msg.ReturnMessage = "User saved successfully";
                    else
                        msg.ReturnMessage = "User updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Email Id already exists";
                }
                else if (data == "ICCC202")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Mobile Number already exists";
                }
                _logger.LogInfo("Function SaveUser End");
                return Ok(msg);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("DeleteUser")]
        public IActionResult DeleteUser([FromBody] UserDetailsCoreEntity model)  
        {
            try
            {

                _logger.LogInfo("Function DeleteUser Started");
                var msg = new Message<UserDetailsEntity>();
                var data = DbClientFactory<UserCoreServices>.Instance.DeleteUser(model.UserId, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "User Deleted";
                }
                else if (data == "ICCC203")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Invalid record";
                }
                _logger.LogInfo("Function DeleteUser End");
                return Ok(msg);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("MappingUserandRole")]
        public IActionResult MappingUserandRole([FromBody] MappingRoleAndUserCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function MappingUserandRole Started");
                var msg = new Message<MappingRoleAndUserCoreEntity>();
                var data = DbClientFactory<UserCoreServices>.Instance.MappingUserandRole(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.UserId == 0)
                        msg.ReturnMessage = "MappingUserandRole saved successfully";
                    else
                        msg.ReturnMessage = "MappingUserandRole updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "MappingUserandRole already exists";
                }
                
                _logger.LogInfo("Function MappingUserandRole End");
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
