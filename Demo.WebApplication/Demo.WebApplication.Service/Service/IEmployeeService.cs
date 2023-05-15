using Demo.WebApplication.Common;
using MySqlConnector;
using System.Data;

namespace Demo.WebApplication.Service
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        /// <summary>
        /// Hàm lấy các dữ liệu đã được phân trang và filter
        /// </summary>
        /// <param name="filter">Các filter được gửi từ client</param>
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
        /// Hàm tạo file excel và thiết kế file excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (18/04/2023)
        public byte[] ExportExcel(Filter filter);

        /// <summary>
        /// Check email đúng định dạng
        /// </summary>
        /// <param name="email">Email cần check</param>
        /// <returns>True nếu đúng định dạng , false nếu sai định dạng</returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        public bool IsValidEmail(string? email);

    }
}
