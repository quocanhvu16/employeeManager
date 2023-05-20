using ClosedXML.Excel;
using Demo.WebApplication.Common;
using Demo.WebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApplication.Service
{
    public class EmployeeService : BaseService<Employee>,IEmployeeService
    {
        #region Field

        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository) 
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

        #region Method

        /// <summary>
        /// Hàm lấy các dữ liệu đã được phân trang và filter
        /// </summary>
        /// <param name="filter">Các filter được gửi từ client</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (13/04/2023)
        public PagingResult GetPaging(Filter filter)
        {
            try
            {
                var result = _employeeRepository.GetPaging(filter);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Ảuthor: Vũ Quốc Anh (13/04/2023)
        public string GetNewEmployeeCode()
        {
            try
            {
                var newEmployeeCode = _employeeRepository.GetNewEmployeeCode();

                return newEmployeeCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm tạo file excel và thiết kế file excel
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (18/04/2023)
        /// <exception cref="NotImplementedException"></exception>
        public byte[] ExportExcel(Filter filter)
        {
            try
            {
                var result = _employeeRepository.ExportExcel(filter);

                var wb = new XLWorkbook();
                var ws = wb.Worksheets.Add("Danh sách nhân viên");
                ws.Cell("A1").Value = "Danh sách nhân viên";


                ws.Cell("A3").Value = "STT";
                char myChar = (char)65;
                int row=4;
                for (int i = 0; i < filter.columns.Length; i++)
                {
                    row = 4;
                    myChar++;
                    for (int b = 0; b < result.Count; b++)
                    {
                        var formatMoney = "";
                        if (result[b].Salary != null)
                        {
                            formatMoney = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00} VNĐ", result[b].Salary);
                        }
                        var formatGender = "";
                        if (result[b].Gender == Gender.Male)
                        {
                            formatGender = "Nữ";
                        }
                        else if (result[b].Gender == Gender.Female)
                        {
                            formatGender = "Nam";
                        }
                        else
                        {
                            formatGender = "Không xác định";
                        }
                        ws.Cell("A" + row).Value = b + 1;
                        switch (filter.columns[i])
                        {
                            case "employeeCode":
                                ws.Cell(myChar + "3").Value = "Mã nhân viên";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].EmployeeCode;
                                break;
                            case "fullName":
                                ws.Cell(myChar + "3").Value = "Tên nhân viên";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].FullName;
                                break;
                            case "gender":
                                ws.Cell(myChar + "3").Value = "Giới tính";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = formatGender;
                                break;
                            case "dateOfBirth":
                                ws.Cell(myChar + "3").Value = "Ngày sinh";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].DateOfBirth;
                                ws.Range(myChar.ToString() + "4:" + myChar.ToString() + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                break;
                            case "identityNumber":
                                ws.Cell(myChar + "3").Value = "Số CMND";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].IdentityNumber;
                                break;
                            case "identityDate":
                                ws.Cell(myChar + "3").Value = "Ngày cấp CMND";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].IdentityDate;
                                ws.Range(myChar.ToString() + "4:" + myChar.ToString() + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                break;
                            case "identityPlace":
                                ws.Cell(myChar + "3").Value = "Nơi cấp CMND";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].IdentityPlace;
                                break;
                            case "position":
                                ws.Cell(myChar + "3").Value = "Chức danh";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].PositionName;
                                break;
                            case "department":
                                ws.Cell(myChar + "3").Value = "Đơn vị";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].DepartmentName;
                                break;
                            case "phoneNumber":
                                ws.Cell(myChar + "3").Value = "Số điện thoại";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].PhoneNumber;
                                break;
                            case "salary":
                                ws.Cell(myChar + "3").Value = "Lương";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = formatMoney;
                                ws.Range(myChar.ToString() + "4:" + myChar.ToString() + row).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                break;
                            case "email":
                                ws.Cell(myChar + "3").Value = "Email";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].Email;
                                break;
                            case "address":
                                ws.Cell(myChar + "3").Value = "Địa chỉ";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].Address;
                                break;
                            case "bankId":
                                ws.Cell(myChar + "3").Value = "Số tài khoản";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].BankId;
                                break;
                            case "bankName":
                                ws.Cell(myChar + "3").Value = "Tên ngân hàng";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].BankName;
                                break;
                            case "bankAddress":
                                ws.Cell(myChar + "3").Value = "Chi nhánh TK ngân hàng";
                                ws.Cell(myChar.ToString() + row.ToString()).Value = result[b].BankAddress;
                                break;
                            default:
                                break;
                        }
                        row++;
                    }
                }

                var rngHeaders = ws.Range("A1:" + myChar.ToString() + "1");
                rngHeaders.Merge();
                ws.Row(1).Height = 21;
                ws.Row(1).Style.Font.FontSize = 16;
                ws.Row(1).Style.Font.FontName = "Arial";
                rngHeaders.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                rngHeaders.Style.Font.Bold = true;

                ws.Range("A2:" + myChar.ToString() + "2").Merge();
                ws.Row(2).Height = 21;

                var rngTable = ws.Range("A3:" +myChar.ToString() + (row-1));
                rngTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;

                rngTable.Row(1).Style.Font.FontSize = 10;
                rngTable.Row(1).Style.Font.Bold = true;
                rngTable.Row(1).Style.Fill.BackgroundColor = XLColor.LightGray;
                rngTable.Row(1).Style.Font.FontName = "Arial";
                rngTable.Row(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                rngTable.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                rngTable.Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                ws.Columns(1, row).AdjustToContents();
              
                string fileName = "./Resource/Danh_Sach_Nhan_Vien_" + DateTime.Now.Ticks + ".xlsx";
                wb.SaveAs(fileName);
                string pathToFile = fileName;
                var byets = System.IO.File.ReadAllBytes(pathToFile);

                System.IO.File.Delete(fileName);
                return byets;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Check email đúng định dạng
        /// </summary>
        /// <param name="email">Email cần check</param>
        /// <returns>True nếu đúng định dạng , false nếu sai định dạng</returns>
        /// Author: Vũ Quốc Anh (04/04/2023)
        public bool IsValidEmail(string? email)
        {
            try
            {
                //B1: Xóa các khoảng trắng ở cuối và đầu
                var trimmedEmail = email.Trim();

                //B2: Validate
                if (trimmedEmail.EndsWith("."))
                {
                    return false;
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == trimmedEmail;
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Hàm validate ghi đè từ Service
        /// </summary>
        /// <param name="employee">Bản ghi muốn validate</param>
        /// <param name="errorData">Lỗi từ validate chung</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (18/04/2023)
        /// <exception cref="NotImplementedException"></exception>
        protected override Dictionary<string, string> ValidatePrivate(Employee employee, Dictionary<string, string> errorData)
        {
            try
            {
                ///Email không đúng định dạng
                if (!errorData.ContainsKey("Email"))
                {
                    if (employee.Email != null && employee.Email != "" && !IsValidEmail(employee.Email))
                    {
                        errorData.Add("Email", Resource.Validate_EmailMalformed);
                    }
                }

                ///Không được để trùng
                var props = employee.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var propValue = prop.GetValue(employee);
                    var propName = prop.Name;
                    var isDefineNotDuplicate = prop.IsDefined(typeof(NotDuplicate), true);
                    if (isDefineNotDuplicate)
                    {
                        if (!errorData.ContainsKey(propName))
                        {
                            CheckDuplicate checkMode = CheckDuplicate.EmployeeCode;
                            switch (propName)
                            {
                                case "EmployeeCode":
                                    checkMode = CheckDuplicate.EmployeeCode;
                                    break;
                                case "IdentityNumber":
                                    checkMode = CheckDuplicate.IdentityNumber;
                                    break;
                            }
                            var displayName = string.Empty;
                            var propertyName = prop.GetCustomAttributes(typeof(PropertyName), true);
                            if (propertyName.Length > 0)
                            {
                                displayName = (propertyName[0] as PropertyName).Name;
                            }
                            if (_employeeRepository.CheckDuplicateCode((string)propValue, employee.EmployeeId, checkMode))
                            {
                                errorData.Add(propName, displayName + Resource.Validate_CheckDuplicate);
                            }
                        }
                    }
                }
                return errorData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
