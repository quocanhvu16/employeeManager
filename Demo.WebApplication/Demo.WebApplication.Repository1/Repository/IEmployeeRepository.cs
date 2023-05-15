using Demo.WebApplication.Common;
using MySqlConnector;
using System.Data;

namespace Demo.WebApplication.Repository
{
    public interface IEmployeeRepository
    {   
        /// <summary>
        /// Hàm khởi tạo connection
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public IDbConnection GetOpenConnection();

        public MySqlTransaction GetTransaction(IDbConnection cnn);

        /// <summary>
        /// Hàm lấy thông tin 1 nhân viên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh(13/04/2023)
        public Employee GetEmployeeById(IDbConnection cnn, Guid employeeId);

        /// <summary>
        /// Hàm lấy các dữ liệu đã được phân trang và filter
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <param name="keyword"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public PagingResult GetPaging(IDbConnection cnn, Guid? departmentId, Guid? positionId, string? keyword, int pageSize = 20, int pageNumber = 1);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <param name="cnn"></param>
        /// <returns></returns>
        /// Ảuthor: Vũ Quốc Anh (13/04/2023)
        public string GetNewEmployeeCode(IDbConnection cnn);

        /// <summary>
        /// Hàm thêm nhân viên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public int InsertEmployee(IDbConnection cnn, Employee employee);

        /// <summary>
        /// Hàm update nhân viên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public int UpdateEmployee(IDbConnection cnn, Employee employee);

        /// <summary>
        /// Hàm lấy tất cả nhân viên theo filter
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public List<Employee> GetAll(IDbConnection cnn, string? keyword);

        /// <summary>
        /// Hàm xóa 1 nhân viên theo Id nhân viên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public int DeleteEmployeeById(IDbConnection cnn, Guid employeeId);

        /// <summary>
        /// Hàm xóa nhiều nhân viên theo các Id nhân viên
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employeeIdArray"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh ( 13/04/2023)
        public int DeleteMultipleEmployeeById(IDbConnection cnn, MySqlTransaction transaction, List<Guid> employeeIds);

        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="newEmployeeCode">Mã nhân viên mới</param>
        /// <param name="oldEmployeeCode">Mã nhân viên cũ (dùng trong trường hợp form là update)</param>
        /// <param name="formMode">Định dạng là form Update hoặc Add</param>
        /// <returns>1 là có trùng mã xảy ra , 0 là không có trùng mã xảy ra</returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        public int CheckEmployeeCode(IDbConnection cnn, string newEmployeeCode, string oldEmployeeCode, int formMode);

        /// <summary>
        /// Lấy mã nhân viên cũ trong form sửa để validate trùng mã
        /// </summary>
        /// <param name="cnn"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (15/04/2023)
        public string GetOldEmployeeCode(IDbConnection cnn,Guid employeeId);

    }
}
