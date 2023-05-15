using ClosedXML.Excel;
using Demo.WebApplication.Common;
using Demo.WebApplication.Repository;
using Demo.WebApplication.Service;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Demo.WebApplication.API
{
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private readonly IEmployeeService _employeeService; 
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        #endregion

        #region API

        /// <summary>
        /// API xuất ra file excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        [HttpPost("export")]
        public IActionResult Export([FromBody] Filter filter)
        {
            try
            {
                ///Nhận kết quả trả về
                var result = _employeeService.ExportExcel(filter);

                ///Xử lý kết quả trả về
                ///Thành công
                if (result != null)
                {
                    return File(result, "application/octet-stream", "Danh_Sach_Nhan_Vien.xlsx");
                }
                ///Thất bại
                else
                {
                    return StatusCode(404, new ErrorResult
                    {
                        UserMsg = Resource.UserMsg_NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        ErrorCode = Error.resourceNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang hiện tại</param>
        /// <param name="keyword">Key tìm kiếm</param>
        /// <param name="departmentId">Tìm theo id phòng ban</param>
        /// <param name="posititionId">Tìm theo id vị trí</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpPost("paging")]
        public IActionResult GetPaging([FromBody] Filter filter)
        {
            try
            {
                //Nhận kết quả trả về
                var result = _employeeService.GetPaging(filter);

                //Xử lý kết quả trả về
                //Thành công
                if (result != null)
                {
                    return Ok(result);
                }
                //Thất bại
                else
                {
                    return NotFound(new ErrorResult
                    {
                        UserMsg = Resource.UserMsg_NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        ErrorCode = Error.resourceNotFound
                    });
                }
            }

            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// API Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên tự động tăng thêm 1 đơn vị với mã lớn nhất</returns>
        /// Author : Vũ Quốc Anh (04/04/2023)
        [HttpGet("newEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeService.GetNewEmployeeCode();

                //B4:Xử lý kết quả trả về
                //4.1.Thành công
                if (newEmployeeCode != null && newEmployeeCode != "")
                {
                    return StatusCode(200, newEmployeeCode);
                }
                //4.2.Thất bại
                else
                {
                    return StatusCode(404, new ErrorResult
                    {
                        UserMsg = Resource.UserMsg_NotFound,
                        DevMsg = Resource.DevMsg_NotFound,
                        ErrorCode = Error.resourceNotFound
                    });
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        #endregion
    }
}
