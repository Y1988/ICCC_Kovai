using CICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.CORE.CoreServices;
using ICCC.UTI.DATA.DataEntities;
using ICCC.UTI.DATA.DataServices;
using ICCC.UTI.DATA.DataUtilities;
using ICCC.UTI.DATA.Repositories;
using ICCC.UTI.LOGGER;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IOptions<MySMTPSettingsEntity> _appSMTPSettings;
        public UserDetailsController(ILoggerManager logger, IOptions<MySettingsEntity> app, IOptions<MySMTPSettingsEntity> appSMTPSettings)
        {
            _logger = logger;
            appSettings = app;
            _appSMTPSettings = appSMTPSettings;
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

        [HttpGet]
        [Route("GetActiveUsers")]
        public IActionResult GetActiveUsers()
        {
            try
            {
                _logger.LogInfo("Function GetActiveUsers Started");
                var data = DbClientFactory<UserCoreServices>.Instance.GetActiveUsers(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetActiveUsers End");
                return Ok(JsonSerializer.Serialize(data));

            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message.ToString());
                return StatusCode(500, "Internel Server Error");
            }
        }
        [HttpGet]
        [Route("GetInActiveUsers")]
        public IActionResult GetInActiveUsers()
        {
            try
            {
                _logger.LogInfo("Function GetInActiveUsers Started");
                var data = DbClientFactory<UserCoreServices>.Instance.GetInActiveUsers(appSettings.Value.DBConnectionString);
                _logger.LogInfo("Function GetInActiveUsers End");
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
                model.Password = EncryptString(appSettings.Value.Key, appSettings.Value.DefaultPassword);
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
        [Route("ActivateUser")]
        public IActionResult ActivateUser([FromBody] UserDetailsCoreEntity model)
        {
            try
            {
                _logger.LogInfo("Function ActivateUser Started");
                var msg = new Message<UserDetailsCoreEntity>();
                var data = DbClientFactory<UserCoreServices>.Instance.ActivateUser(model.UserId, appSettings.Value.DBConnectionString, out string Returncode);
                if (Returncode == "ICCC200")
                {
                    msg.IsSuccess = true;
                    msg.ReturnMessage = "User Activated successfully";
                    SendEmail(data.ToList());
                    // msg.Data =data.ToList(); ;
                }
                else if (Returncode == "ICCC201")
                {
                    msg.IsSuccess = false;
                    msg.ReturnMessage = "User Id not exists";
                }
                _logger.LogInfo("Function ActivateUser End");
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendEmail(List<UserDetailsCoreEntity> userDetails)
        {
            string Body = _appSMTPSettings.Value.Body;
            Body = Body.Replace("#User", userDetails[0].FirstName+' '+ userDetails[0].LastName);
            Body = Body.Replace("#Password",DecryptString(appSettings.Value.Key, userDetails[0].Password));
            var Email = new MimeMessage();
            //Email.From.Add(MailboxAddress.Parse(_appSMTPSettings.Value.From.ToString()));
            Email.To.Add(MailboxAddress.Parse(userDetails[0].EMAIL));
            Email.To.Add(MailboxAddress.Parse(_appSMTPSettings.Value.From.ToString()));
            Email.Subject = _appSMTPSettings.Value.Subject;
            Email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = Body };
            using (var smtp = new SmtpClient())
            {
            smtp.Connect(_appSMTPSettings.Value.OutServer, _appSMTPSettings.Value.SMTPPort);
            smtp.Authenticate(_appSMTPSettings.Value.Username, _appSMTPSettings.Value.Password);
            smtp.Send(Email);
            smtp.Disconnect(true);

             }
            //return Ok();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
