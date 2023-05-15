import MISAResource from './base/MISAResource.js';
const langCode = "VI";

const form = {
    /**
     * Hàm kiểm tra xem employeeCode có rỗng không
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    async validateEmployeeCOde(employeeCode){
        try {
            if(employeeCode){
                if(employeeCode.trim() == ""){
                    return `${MISAResource[langCode].EmployeeCodeEmpty}`
                }
                else{
                    return ""
                }
            }
            else{
                return `${MISAResource[langCode].EmployeeCodeEmpty}`
            }
        } catch (error) {
            console.log(error);
        }
    },
    
    /**
     * Hàm kiểm tra họ tên
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    validateName(name){
        try {
            //Nếu input rỗng
            if(!name){
                return `${MISAResource[langCode].FullNameEmpty}`;
            }
            else{
                if(name.trim()==""){
                    return `${MISAResource[langCode].FullNameEmpty}`;
                }
                else{
                    const pattern = /[^a-zA-Z\s]/g;
                    //Nếu input không đúng định dạng
                    if(pattern.test(this.removeVietnameseTones(name))){
                        return `${MISAResource[langCode].FullNameMalformed}`;
                    }
                    return ""
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Hàm kiểm tra ngày tháng sinh nhật
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    validateBirthday(birthday){
        try {
            const birthDay = new Date(birthday)
            let date = new Date()
            if(birthDay.getTime() > date.getTime()){
                return `${MISAResource[langCode].BitrhdayWrong}`;
            }
            return ""
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * hàm check xem trường đơn vị được chọn chưa
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    validateDepartment(department){
        try {
            if(department == "" || department == undefined || department == null){
                return `${MISAResource[langCode].DepartmentEmpty}`
            }
            if(department != ""){
                return ""
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Hàm validate identityNumber
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    validateIdentityNumber(identityNumber){
        try {
            if(!identityNumber || identityNumber == ""){
                return ""
            }
            else{
                const regex = /^\d+$/
                if (regex.test(identityNumber)){
                    return ""
                }
                else{
                    return `${MISAResource[langCode].IdentityMalformed}`
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Hàm validate PhoneNumber
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    validatePhoneNumber(phoneNumber){
        try {
            if(!phoneNumber || phoneNumber == ""){
                return ""
            }
            else{
                const regex = /^\d+$/
                if (regex.test(phoneNumber)){
                    return ""
                }
                else{
                    return `${MISAResource[langCode].PhoneNumberMalformed}`
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

     /**
     * Hàm validate Salary
     * Author: Vũ Quốc Anh (19/03/2023)
     */
     validateSalary(salary){
        try {
            if(!salary || salary == ""){
                return ""
            }
            else{
                const regex = /^\d+$/
                if (regex.test(salary)){
                    return ""
                }
                else{
                    return `${MISAResource[langCode].SalaryMalformed}`
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

     /**
     * Hàm validate Email
     * Author: Vũ Quốc Anh (19/03/2023)
     */
     validateEmail(email){
        try {
            if(!email || email == ""){
                return ""
            }
            else{
                const regex = /^[\w-.]+@([\w-]+\.)+[\w-]{2,}$/;
                if (regex.test(email)){
                    return ""
                }
                else{
                    return `${MISAResource[langCode].EmailMalformed}`
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Hàm xuất hiện toast khi thêm nhân viên thành công
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    showToast(text,type){
        try {
            const main= document.getElementById('toast');
            if(main){
                const toast = document.createElement('div');
                if(type == 0){
                    toast.classList.add('toast','toast--success');
                    toast.innerHTML = `
                        <div class="toast__icon">
                            <i class="fa-solid fa-circle-check"></i>
                        </div>
                        <div class="toast__body">
                            <p><span>Thành công!</span> ${text}</p>
                        </div>
                        <div class="toast__action">
                            
                        </div>
                        <div class="toast__close">
                            <i class="fa-solid fa-xmark"></i>
                        </div> 
                    `;
                }
                if(type == 1){
                    toast.classList.add('toast','toast--error');
                    toast.innerHTML = `
                        <div class="toast__icon">
                            <i class="fa-solid fa-circle-xmark"></i>
                        </div>
                        <div class="toast__body">
                            <p><span>Lỗi!</span> ${text}</u></p>
                        </div>
                        <div class="toast__action">
            
                        </div>
                        <div class="toast__close">
                            <i class="fa-solid fa-xmark"></i>
                        </div>
                    `;
                }
                main.appendChild(toast)
                const removeId = setTimeout(()=>{
                    main.removeChild(toast)
                },6000)
                //Set sự kiên khi ấn vào nút close để tắt toast
                toast.onclick = (e) => {
                    if(e.target.closest('.toast__close')){
                        main.removeChild(toast)
                        clearTimeout(removeId)
                    }
                }
            }
        } catch (error) {
            console.log(error);
        }
    },

    /**
     * Hàm chuyển đổi chữ có dấu thành không dấu
     * Author: Vũ Quốc Anh (19/03/2023)
     */
    removeVietnameseTones(str) {
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
        str = str.replace(/đ/g, 'd');
        str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, 'A');
        str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, 'E');
        str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, 'I');
        str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, 'O');
        str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, 'U');
        str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, 'Y');
        str = str.replace(/Đ/g, 'D');
        // Some system encode vietnamese combining accent as individual utf-8 characters
        // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
        str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ''); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
        str = str.replace(/\u02C6|\u0306|\u031B/g, ''); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
        // Remove extra spaces
        // Bỏ các khoảng trắng liền nhau
        str = str.replace(/ + /g, ' ');                     
        str = str.trim();
        // Remove punctuations
        // Bỏ dấu câu, kí tự đặc biệt
        // str = str.replace(
        //   /!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g,
        //   ' ',
        // );
        return str;
    }
}


export default form;
