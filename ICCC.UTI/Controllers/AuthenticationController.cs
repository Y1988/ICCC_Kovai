using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.Coreinterface;
using ICCC.UTI.LOGGER;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace ICCC.UTI.Controllers
{
    [EnableCors]
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationCore _authenticationCore;
        private readonly ILoggerManager _logger;
        public AuthenticationController(IAuthenticationCore authenticationCore, ILoggerManager logger)
        {
            _authenticationCore = authenticationCore;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthenticationCoreEntity coreEntity)
        {
            try
            {
                _logger.LogInfo("Authentication Post Function Started");
                var UserDetailsCoreEntity = _authenticationCore.AuthenticateCoreAsync(coreEntity.UserName, coreEntity.Password);
                _logger.LogInfo("Authentication Post Function End");
                if (UserDetailsCoreEntity.Result == null) { return BadRequest(); }
                return Ok(JsonSerializer.Serialize(UserDetailsCoreEntity.Result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }

        }

    }
}
