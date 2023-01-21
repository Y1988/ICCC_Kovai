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
    public class ReportingController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IOptions<MySettingsEntity> appSettings;
        public ReportingController(ILoggerManager logger, IOptions<MySettingsEntity> app)
        {
            _logger = logger;
            appSettings = app;
        }

        [HttpGet]
        [Route("GetAllReportings")]
        public IActionResult GetAllReportings()
        {
            try
            {
                _logger.LogInfo("Function GetAllReportings Started");
                var data = DbClientFactory<ReportingCoreServices>.Instance.GetAllReportings(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetAllReportings End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("SaveReporting")]
        public IActionResult SaveReporting([FromBody] ReportingDetailsCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function SaveReporting Started");
                var msg = new Message<ReportingDetailsCoreEntity>();
                var data = DbClientFactory<ReportingCoreServices>.Instance.SaveReporting(model, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    if (model.ReportingID == 0)
                        msg.ReturnMessage = "Reporting saved successfully";
                    else
                        msg.ReturnMessage = "Reporting updated successfully";
                }
                else if (data == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Reporting already exists";
                }
                _logger.LogInfo("Function SaveReporting End");
                return Ok(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }

        [HttpPost]
        [Route("DeleteReporting")]
        public IActionResult DeleteReporting([FromBody] int ReportingId)
        {
            try
            {

                _logger.LogInfo("Function DeleteReporting Started");
                var msg = new Message<ReportingDetailsEntity>();
                var data = DbClientFactory<ReportingCoreServices>.Instance.DeleteReporting(ReportingId, appSettings.Value.DBConnectionString);
                if (data == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "Reporting Deleted";
                }
                else if (data == "ICCC203")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "Invalid record";
                }
                _logger.LogInfo("Function DeleteReporting End");
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
