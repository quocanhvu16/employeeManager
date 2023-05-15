namespace Demo.WebApplication.Common
{
    public class ErrorResult
    {
        /// <summary>
        /// Thông báo trả về cho Dev
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông báo trả về cho user
        /// </summary>
        public string? UserMsg { get; set; }
     
        /// <summary>
        /// Thông báo về lỗi khi gặp exception
        /// </summary>
        public object? Data { get; set; }

        /// <summary>
        /// Mã lỗi khi có lỗi xảy ra
        /// </summary>
        public Error? ErrorCode { get; set; }
    }
}
