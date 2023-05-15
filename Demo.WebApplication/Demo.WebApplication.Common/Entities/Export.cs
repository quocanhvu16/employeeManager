namespace Demo.WebApplication.Common
{
    public class Export
    {
        /// <summary>
        /// Số lượng bản ghi 1 trang
        /// </summary>
        public int pageSize { get; set; }

        public int pageNumber { get; set; }

        public string keyword { get; set; }

        public object dataColumns { get; set; }

    }
}
