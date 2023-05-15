using Demo.WebApplication.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Repository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
    }
}
