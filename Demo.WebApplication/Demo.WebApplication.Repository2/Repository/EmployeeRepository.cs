using Dapper;
using Demo.WebApplication.Common;
using MySqlConnector;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using static Dapper.SqlMapper;

namespace Demo.WebApplication.Repository
{
    public class EmployeeRepository : BaseRepository<Employee> ,IEmployeeRepository
    {

        #region Method

        /// <summary>
        /// Hàm lấy tất cả nhân viên theo filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public List<Employee> ExportExcel(Filter filter)
        {
            try
            {
                //B1: Chuyển đổi dữ liệu
                if (filter.keyword == null)
                {
                    filter.keyword = "";
                }
                string sqlSort = BuildQuerySort(filter);
                string sqlFilter = BuildQueryFilter(filter);
                //B2: Chuẩn bị tham số cho procedure
                var parameters = new DynamicParameters();
                parameters.Add("@keyword", filter.keyword);
                parameters.Add("@departmentId", filter.departmentId);

                //B3: Chuẩn bị tên stored procedure
                string sql = "SELECT * FROM view_employee ve WHERE ((ve.EmployeeCode LIKE CONCAT('%',@keyword,'%')) OR (ve.FullName LIKE CONCAT('%',@keyword,'%'))) AND IF(LENGTH(@departmentId) > 0, DepartmentId = @departmentId, 1) "+ sqlFilter + sqlSort;

                //B4: Thực hiện gọi database
                var employees = _cnn.Query<Employee>(sql, param: parameters);

                //B5:Dùng xong database thì đóng kết nối
                _cnn.Close();

                //B6:Trả về kết quả
                return (List<Employee>)employees;
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm lấy các dữ liệu đã được phân trang và filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public PagingResult GetPaging(Filter filter)
        {
            try
            {
                //B1: Chuyển đổi dữ liệu
                var limit = filter.pageSize;
                var offset = filter.pageSize * (filter.pageNumber - 1);
                if (filter.keyword == null)
                {
                    filter.keyword = "";
                }
                string sqlSort = BuildQuerySort(filter);
                string sqlFilter = BuildQueryFilter(filter);
                //B2:Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@offset", offset);
                parameters.Add("@limit", limit);
                parameters.Add("@keyword", filter.keyword);
                parameters.Add("@departmentId", filter.departmentId);
                //B3: Chuẩn bị tên stored procedure
                string sql = "SELECT * FROM view_employee ve WHERE ((ve.EmployeeCode LIKE CONCAT('%',@keyword,'%')) OR (ve.FullName LIKE CONCAT('%',@keyword,'%'))) AND IF(LENGTH(@departmentId) > 0, DepartmentId = @departmentId, 1) " + sqlFilter + sqlSort + " LIMIT @offset,@limit;" +
                    "SELECT COUNT(1) FROM view_employee ve WHERE ((ve.EmployeeCode LIKE CONCAT('%',@keyword,'%')) OR (ve.FullName LIKE CONCAT('%', @keyword ,'%'))) AND IF(LENGTH(@departmentId) > 0, DepartmentId = @departmentId, 1) " + sqlFilter + sqlSort + "; ";
                //B4: Thực hiện gọi database
                var data = _cnn.QueryMultiple(sql, param: parameters);
                //4.1. Lấy danh sách nhân viên
                var employees = data.Read<Employee>();
                //4.2. Lấy tổng số bản ghi nhân viên
                var totalRecord = data.ReadSingleOrDefault<int>();

                //B5:Dùng xong database thì đóng kết nối
                _cnn.Close();

                //B6:Trả về kết quả
                return new PagingResult()
                {
                    Data = (List<Employee>)employees,
                    TotalRecord = totalRecord
                };
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Ảuthor: Vũ Quốc Anh (13/04/2023)
        public string GetNewEmployeeCode()
        {
            try
            {
                //B1: Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_Employee_GetNewEmployeeCode";

                //B2: Gọi tới database
                var newEmployeeCode = _cnn.QueryFirstOrDefault<string>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                //B3: Đóng database
                _cnn.Close();

                //B4:Trả về kết quả
                return newEmployeeCode;
            }
            catch (Exception ex)
            {
                _cnn.Close();
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm build query liên quan tới sắp xếp
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (21/04/2023)
        private string BuildQuerySort(Filter filter)
        {
            string sqlSort = "";
            int sortLength = filter.sort.Length;

            if (sortLength > 0)
            {
                sqlSort = "ORDER BY ";
                for (int i = 0; i < sortLength; i++)
                {
                    string order = filter.sort[i].Substring(0, 1);
                    string sortName = filter.sort[i].Substring(1);
                    if (order == "-")
                    {
                        order = "DESC";
                    }
                    else
                    {
                        order = "ASC";
                    }
                    if (sortLength == 1)
                    {
                        if (sortName == "FullName")
                        {
                            sqlSort = sqlSort + "FirstName COLLATE utf8mb4_vietnamese_ci " + order + ", FamilyName COLLATE utf8mb4_vietnamese_ci " + order + ", MiddleName COLLATE utf8mb4_vietnamese_ci " + order + " ";
                            //sqlSort = sqlSort + "FirstName COLLATE utf8mb4_vietnamese_ci " + order + ", FamilyName COLLATE utf8mb4_vietnamese_ci " + order+ " ";
                        }
                        else
                        {
                            sqlSort = sqlSort + sortName + " " + order + " ";
                        }
                    }
                    else
                    {
                        if (i != sortLength - 1)
                        {
                            if (sortName == "FullName")
                            {
                                sqlSort = sqlSort + "FirstName COLLATE utf8mb4_vietnamese_ci " + order + ", FamilyName COLLATE utf8mb4_vietnamese_ci " + order + ", MiddleName COLLATE utf8mb4_vietnamese_ci " + order + ", ";
                            }
                            else
                            {
                                sqlSort = sqlSort + sortName + " " + order + ", ";
                            }
                        }
                        else
                        {
                            if (sortName == "FullName")
                            {
                                sqlSort = sqlSort + "FirstName COLLATE utf8mb4_vietnamese_ci " + order + ", MiddleName COLLATE utf8mb4_vietnamese_ci " + order + " ";
                            }
                            else
                            {
                                sqlSort = sqlSort + sortName + " " + order + " ";
                            }
                        }
                    }
                }
            }
            return sqlSort;
        }

        /// <summary>
        /// Hàm build query liên quan tới lọc dữ liệu
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (21/04/2023)
        private string BuildQueryFilter(Filter filter)
        {
            string sqlFilter = "";
            int filterLength = filter.dataFilter.Length;
            if (filterLength > 0)
            {
                char delimeter = '-';
                for (int i = 0; i < filterLength; i++)
                {
                    List<int> positions = new List<int>();
                    int index = filter.dataFilter[i].IndexOf(delimeter);
                    while (positions.Count < 2)
                    {
                        positions.Add(index);
                        index = filter.dataFilter[i].IndexOf(delimeter, index + 1);
                    }
                    string column = filter.dataFilter[i].Substring(0, positions[0]);
                    int filterMode;
                    string filterText = "";
                    if (positions[1] != -1)
                    {
                        filterMode = int.Parse(filter.dataFilter[i].Substring(positions[0] + 1, positions[1] - positions[0] - 1));
                        filterText = filter.dataFilter[i].Substring(positions[1] + 1);
                    }
                    else
                    {
                        filterMode = int.Parse(filter.dataFilter[i].Substring(positions[0] + 1));
                    }
                    switch (filterMode)
                    {
                        case (int)FilterMode.empty:
                            sqlFilter = sqlFilter + " AND " + column + " IS NULL ";
                            break;
                        case (int)FilterMode.notEmpty:
                            sqlFilter = sqlFilter + " AND " + column + " IS NOT NULL ";
                            break;
                        case (int)FilterMode.equal:
                            sqlFilter = sqlFilter + " AND " + column + " = '" + filterText + "' ";
                            break;
                        case (int)FilterMode.different:
                            sqlFilter = sqlFilter + " AND " + column + " != '" + filterText + "' ";
                            break;
                        case (int)FilterMode.contain:
                            sqlFilter = sqlFilter + " AND " + column + " LIKE '%" + filterText + "%' ";
                            break;
                        case (int)FilterMode.notContain:
                            sqlFilter = sqlFilter + " AND " + column + " NOT LIKE '%" + filterText + "%' ";
                            break;
                        case (int)FilterMode.startWith:
                            sqlFilter = sqlFilter + " AND " + column + " LIKE '" + filterText + "%' ";
                            break;
                        case (int)FilterMode.endWith:
                            sqlFilter = sqlFilter + " AND " + column + " LIKE '%" + filterText + "' ";
                            break;
                        case (int)FilterMode.less:
                            sqlFilter = sqlFilter + " AND " + column + " < '" + filterText + "' ";
                            break;
                        case (int)FilterMode.lessOrEqual:
                            sqlFilter = sqlFilter + " AND " + column + " <= '" + filterText + "' ";
                            break;
                        case (int)FilterMode.bigger:
                            sqlFilter = sqlFilter + " AND " + column + " > '" + filterText + "' ";
                            break;
                        case (int)FilterMode.biggerOrEqual:
                            sqlFilter = sqlFilter + " AND " + column + " >= '" + filterText + "' ";
                            break;
                    }
                }
            }
            return sqlFilter;
        }

        #endregion
    }
}
