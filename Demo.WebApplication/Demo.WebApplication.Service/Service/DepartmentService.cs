using Demo.WebApplication.Common;
using Demo.WebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Service
{
    public class DepartmentService : BaseService<Department>,IDepartmentService
    {
        #region Field

        private readonly IDepartmentRepository _departmentRepository;

        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
    }
}
