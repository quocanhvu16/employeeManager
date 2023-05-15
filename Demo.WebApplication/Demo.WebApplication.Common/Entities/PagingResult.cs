namespace Demo.WebApplication.Common
{
    public class PagingResult
    {

        /// <summary>
        /// Các bản ghi trả về
        /// </summary>
        public List<Employee> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi thỏa mãn điều kiện
        /// </summary>
        public int TotalRecord { get; set; }
    }
}
