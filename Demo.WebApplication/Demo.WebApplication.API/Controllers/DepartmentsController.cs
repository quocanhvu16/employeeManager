using Dapper;
using Demo.WebApplication.Common;
using Demo.WebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Demo.WebApplication.API
{
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        #region Field

        private readonly IDepartmentService _departmentService;

        #endregion

        #region Constructor

        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }

        #endregion
    }
}
