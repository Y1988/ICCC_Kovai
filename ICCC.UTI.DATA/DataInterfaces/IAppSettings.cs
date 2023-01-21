using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IAppSettings
    {
        string DBConnectionString();
        string JwtTokenKey();

    }
}
