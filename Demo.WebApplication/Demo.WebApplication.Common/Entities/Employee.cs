namespace Demo.WebApplication.Common
{   
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee : BaseEntity
    {
        /// <summary>
        /// ID của nhân viên( Khóa chính)
        /// </summary>
        [PropertyName("ID nhân viên")]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [NotDuplicate]
        [Length20]
        [PropertyName("Mã")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [NotEmpty]
        [Length100]
        [PropertyName("Tên nhân viên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [PropertyName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [PropertyName("Ngày sinh")]
        [TimeMalformed]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [PropertyName("Số căn cước công dân")]
        [NotDuplicate]
        [Length25]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp căn cước công dân
        /// </summary>
        [PropertyName("Ngày cấp căn cước công dân")]
        [TimeMalformed]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp căn cước công dân
        /// </summary>
        [PropertyName("Nơi cấp căn cước công dân")]
        [Length255]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [PropertyName("Địa chỉ")]
        [Length255]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [PropertyName("Số điện thoại")]
        [Length50]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [PropertyName("Email")]
        [Length100]
        public string? Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        [PropertyName("Tài khoản ngân hàng")]
        [Length25]
        public string? BankId { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [PropertyName("Tên ngân hàng")]
        [Length255]
        public string? BankName { get; set; }

        /// <summary>
        /// Địa chỉ chi nhánh ngân hàng
        /// </summary>
        [PropertyName("Chi nhánh ngân hàng")]
        [Length255]
        public string? BankAddress { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        [PropertyName("Lương")]
        public double? Salary { get; set; }

        /// <summary>
        /// ID chức danh
        /// </summary>
        [PropertyName("ID chức danh")]
        public Guid? PositionId { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        [PropertyName("Chức danh")]
        public string? PositionName { get; set; }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        [NotEmpty]
        [PropertyName("ID phòng ban")]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [PropertyName("Tên phòng ban")]
        public string? DepartmentName { get; set; }
    }
}
