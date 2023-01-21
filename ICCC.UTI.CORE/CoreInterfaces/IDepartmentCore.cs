using ICCC.UTI.CORE.CoreEntities;
using ICCC.UTI.DATA.DataEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICCC.UTI.CORE.CoreInterfaces
{
    public interface IDepartmentCore
    {
         List<DepartmentDetailsCoreEntity> GetAllDepartments(string ConnectionString);
         string SaveDepartment(DepartmentDetailsCoreEntity model, string ConnectionString);
         string DeleteDepartment(int id, string ConnectionString);
    }
}
