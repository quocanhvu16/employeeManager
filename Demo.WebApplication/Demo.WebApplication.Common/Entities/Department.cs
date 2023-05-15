namespace Demo.WebApplication.Common
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã phòng ban")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên phòng ban")]
        public string DepartmentName { get; set;}
    }
}
