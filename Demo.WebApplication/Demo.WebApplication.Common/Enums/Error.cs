using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    public enum Error
    {
        /// <summary>
        /// Lỗi exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi khi validate
        /// </summary>
        Validate = 2,  
        
        /// <summary>
        /// Lỗi không tìm thấy tài nguyên
        /// </summary>
        resourceNotFound = 3,

    }
}
