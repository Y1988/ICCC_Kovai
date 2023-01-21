using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IAuthentication
    {
        Task<UserDetailsEntity> AuthenticateAsync(UserDetailsEntity userDetailsEntity);
    }
}
