using Demo.WebApplication.Common;
using Demo.WebApplication.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.WebApplication.API
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private readonly IBaseService<T> _baseService;

        #endregion

        #region Constructor
        public BasesController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }

        #endregion

        #region API

        /// <summary>
        /// Lấy thông tin tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var record = _baseService.GetAll();

                ///Xử lý kết quả trả về
                ///Thành công
                if (record != null)
                {
                    return StatusCode(200, record);
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
        /// API lấy thông tin chi tiết 1 bản ghi
        /// </summary>
        /// <param name="recordId">ID bản ghi muốn lấy</param>
        /// <returns>Đối tượng bản ghi</returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpGet("{recordId}")]
        public IActionResult GetRecordById([FromRoute] Guid recordId)
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var record = _baseService.GetRecordById(recordId);

                //Xử lý kết quả trả về
                //Thành công
                if (record != null)
                {
                    return StatusCode(200, record);
                }
                //Thất bại
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
        /// API Xóa nhân viên theo id bản ghi truyền vào
        /// </summary>
        /// <param name="recordId">Id bản ghi cần xóa</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023
        [HttpDelete("{recordId}")]
        public IActionResult DeleteRecordById([FromRoute] Guid recordId)
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var number = _baseService.DeleteRecordById(recordId);

                //Xử lý kết quả trả về
                //Thành công
                if (number == 1)
                {
                    return StatusCode(200, number);
                }
                //Thất bại
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
        /// API Xóa nhiều bản ghi được chọn
        /// </summary>
        /// <param name="recordIds">ID những bản ghi cần xóa</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpDelete]
        public IActionResult DeleteMultipleRecordById([FromBody] List<Guid> recordIds)
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var number = _baseService.DeleteMultipleRecordByIds(recordIds);

                //Xử lý kết quả trả về
                //Thành công
                if (number == recordIds.Count)
                {
                    return StatusCode(200, number);
                }
                //Thất bại
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
        /// Bắt exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        protected IActionResult HandleException(Exception ex)
        {
            ///Khởi tạo exception
            var error = new ErrorResult()
            {
                UserMsg = Resource.UserMsg_Exception,
                DevMsg = ex.Message,
                Data = ex.Data,
                ErrorCode = Error.Exception
            };

            ///Kết quả trả về cho client
            var controllerResult = new ControllerResult()
            {
                Success = false,
                Result = error
            };
            return StatusCode(500, controllerResult);
        }

        /// <summary>
        /// API thêm nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên cần thêm</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var res = _baseService.InsertRecord(record);

                ///Xử lý kết quả trả về
                ///Thất bại
                if (res.error != null)
                {
                    res.error.ErrorCode = Error.Validate;
                    return StatusCode(201, new ControllerResult()
                    {
                        Success = false,
                        Result = res.error
                    });
                }
                ///Thành công
                else
                {
                    return StatusCode(201, new ControllerResult()
                    {
                        Success = true,
                        Result = res.result
                    });
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Sửa thông tin bản ghi
        /// </summary>
        /// <param name="employee">Thông tin bản ghi muốn sửa</param>
        /// <param name="employeeId">Id bản ghi muốn sửa</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        [HttpPut("{recordId}")]
        public IActionResult UpdateRecord([FromBody] T record, [FromRoute] Guid recordId)
        {
            try
            {
                ///Nhận kết quả trả về từ BL
                var res = _baseService.UpdateRecord(record, recordId);

                ///Có lỗi
                if (res.error != null)
                {
                    res.error.ErrorCode = Error.Validate;
                    return StatusCode(200, new ControllerResult()
                    {
                        Success = false,
                        Result = res.error
                    });
                }
                ///Thành công
                else
                {
                    if (res.result > 0)
                    {
                        return StatusCode(200, new ControllerResult()
                        {
                            Success = true,
                            Result = res.result
                        });
                    }
                    else
                    {
                        var error = new ErrorResult()
                        {
                            UserMsg = Resource.UserMsg_NotFound,
                            DevMsg = Resource.DevMsg_NotFound,
                        };
                        return StatusCode(404, new ControllerResult()
                        {
                            Success = true,
                            Result = error
                        });
                    }
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
