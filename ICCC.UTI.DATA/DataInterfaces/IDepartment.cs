using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.DATA.DataInterfaces
{
    public interface IDepartment
    {
        public List<DepartmentDetailsEntity> GetAllDepartments(string ConnectionString);
        public string SaveDepartment(DepartmentDetailsEntity model, string ConnectionString);
        public string DeleteDepartment(int id, string ConnectionString);
    }
}
