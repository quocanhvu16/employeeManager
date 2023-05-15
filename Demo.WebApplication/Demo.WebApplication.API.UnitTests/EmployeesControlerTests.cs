//using Demo.WebApplication.API.Controllers;
//using Demo.WebApplication.Common;
//using Demo.WebApplication.Repository;
//using Demo.WebApplication.Service;
//using Microsoft.AspNetCore.Mvc;
//using MySqlConnector;
//using NSubstitute;
//using System.Data;

//namespace Demo.WebApplication.API.UnitTests
//{
//    [TestFixture]
//    public class EmployeesControlerTests
//    {
//        /// <summary>
//        /// Hàm Test API lấy thông tin nhân viên theo id trả về Code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void GetEmployeeById_EmployeeId_ReturnsOk()
//        {
//            //Arrange

//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");

//            var employee = new Employee()
//            {
//                EmployeeId = employeeId,
//                EmployeeCode = "NV-744930",
//                FullName = "Dương Gia Cần"
//            };

//            var employeeService = Substitute.For<IEmployeeService>();
//            employeeService.GetEmployeeById(Arg.Any<Guid>())  
//                .Returns(new Employee()
//                {
//                    EmployeeId = employeeId,
//                    EmployeeCode = "NV-744930",
//                    FullName = "Dương Gia Cần"
//                });
//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(employee);
//            expectedResult.StatusCode = 200;

//            //Act
//            var actualResult = (ObjectResult)employeesController.GetEmployeeById(employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(((Employee)expectedResult.Value).EmployeeId, Is.EqualTo(((Employee)actualResult.Value).EmployeeId));
//            Assert.That(((Employee)expectedResult.Value).EmployeeCode, Is.EqualTo(((Employee)actualResult.Value).EmployeeCode));
//            Assert.That(((Employee)expectedResult.Value).FullName, Is.EqualTo(((Employee)actualResult.Value).FullName));
//        }

//        /// <summary>
//        /// Hàm Test API lấy thông tin nhân viên theo id trả về Code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh ( 15/04/2023 )
//        [Test]
//        public void GetEmployeeById_EmployeeId_ReturnsNotFound()
//        {
//            //Arrange
//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");

//            var error = new ErrorResult
//            {
//                UserMsg = Resource.Not_Found
//            };

//            var employeeService = Substitute.For<IEmployeeService>();
//            employeeService.GetEmployeeById(
//                Arg.Any<Guid>())
//                .Returns((Employee)null);
//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(error);
//            expectedResult.StatusCode = 404;

//            //Act
//            var actualResult = (ObjectResult)employeesController.GetEmployeeById(employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(((ErrorResult)expectedResult.Value).UserMsg, Is.EqualTo(((ErrorResult)actualResult.Value).UserMsg));
//        }

//        /// <summary>
//        /// Hàm test API lấy mã nhân viên mới và trả về Code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void GetNewEmployeeCode_ReturnsOk()
//        {
//            //Arrange
//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.GetNewEmployeeCode().Returns("NV-000001");

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult("NV-000001");
//            expectedResult.StatusCode = 200;

//            //Act
//            var actualResult = (ObjectResult)employeesController.GetNewEmployeeCode();

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }

//        /// <summary>
//        /// Hàm test API lấy mã nhân viên mới và trả về Code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void GetNewEmployeeCode_ReturnsNotFound()
//        {
//            //Arrange
//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.GetNewEmployeeCode().Returns("");

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new StatusCodeResult(404);

//            //Act
//            var actualResult = (StatusCodeResult)employeesController.GetNewEmployeeCode();

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//        }

//        /// <summary>
//        /// Hàm test API xóa 1 nhân viên theo ID trả về code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void DeleteEmployeeById_ReturnsOk()
//        {
//            //Arrange
//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");

//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.DeleteEmployeeById(Arg.Any<Guid>()).Returns(1);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(1);
//            expectedResult.StatusCode = 200;

//            //Act
//            var actualResult = (ObjectResult)employeesController.DeleteEmployeeById(employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));

//        }

//        /// <summary>
//        /// Hàm test API xóa 1 nhân viên theo ID trả về code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void DeleteEmployeeById_ReturnsNotFound()
//        {
//            //Arrange
//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");

//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.DeleteEmployeeById(Arg.Any<Guid>()).Returns(0);

//            var employeesController = new EmployeesController(employeeService);

//            var error = new ErrorResult
//            {
//                UserMsg = Resource.Not_Found
//            };

//            var expectedResult = new ObjectResult(error);
//            expectedResult.StatusCode = 404;

//            //Act
//            var actualResult = (ObjectResult)employeesController.DeleteEmployeeById(employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(((ErrorResult)expectedResult.Value).UserMsg, Is.EqualTo(((ErrorResult)actualResult.Value).UserMsg));
//        }

//        /// <summary>
//        /// Hàm test API xóa nhiều nhân viên theo ID trả về code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void DeleteMultipleEmployeeById_ReturnsOk()
//        {
//            //Arrange
//            Guid employeeId1 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");
//            Guid employeeId2 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32633");
//            Guid employeeId3 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32634");
//            List<Guid> employeeIds = new() { employeeId1, employeeId2, employeeId3 };

//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.DeleteMultipleEmployeeById(Arg.Any<List<Guid>>()).Returns(3);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(3);
//            expectedResult.StatusCode = 200;

//            //Act
//            var actualResult = (ObjectResult)employeesController.DeleteMultipleEmployeeById(employeeIds);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }

//        /// <summary>
//        /// Hàm test API xóa nhiều nhân viên theo ID trả về code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void DeleteMultipleEmployeeById_ReturnsNotFound()
//        {
//            //Arrange
//            Guid employeeId1 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");
//            Guid employeeId2 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32633");
//            Guid employeeId3 = new Guid("ff491991-e3d5-4916-a081-cce5d0d32634");
//            List<Guid> employeeIds = new() { employeeId1, employeeId2, employeeId3 };

//            var employeeService = Substitute.For<IEmployeeService>();

//            employeeService.DeleteMultipleEmployeeById(Arg.Any<List<Guid>>()).Returns(2);

//            var employeesController = new EmployeesController(employeeService);

//            var error = new ErrorResult
//            {
//                UserMsg = Resource.Not_Found
//            };

//            var expectedResult = new ObjectResult(error);
//            expectedResult.StatusCode = 404;

//            //Act
//            var actualResult = (ObjectResult)employeesController.DeleteMultipleEmployeeById(employeeIds);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(((ErrorResult)expectedResult.Value).UserMsg, Is.EqualTo(((ErrorResult)actualResult.Value).UserMsg));
//        }

//        /// <summary>
//        /// Hàm test API thêm nhân viên trả về code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void InsertEmployee_ReturnsOk()
//        {
//            //Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632"),
//                EmployeeCode = "NV-744930",
//                FullName = "Dương Gia Cần",
//                DepartmentId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632")
//            };

//            var employeeService = Substitute.For<IEmployeeService>();

//            var serviceResult = new ServiceResult()
//            {
//                result = 1
//            };

//            employeeService.InsertEmployee(Arg.Any<Employee>()).Returns(serviceResult);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(1);
//            expectedResult.StatusCode = 201;

//            //Act
//            var actualResult = (ObjectResult)employeesController.InsertEmployee(employee);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }

//        /// <summary>
//        /// Hàm test API thêm nhân viên trả về code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void InsertEmployee_ReturnsInValid()
//        {
//            //Arrange
//            var employee = new Employee()
//            {
//                EmployeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632"),
//                EmployeeCode = "NV-744930",
//                FullName = "Dương Gia Cần",
//            };

//            var employeeService = Substitute.For<IEmployeeService>();

//            var errorData = new Dictionary<string, string>();
//            errorData.Add("departmentId", Resource.Validate_DepartmentIdEmpty);
//            errorData.Add("employeeCode", Resource.Validate_EmployeeCodeDuplicate);
//            var error = new ErrorResult
//            {
//                UserMsg = Resource.Validate_UserMsg,
//                Data = errorData
//            };

//            var serviceResult = new ServiceResult()
//            {
//                error = error
//            };

//            employeeService.InsertEmployee(Arg.Any<Employee>()).Returns(serviceResult);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(error);
//            expectedResult.StatusCode = 400;

//            //Act
//            var actualResult = (ObjectResult)employeesController.InsertEmployee(employee);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }

//        /// <summary>
//        /// Hàm test API sửa nhân viên trả về code 200
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void UpdateEmployee_ReturnsOk()
//        {
//            //Arrange
//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");
//            var employee = new Employee()
//            {
//                EmployeeId = employeeId,
//                EmployeeCode = "NV-744930",
//                FullName = "Dương Gia Cần",
//                DepartmentId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632")
//            };

//            var employeeService = Substitute.For<IEmployeeService>();

//            var serviceResult = new ServiceResult()
//            {
//                result = 1
//            };

//            employeeService.UpdateEmployee(Arg.Any<Employee>(), Arg.Any<Guid>()).Returns(serviceResult);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(1);
//            expectedResult.StatusCode = 200;

//            //Act
//            var actualResult = (ObjectResult)employeesController.UpdateEmployee(employee, employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }

//        /// <summary>
//        /// Hàm test API sửa nhân viên trả về code 404
//        /// </summary>
//        /// Author: Vũ Quốc Anh (15/04/2023)
//        [Test]
//        public void UpdateEmployee_ReturnsInValid()
//        {
//            //Arrange

//            var employeeId = new Guid("ff491991-e3d5-4916-a081-cce5d0d32632");
//            var employee = new Employee()
//            {
//                EmployeeId = employeeId,
//                EmployeeCode = "NV-744930",
//            };

//            var employeeService = Substitute.For<IEmployeeService>();

//            var errorData = new Dictionary<string, string>();
//            errorData.Add("departmentId", Resource.Validate_DepartmentIdEmpty);
//            errorData.Add("employeeCode", Resource.Validate_EmployeeCodeDuplicate);
//            errorData.Add("fullName", Resource.Validate_FullNameEmpty);

//            var error = new ErrorResult
//            {
//                UserMsg = Resource.Validate_UserMsg,
//                Data = errorData
//            };

//            var serviceResult = new ServiceResult()
//            {
//                error = error
//            };

//            employeeService.UpdateEmployee(Arg.Any<Employee>(), Arg.Any<Guid>()).Returns(serviceResult);

//            var employeesController = new EmployeesController(employeeService);

//            var expectedResult = new ObjectResult(error);
//            expectedResult.StatusCode = 400;

//            //Act
//            var actualResult = (ObjectResult)employeesController.UpdateEmployee(employee, employeeId);

//            //Assert
//            Assert.That(expectedResult.StatusCode, Is.EqualTo(actualResult.StatusCode));
//            Assert.That(expectedResult.Value, Is.EqualTo(actualResult.Value));
//        }
//    }
//}
