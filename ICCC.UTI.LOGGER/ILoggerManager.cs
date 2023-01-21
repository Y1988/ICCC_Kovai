using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.LOGGER
{
    public interface ILoggerManager
    {
       public void LogError(string message);
        public void LogWarning(string message);
        public void LogInfo(string message);
        public void LogDebug(string message);
    }
}
