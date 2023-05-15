using Dapper;
using Demo.WebApplication.Common;
using MySqlConnector;
using System.Data;
using static Dapper.SqlMapper;

namespace Demo.WebApplication.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Field

        private readonly string _connectionString = Resource.connectionString;

        #endregion

        #region Method
        public IDbConnection GetOpenConnection()
        {
            //B1:Khởi tạo tới database
            var cnn = new MySqlConnection(_connectionString);

            //B2: Mở kết nối tới database
            cnn.Open();

            //B3: return về kết quả
            return cnn;
        }

        public Employee GetEmployeeById(IDbConnection cnn, Guid employeeId)
        {
            //B1: Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_GetEmployeeById";

            //B2:Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", employeeId);

            //B3:Thực hiện gọi database
            var employee = cnn.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //B3:Dùng xong database thì đóng kết nối
            cnn.Close();

            //B4: Trả về kết quả lấy được từ database
            return employee;
        }

        public PagingResult GetPaging(IDbConnection cnn, Guid? departmentId, Guid? positionId, string? keyword, int pageSize = 20, int pageNumber = 1)
        {
            //B1: Chuyển đổi dữ liệu
            var limit = pageSize;
            var offset = pageSize * (pageNumber - 1);

            //B2:Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("p_offset", offset);
            parameters.Add("p_limit", limit);
            if (keyword == null)
            {
                keyword = "";
            }
            parameters.Add("p_keyword", keyword);

            //B3: Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_GetPaging";

            //B4: Thực hiện gọi database
            var data = cnn.QueryMultiple(storedProcedureName, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            //4.1. Lấy danh sách nhân viên
            var employees = data.Read<Employee>();
            //4.2. Lấy tổng số bản ghi nhân viên
            var totalRecord = data.ReadSingleOrDefault<int>();

            //B5:Dùng xong database thì đóng kết nối
            cnn.Close();

            //B6:Trả về kết quả
            return new PagingResult()
            {
                Data = (List<Employee>)employees,
                TotalRecord = totalRecord
            };
        }

        public string GetNewEmployeeCode(IDbConnection cnn)
        {
            //B1: Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_GetNewEmployeeCode";

            //B2: Gọi tới database
            var newEmployeeCode = cnn.QueryFirstOrDefault<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

            //B3: Đóng database
            cnn.Close();

            //B4:Trả về kết quả
            return newEmployeeCode;
        }

        public int InsertEmployee(IDbConnection cnn, Employee employee)
        {
            string storedProcedureName = "Proc_Employee_Insert";
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", Guid.NewGuid());
            parameters.Add("p_EmployeeCode", employee.EmployeeCode);
            parameters.Add("p_FullName", employee.FullName);
            parameters.Add("p_Gender", employee.Gender);
            parameters.Add("p_DateOfBirth", employee.DateOfBirth);
            parameters.Add("p_DepartmentId", employee.DepartmentId);
            parameters.Add("p_IdentityNumber", employee.IdentityNumber);
            parameters.Add("p_IdentityDate", employee.IdentityDate);
            parameters.Add("p_IdentityPlace", employee.IdentityPlace);
            parameters.Add("p_PositionId", employee.PositionId);
            parameters.Add("p_Address", employee.Address);
            parameters.Add("p_PhoneNumber", employee.PhoneNumber);
            parameters.Add("p_Salary", employee.Salary);
            parameters.Add("p_Email", employee.Email);
            parameters.Add("p_BankId", employee.BankId);
            parameters.Add("p_BankName", employee.BankName);
            parameters.Add("p_BankAddress", employee.BankAddress);
            parameters.Add("p_CreatedBy", "Admin Dự Án");
            parameters.Add("p_CreatedDate", DateTime.Now);

            var res = cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            cnn.Close();

            return res;
        }

        public int UpdateEmployee(IDbConnection cnn, Employee employee)
        {
            string storedProcedureName = "Proc_Employee_Update";

            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", employee.EmployeeId);
            parameters.Add("p_EmployeeCode", employee.EmployeeCode);
            parameters.Add("p_FullName", employee.FullName);
            parameters.Add("p_Gender", employee.Gender);
            parameters.Add("p_DateOfBirth", employee.DateOfBirth);
            parameters.Add("p_DepartmentId", employee.DepartmentId);
            parameters.Add("p_IdentityNumber", employee.IdentityNumber);
            parameters.Add("p_IdentityDate", employee.IdentityDate);
            parameters.Add("p_IdentityPlace", employee.IdentityPlace);
            parameters.Add("p_PositionId", employee.PositionId);
            parameters.Add("p_Address", employee.Address);
            parameters.Add("p_PhoneNumber", employee.PhoneNumber);
            parameters.Add("p_Salary", employee.Salary);
            parameters.Add("p_Email", employee.Email);
            parameters.Add("p_BankId", employee.BankId);
            parameters.Add("p_BankName", employee.BankName);
            parameters.Add("p_BankAddress", employee.BankAddress);
            parameters.Add("p_ModifiedBy", "Vũ Quốc Anh");
            parameters.Add("p_ModifiedDate", DateTime.Now);

            var res = cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            cnn.Close();

            return res;
        }

        public List<Employee> GetAll(IDbConnection cnn, string? keyword)
        {

            //B2: Chuẩn bị tham số cho procedure
            if (keyword == null)
            {
                keyword = "";
            }
            var parameters = new DynamicParameters();
            parameters.Add("p_keyword", keyword);

            //B3: Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_GetAll";

            //B4: Thực hiện gọi database
            var employees = cnn.Query<Employee>(storedProcedureName, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

            //B5:Dùng xong database thì đóng kết nối
            cnn.Close();

            //B6:Trả về kết quả
            return (List<Employee>)employees;
        }

        public int DeleteEmployeeById(IDbConnection cnn, Guid employeeId)
        {
            //B1:Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_DeleteById";

            //B2: Chuẩn bị tham số cho procedure
            var parameters = new DynamicParameters();
            parameters.Add("p_EmployeeId", employeeId);

            //B3:Thực hiện gọi vào database để chạy stored procedure
            var number = cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            //B4: Đóng database
            cnn.Close();

            //Trả về kết quả
            return number;

        }

        public int DeleteMultipleEmployeeById(IDbConnection cnn, MySqlTransaction transaction, List<Guid> employeeIds)
        {
            //B1:Chuẩn bị tên stored procedure
            string storedProcedureName = "Proc_Employee_DeleteMultipleByIds";

            //B2:Chuẩn bị tham số đầu vào cho stored procedure
            var parameters = new DynamicParameters();
            string employeeIdArray = "";
            //Nối các id thành 1 chuỗi rồi gửi lên database
            for (int i = 0; i < employeeIds.Count; i++)
            {
                if (i == employeeIds.Count - 1)
                {
                    employeeIdArray = string.Concat(employeeIdArray, employeeIds[i]);
                }
                else
                {
                    employeeIdArray = string.Concat(employeeIdArray, employeeIds[i], ",");
                }
            }
            parameters.Add("p_EmployeeIds", employeeIdArray);

            var number = cnn.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

            if(number == employeeIds.Count)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return number;
        }

        public MySqlTransaction GetTransaction(IDbConnection cnn)
        {
            return (MySqlTransaction)cnn.BeginTransaction();
        }

        public int CheckEmployeeCode(IDbConnection cnn, string newEmployeeCode, string oldEmployeeCode, int formMode)
        {
            //B2: Khởi tạo tham số đầu vào
            var parameters = new DynamicParameters();
            if (formMode == FormMode.Add)
            {
                //Viết câu lệnh SQL
                var sqlCheck = "SELECT EmployeeCode FROM employee WHERE EmployeeCode = @EmployeeCode";
                //Truyền tham số đầu vào
                parameters.Add("@EmployeeCode", newEmployeeCode);
                //Gọi database
                var res = cnn.QueryFirstOrDefault<string>(sqlCheck, param: parameters);
                Console.WriteLine(res is null);
                //Xử lý kết quả
                //Có trùng mã
                if (res != null)
                {
                    return 1;
                }
                //Không trùng mã
                return 0;
            }
            //3.1. Nếu là form Sửa nhân viên
            else
            {
                //Viết câu lệnh SQL
                var sqlCheck = "SELECT EmployeeCode FROM employee WHERE (NOT EmployeeCode = @oldEmployeeCode) AND EmployeeCode = @newEmployeeCode;";
                //Truyền tham số đầu vào
                parameters.Add("@newEmployeeCode", newEmployeeCode);
                parameters.Add("@oldEmployeeCode", oldEmployeeCode);
                //Gọi database
                var res = cnn.QueryFirstOrDefault<string>(sqlCheck, param: parameters);
                //Xử lý kết quả
                //Có trùng mã
                if (res != null)
                {
                    return 1;
                }
                //Không trùng mã
                return 0;
            }
        }

        public string GetOldEmployeeCode(IDbConnection cnn, Guid employeeId)
        {
            var sqlCheck = $"SELECT EmployeeCode from employee where EmployeeId = '{employeeId}'";
            var oldEmployeeCode = cnn.QueryFirstOrDefault<string>(sql: sqlCheck);

            return oldEmployeeCode;
        }

        #endregion
    }
}
