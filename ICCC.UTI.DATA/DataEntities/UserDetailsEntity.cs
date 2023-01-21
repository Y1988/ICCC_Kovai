using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataEntities
{
    public class UserDetailsEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EMAIL { get; set; }
        public string MobileNo { get; set; }
        public string KYCID { get; set; }
        public string KYCName { get; set; }
        public int DepartMentID { get; set; }
        public int IGovt { get; set; }
        public int IsVendor { get; set; }
        public int AccessPrivillage { get; set; }
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PinCode { get; set; }
        public int IsActive { get; set; }
        public string TokenKey { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string ReportingTo { get; set; }
        public string CreatedBy { get; set; }
    }
}
