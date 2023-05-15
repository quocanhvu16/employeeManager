using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    public class Filter
    {
        /// <summary>
        /// Từ khóa
        /// </summary>
        public string? keyword { get; set; }

        /// <summary>
        /// Số bản ghi 1 trang
        /// </summary>
        public int? pageSize { get; set; }

        /// <summary>
        /// Số trang
        /// </summary>
        public int? pageNumber { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public Guid? departmentId { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public string[]? sort { get; set; }

        /// <summary>
        /// Bộ lọc dữ liệu
        /// </summary>
        public string[]? dataFilter { get; set; }

        /// <summary>
        /// Các cột trong bảng
        /// </summary>
        public string[]? columns { get; set; }
    }
}
