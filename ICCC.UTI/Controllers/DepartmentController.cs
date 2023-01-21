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
    public class DepartmentController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public DepartmentController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            try
            {
                _logger.LogInfo("Function GetAllDepartments Started");
                var data = DbClientFactory<DepartmentCoreServices>.Instance.GetAllDepartments(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetAllDepartments End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("SaveDepartment")]
        public IActionResult SaveDepartment([FromBody] DepartmentDetailsCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function SaveDepartment Started");
                var msg = new Message<DepartmentDetailsCoreEntity>();
                var data = DbClientFactory<DepartmentCoreServices>.Instance.SaveDepartment(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.DepartmentID == 0)
                        msg.ReturnMessage = "Department saved successfully";
                    else
                        msg.ReturnMessage = "Department updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Department already exists";
                }
                _logger.LogInfo("Function SaveDepartment End");
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("DeleteDepartment")]
        public IActionResult DeleteDepartment([FromBody] int DepartmentId)
        {
            try
            {

                _logger.LogInfo("Function DeleteDepartment Started");
                var msg = new Message<DepartmentDetailsEntity>();
                var data = DbClientFactory<DepartmentCoreServices>.Instance.DeleteDepartment(DepartmentId, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Department Deleted";
                }
                else if (data == "ICCC203")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Invalid record";
                }
                _logger.LogInfo("Function DeleteDepartment End");
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
