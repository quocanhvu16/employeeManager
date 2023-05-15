namespace Demo.WebApplication.Common
{
    public class Position : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Mã chức danh
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã chức danh")]
        public string PositionCode { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên chức danh")]
        public string PositionName { get; set; }

    }
}
