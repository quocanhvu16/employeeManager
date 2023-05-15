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
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Hàm khởi tạo connection
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public IDbConnection GetOpenConnection();

        /// <summary>
        /// Hàm khởi tạo transaction
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public MySqlTransaction GetTransaction();
        
        /// <summary>
        /// Lấy thông tin của 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public T GetRecordById(Guid id);

        /// <summary>
        /// Lấy thông tin tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public List<T> GetAll();

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteRecordById(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteMultipleRecord(MySqlTransaction transaction, List<Guid>ids);

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int InsertRecord(T record);

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int UpdateRecord(T record , Guid id);

        /// <summary>
        /// Check trùng code với những trường dữ liệu có thuộc tính NotDuplicate
        /// </summary>
        /// <param name="needCheck">Mã cần check</param>
        /// <param name="recordId">Bản ghi cần check</param>
        /// <param name="mode">Check theo loại nào</param>
        /// <returns></returns>
        /// /// Author: Vũ Quốc Anh (29/04/2023)
        public bool CheckDuplicateCode(string needCheck, Guid recordId, CheckDuplicate mode);
    }
}
