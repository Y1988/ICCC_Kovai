using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICCC.UTI.CORE.CoreEntities
{
    public class MySettingsEntity
    {
        public string Key { get; set; }
        public string DBConnectionString { get; set; }
        public string DefaultPassword { get; set; }
        
    }
    public class MySMTPSettingsEntity
    {
        public string From { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OutServer { get; set; }
        public int SMTPPort { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
