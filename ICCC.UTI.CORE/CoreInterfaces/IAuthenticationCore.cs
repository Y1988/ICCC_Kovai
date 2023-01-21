using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ICCC.UTI.CORE.Coreinterface
{
    public interface IAuthenticationCore
    {
        Task<UserDetailsCoreEntity> AuthenticateCoreAsync(string Username, string Password);
    }
}
