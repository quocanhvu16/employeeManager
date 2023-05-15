using Demo.WebApplication.Common;
using MySqlConnector;
using System.Data;

namespace Demo.WebApplication.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {   
        /// <summary>
        /// Hàm lấy các dữ liệu đã được phân trang và filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public PagingResult GetPaging(Filter filter);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Ảuthor: Vũ Quốc Anh (13/04/2023)
        public string GetNewEmployeeCode();

        /// <summary>
        /// Hàm lấy tất cả nhân viên theo filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public List<Employee> ExportExcel(Filter filter);

    }
}
