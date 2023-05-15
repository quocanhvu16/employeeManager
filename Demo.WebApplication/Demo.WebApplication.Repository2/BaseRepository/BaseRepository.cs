using Dapper;
using Demo.WebApplication.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly string _connectionString = Resource.connectionString;

        protected readonly IDbConnection _cnn;

        public BaseRepository()
        {
            _cnn = GetOpenConnection();
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteMultipleRecord(MySqlTransaction transaction, List<Guid> ids)
        {
            try
            {
                //B1:Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_{typeof(T).Name}_DeleteMultipleByIds";

                //B2:Chuẩn bị tham số đầu vào cho stored procedure
                var parameters = new DynamicParameters();
                string employeeIdArray = "";
                //Nối các id thành 1 chuỗi rồi gửi lên database
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i == ids.Count - 1)
                    {
                        employeeIdArray = string.Concat(employeeIdArray, ids[i]);
                    }
                    else
                    {
                        employeeIdArray = string.Concat(employeeIdArray, ids[i], ",");
                    }
                }
                parameters.Add($"p_{typeof(T).Name}Ids", employeeIdArray);

                var number = _cnn.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                if (number == ids.Count)
                {
                    transaction.Commit();
                    _cnn.Close();
                }
                else
                {
                    transaction.Rollback();
                    _cnn.Close();
                }

                return number;
            }
            catch (Exception ex) {
                transaction.Rollback();
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteRecordById(Guid id)
        {
            try
            {
                //B1:Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_{typeof(T).Name}_DeleteById";

                //B2: Chuẩn bị tham số cho procedure
                var parameters = new DynamicParameters();
                parameters.Add($"p_{typeof(T).Name}Id", id);

                //B3:Thực hiện gọi vào database để chạy stored procedure
                var number = _cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //B4: Đóng database
                _cnn.Close();

                //Trả về kết quả
                return number;
            }
            catch(Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Lấy thông tin tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public List<T> GetAll()
        {
            try
            {
                string storedProcedureName = $"Proc_{typeof(T).Name}_GetAll";

                var records = _cnn.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                _cnn.Close();

                return (List<T>)records;
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Lấy thông tin của 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public T GetRecordById(Guid id)
        {
            try
            {
                //B1: Chuẩn bị tên stored procedure
                string storedProcedureName = $"Proc_{typeof(T).Name}_GetEmployeeById";

                //B2:Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add($"p_{typeof(T).Name}Id", id);

                //B3:Thực hiện gọi database
                var record = _cnn.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //B3:Dùng xong database thì đóng kết nối
                _cnn.Close();

                //B4: Trả về kết quả lấy được từ database
                return record;
            }
            catch(Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();

            }
        }

        /// <summary>
        /// Hàm khởi tạo connection
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public IDbConnection GetOpenConnection()
        {
            try
            {
                //B1:Khởi tạo tới database
                var cnn = new MySqlConnection(_connectionString);

                //B2: Mở kết nối tới database
                cnn.Open();

                //B3: return về kết quả
                return cnn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm khởi tạo transaction
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public MySqlTransaction GetTransaction()
        {
            try
            {
                return (MySqlTransaction)_cnn.BeginTransaction();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int InsertRecord(T record)
        {
            try
            {
                string storedProcedureName = $"Proc_{typeof(T).Name}_Insert";
                var sqlCommand = _cnn.CreateCommand();
                sqlCommand.CommandText = storedProcedureName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters((MySqlCommand)sqlCommand);

                var parameters = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName.Replace("@", "");
                    var propName = parameter.ParameterName.Replace("@p_", "");
                    var propValue = record.GetType().GetProperty(propName).GetValue(record);
                    switch (propName)
                    {
                        case "CreatedBy":
                            parameters.Add(paramName, "Admin Dự Án");
                            break;
                        case "CreatedDate":
                            parameters.Add(paramName, DateTime.Now);
                            break;
                        default:
                            parameters.Add(paramName, propValue);
                            break;
                    }
                }

                var res = _cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                _cnn.Close();

                return res;
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int UpdateRecord(T record, Guid id)
        {
            try
            {
                string storedProcedureName = $"Proc_{typeof(T).Name}_Update";
                var sqlCommand = _cnn.CreateCommand();
                sqlCommand.CommandText = storedProcedureName;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                MySqlCommandBuilder.DeriveParameters((MySqlCommand)sqlCommand);

                var parameters = new DynamicParameters();
                foreach (MySqlParameter parameter in sqlCommand.Parameters)
                {
                    var paramName = parameter.ParameterName.Replace("@", "");
                    var propName = parameter.ParameterName.Replace("@p_", "");
                    var propValue = record.GetType().GetProperty(propName).GetValue(record);
                    switch (propName)
                    {
                        case "ModifiedBy":
                            parameters.Add(paramName, "Vũ Quốc Anh");
                            break;
                        case "ModifiedDate":
                            parameters.Add(paramName, DateTime.Now);
                            break;
                        default:
                            parameters.Add(paramName, propValue);
                            break;
                    }
                }

                var res = _cnn.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                _cnn.Close();

                return res;
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Check trùng code với những trường dữ liệu có thuộc tính NotDuplicate
        /// </summary>
        /// <param name="needCheck">Mã cần check</param>
        /// <param name="recordId">Bản ghi cần check</param>
        /// <param name="mode">Check theo loại nào</param>
        /// <returns></returns>
        /// /// Author: Vũ Quốc Anh (29/04/2023)
        public bool CheckDuplicateCode(string needCheck, Guid recordId, CheckDuplicate mode)
        {
            try
            {
                //B2: Khởi tạo tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@needCheck", needCheck);
                parameters.Add("@recordId", recordId);
                string sql = "";
                switch (mode)
                {
                    case CheckDuplicate.EmployeeCode:
                        sql = "SELECT EmployeeCode FROM employee e WHERE e.EmployeeCode = @needCheck AND e.EmployeeId != @recordId;";
                        break;
                    case CheckDuplicate.IdentityNumber:
                        sql = "SELECT IdentityNumber FROM employee e WHERE e.IdentityNumber = @needCheck AND e.EmployeeId != @recordId;";
                        break;
                }
                //Gọi database
                var res = _cnn.QueryFirstOrDefault<string>(sql, param: parameters);
                //Xử lý kết quả
                //Có trùng mã
                if (res != null)
                {
                    return true;
                }
                //Không trùng mã
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

    }
}
