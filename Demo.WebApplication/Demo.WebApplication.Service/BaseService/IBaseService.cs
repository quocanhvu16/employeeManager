using Demo.WebApplication.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Service
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy thông tin tất cả các bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public List<T> GetAll();

        /// <summary>
        /// Lấy thông tin của 1 bản ghi theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public T GetRecordById(Guid id);

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
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteMultipleRecordByIds(List<Guid> ids);

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public ServiceResult InsertRecord(T record);

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public ServiceResult UpdateRecord(T record, Guid id);

        /// <summary>
        /// Validate dữ liệu cho những thành phần có đặc điểm chung
        /// </summary>
        /// <param name="record">Bản ghi caanfa validate</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (29/04/2023)
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<string, string> Validate(T record);

    }
}
