namespace Demo.WebApplication.Common
{
    public class ServiceResult
    {
        /// <summary>
        /// Kết quả trả về nếu không có lỗi
        /// </summary>
        public int? result { get; set; }

        /// <summary>
        /// Nếu có lỗi sẽ trả về lỗi
        /// </summary>
        public ErrorResult? error { get; set; }

    }
}
