using ICCC.UTI.DATA.DataInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataServices
{

    public class AppSetings : IAppSettings
    {
        private IConfiguration _confiquration;

        public AppSetings(IConfiguration configuration)
        {
            _confiquration = configuration;
        }
        public AppSetings()
        {
            
        }

        string IAppSettings.DBConnectionString()
        {
            return _confiquration["ConnectionStrings:DBConnectionString"];
        }
        string IAppSettings.JwtTokenKey()
        {
            return _confiquration["JWTDetails:Key"];
        }

        public string Key { get; set; }
    }
}
