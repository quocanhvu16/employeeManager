using Demo.WebApplication.Common;
using Demo.WebApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Demo.WebApplication.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        #region Field
        private readonly IBaseRepository<T> _baseRepository;
        #endregion

        #region Method
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteMultipleRecordByIds(List<Guid> ids)
        {
            try
            {
                var transaction = _baseRepository.GetTransaction();

                var number = _baseRepository.DeleteMultipleRecord(transaction, ids);

                return number;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public int DeleteRecordById(Guid id)
        {
            try
            {
                var number = _baseRepository.DeleteRecordById(id);

                return number;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Lấy thông tin tất cả các bản ghi
        /// </summary>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public List<T> GetAll()
        {
            try
            {
                var position = _baseRepository.GetAll();
                return position;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();

            }
        }

        /// <summary>
        /// Lấy thông tin của 1 bản ghi theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public T GetRecordById(Guid id)
        {
            try
            {
                //var cnn = _baseRepository.GetOpenConnection();

                var record = _baseRepository.GetRecordById(id);

                return record;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Thêm 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public ServiceResult InsertRecord(T record)
        {
            try
            {
                //Khai báo lỗi
                var error = new ErrorResult();
                //Validate dữ liệu
                var errorValidate = Validate(record);
                errorValidate = ValidatePrivate(record , errorValidate);
                if (errorValidate.Count > 0)
                {
                    error.UserMsg = Resource.UserMsg_Validate;
                    error.DevMsg = Resource.DevMsg_Validate;
                    error.Data = errorValidate;
                    return new ServiceResult()
                    {
                        error = error
                    };
                }

                var res = _baseRepository.InsertRecord(record);

                return new ServiceResult()
                {
                    result = res
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Sửa 1 bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (19/04/2023)
        public ServiceResult UpdateRecord(T record , Guid id)
        {
            try
            {
                //Khai báo lỗi
                var error = new ErrorResult();
                //Validate dữ liệu
                var errorValidate = Validate(record);
                errorValidate = ValidatePrivate(record, errorValidate);
                if (errorValidate.Count > 0)
                {
                    error.UserMsg = Resource.UserMsg_Validate;
                    error.Data = errorValidate;
                    return new ServiceResult()
                    {
                        error = error
                    };
                }

                var res = _baseRepository.UpdateRecord(record, id);

                return new ServiceResult()
                {
                    result = res
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Validate dữ liệu cho những thành phần có đặc điểm chung
        /// </summary>
        /// <param name="record">Bản ghi caanfa validate</param>
        /// <returns></returns>
        /// Author: Vũ Quốc Anh (29/04/2023)
        /// <exception cref="NotImplementedException"></exception>
        public Dictionary<string, string> Validate(T record)
        {
            try
            {
                //Khai báo lỗi
                var errorData = new Dictionary<string, string>();

                var props = record.GetType().GetProperties();
                foreach (var prop in props)
                {
                    //Lấy các thông tin như name và value của từng thành phần
                    var propValue = prop.GetValue(record);
                    var propName = prop.Name;
                    var displayName = string.Empty;
                    var propertyName = prop.GetCustomAttributes(typeof(PropertyName), true);
                    if (propertyName.Length > 0)
                    {
                        displayName = (propertyName[0] as PropertyName).Name;
                    }

                    //Validate lỗi không được để trống
                    var isDefineEmpties = prop.IsDefined(typeof(NotEmpty), true);
                    if (isDefineEmpties)
                    {
                        if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                        {
                            errorData.Add(propName, $"{displayName} không được để trống");
                        }
                    }

                    //Validate lỗi thời gian không đúng định dạng
                    var isDefineTimeMalformed = prop.IsDefined(typeof(TimeMalformed), true);
                    if (isDefineTimeMalformed)
                    {
                        if (propValue != null && (DateTime)propValue > DateTime.Now)
                        {
                            errorData.Add(propName, Resource.Validate_TimeMalformed);
                        }
                    }

                    //Validate lỗi không quá 20 ký tự
                    var isDefineLength20 = prop.IsDefined(typeof(Length20), true);
                    if (isDefineLength20)
                    {
                        if (propValue != null && propValue.ToString().Length > 20)
                        {
                            errorData.Add(propName, Resource.Validate_Length20);
                        }
                    }

                    //Validate lỗi không quá 25 ký tự
                    var isDefineLength25 = prop.IsDefined(typeof(Length25), true);
                    if (isDefineLength25)
                    {
                        if (propValue != null && propValue.ToString().Length > 25)
                        {
                            errorData.Add(propName, Resource.Validate_Length25);
                        }
                    }

                    //Validate lỗi không quá 50 ký tự
                    var isDefineLength50 = prop.IsDefined(typeof(Length50), true);
                    if (isDefineLength50)
                    {
                        if (propValue != null && propValue.ToString().Length > 50)
                        {
                            errorData.Add(propName, Resource.Validate_Length50);
                        }
                    }

                    //Validate lỗi không quá 100 ký tự
                    var isDefineLength100 = prop.IsDefined(typeof(Length100), true);
                    if (isDefineLength100)
                    {
                        if (propValue != null && propValue.ToString().Length > 100)
                        {
                            errorData.Add(propName, Resource.Validate_Length100);
                        }
                    }

                    //Validate lỗi không quá 255 ký tự
                    var isDefineLength255 = prop.IsDefined(typeof(Length255), true);
                    if (isDefineLength255)
                    {
                        if (propValue != null && propValue.ToString().Length > 255)
                        {
                            errorData.Add(propName, Resource.Validate_Length255);
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

        /// <summary>
        /// Validate cho 1 số trường đặc biệt chỉ có ở một vài tình huống
        /// </summary>
        /// <param name="record"></param>
        /// <param name="errorData">Các lỗi khi đã validate cho những thành phần chung</param>
        /// Author: Vũ Quốc Anh (29/04/2023)
        /// <returns></returns>
        protected virtual Dictionary<string, string> ValidatePrivate(T record , Dictionary<string, string> errorData )
        {
            return errorData;
        }

        #endregion
    }
}
