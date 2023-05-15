using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Common
{
    public class ControllerResult
    {
        /// <summary>
        /// Kết quả trả về xem call API thành công hay không
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// Kết quả nhận được
        /// </summary>
        public object? Result { get; set; }
    }
}
