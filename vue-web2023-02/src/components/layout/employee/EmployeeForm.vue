<template>
    <MDialog v-if="dialog" :dataDialog="dataDialog" @close-dialog="closeDialog" 
                    @change-noaccept="changeNoAccept" @change-accept="changeAccept"/>
    <div :class="['overlay','active']" >
        <div class="m-popup">
            <div class="m-popup__title">
                <h1 :style="['margin-right: 20px','flex-grow:1']">{{titleForm}}</h1>
                <div class="question tooltip-down" :data-c-tooltip="MISAResource[langCode].tooltip.help">
                    <i class="fa-regular fa-circle-question"></i>
                </div>
                <div class="close tooltip-down" @click="btnCloseOnClick" :data-c-tooltip="MISAResource[langCode].tooltip.exitAndQuickKeys" v-esc="btnCloseOnClick">
                    <i class="fa-solid fa-xmark"></i>
                </div>
            </div>
            <!-- <div class="m-popup__desc">
                <p>{{MISAResource[langCode].form.description}}</p>
            </div> -->
            <div class="row1">
                <MInput v-model="employee.employeeCode" @blur-input="blurInputEmployeeCode"
                    :data="{
                        'class':'m-popup__id require',
                        'textInput':MISAResource[langCode].column.employeeCode,
                        'placeholder':MISAResource[langCode].input.placeholder.employeeCode,
                        'typeInput':'text',
                        'tabindex':1,
                        'ref':'EmployeeCode',
                        'refArray' : this.ref,
                        'error':this.errors.EmployeeCode,
                        'maxLength':20
                    }"
                />
                <MInput v-model="employee.fullName" @blur-input="blurInputFullname"
                    :data="{
                        'class':'m-popup__name require',
                        'textInput':MISAResource[langCode].column.fullName,
                        'placeholder':MISAResource[langCode].input.placeholder.fullName,
                        'typeInput':'text',
                        'tabindex':2,
                        'ref':'FullName',
                        'refArray' : this.ref,
                        'error':this.errors.FullName,
                        'maxLength':100
                    }"
                />
                <!-- <div class="m-popup__birthday">
                    <p>{{MISAResource[langCode].column.dateOfBirth}}</p>
                    <flat-pickr v-model="employee.dateOfBirth" :tabindex=5 :ref='DateOfBirth' :config="config"/>
                    <div class="error__text"><p>{{ this.errors.DateOfBirth }}</p></div>
                </div> -->
                <MInput v-model="employee.dateOfBirth"
                    :data="{
                        'class':'m-popup__birthday',
                        'textInput':MISAResource[langCode].column.dateOfBirth,
                        'typeInput':'date',
                        'tabindex':5,
                        'ref':'DateOfBirth',
                        'refArray' : this.ref,
                        'error':this.errors.DateOfBirth,
                    }"
                />
                <div class="m-popup__gender">
                    <p>{{MISAResource[langCode].column.gender}}</p>
                    <form style="display: flex;align-items: center;height: 36px;">
                        <div style="display: flex;align-items: center;position:relative" v-for="gender in gender" :key="gender.id">
                            <input class="input" type="radio" name="sex" tabindex="6"
                                :value="gender.id" :id="gender.id" :checked="employee.gender===gender.id" @change="changeValueGender(gender.id)">
                            <span></span>
                            <label style="margin-right: 40px;">{{ gender.name }}</label>
                        </div>
                    </form>
                </div>
            </div>
            <div class="row2">
                <div class="m-popup__workplace require">
                    <p>{{MISAResource[langCode].column.departmentId}} <span style="color: #E81E1E;">*</span></p>
                    <MCombobox :api="urlDepartment" :propText="'departmentName'" :id="'department'" :refArray="this.ref" :refs="'DepartmentId'"
                        :placeholder="MISAResource[langCode].input.placeholder.departmentId" v-model="employee.departmentId" 
                        :propValue="'departmentId'" @blurInput="blurInputDepartment" :error="errors.DepartmentId" :tabindexs="3" 
                    />
                    <div class="error__text"><p>{{ this.errors.DepartmentId }}</p></div>
                </div>
                <MInput v-model="employee.identityNumber"
                    :data="{
                        'class':'m-popup__idCard',
                        'textInput':MISAResource[langCode].column.identityNumber,
                        'placeholder':MISAResource[langCode].input.placeholder.identityNumber,
                        'data-c-tooltip': MISAResource[langCode].tooltip.identityNumber,
                        'typeInput':'text',
                        'tabindex':7,
                        'ref':'IdentityNumber',
                        'refArray' : this.ref,
                        'error':this.errors.IdentityNumber,
                        'maxLength':25
                    }"
                />
                <MInput v-model="employee.identityDate"
                    :data="{
                        'class':'m-popup__dateIdCard',
                        'textInput':MISAResource[langCode].column.identityDate,
                        'typeInput':'date',
                        'classTooltip':'tooltip-down tooltip-down-right',
                        'data-c-tooltip': MISAResource[langCode].tooltip.identityDate,
                        'tabindex':8,
                        'ref':'IdentityDate',
                        'refArray' : this.ref,
                        'error':this.errors.IdentityDate
                    }"
                />
            </div>
            <div class="row3">
                <div class="m-popup__workplace">
                    <p>{{MISAResource[langCode].column.positionId}}</p>
                    <MCombobox :api="urlPosition" :propText="'positionName'" :id="'position'"  :tabindexs="4" :refs="'Position'"
                        :placeholder="MISAResource[langCode].input.placeholder.positionId" v-model="employee.positionId" :propValue="'positionId'"/>
                </div>
                <MInput v-model="employee.identityPlace"
                    :data="{
                        'class':'m-popup__idplace',
                        'textInput':MISAResource[langCode].column.identityPlace,
                        'placeholder':MISAResource[langCode].input.placeholder.identityPlace,
                        'classTooltip':'tooltip-down tooltip-down-right',
                        'data-c-tooltip': MISAResource[langCode].tooltip.identityPlace,
                        'typeInput':'text',
                        'tabindex':9,
                        'maxLength':255
                    }"
                />
            </div>
            <div class="row4">
                <MInput v-model="employee.address"
                    :data="{
                        'class':'m-popup__address',
                        'textInput':MISAResource[langCode].column.address,
                        'placeholder':MISAResource[langCode].input.placeholder.address,
                        'typeInput':'text',
                        'tabindex':10,
                        'maxLength':255
                    }"
                />
            </div>
            <div class="row5">
                <MInput v-model="employee.phoneNumber" 
                    :data="{
                        'class':'m-popup__phone',
                        'textInput':MISAResource[langCode].column.phoneNumber,
                        'placeholder':MISAResource[langCode].input.placeholder.phoneNumber,
                        'typeInput':'text',
                        'tabindex':11,
                        'ref':'PhoneNumber',
                        'refArray' : this.ref,
                        'error':this.errors.PhoneNumber,
                        'maxLength':50
                    }"
                />
                <div class="m-popup__homephone">
                    <p>{{MISAResource[langCode].column.salary}}</p>
                    <input class="input" type="text" :placeholder="MISAResource[langCode].input.placeholder.salary" tabindex="12" :value="formattedValue" @input="handleInput"
                        ref="Salary">
                </div>
                <MInput v-model="employee.email" 
                    :data="{
                        'class':'m-popup__email',
                        'textInput':MISAResource[langCode].column.email,
                        'placeholder':MISAResource[langCode].input.placeholder.email,
                        'typeInput':'text',
                        'tabindex':13,
                        'ref':'Email',
                        'refArray' : this.ref,
                        'error':this.errors.Email,
                        'maxLength':100
                    }"
                />
            </div>
            <div class="row6">
                <MInput v-model="employee.bankId" 
                    :data="{
                        'class':'m-popup__bankid',
                        'textInput':MISAResource[langCode].column.bankId,
                        'placeholder':MISAResource[langCode].input.placeholder.bankId,
                        'typeInput':'text',
                        'tabindex':14,
                    }"
                />
                <MInput v-model="employee.bankName" 
                    :data="{
                        'class':'m-popup__bankname',
                        'textInput':MISAResource[langCode].column.bankName,
                        'placeholder':MISAResource[langCode].input.placeholder.bankName,
                        'typeInput':'text',
                        'tabindex':15,
                    }"
                />
                <MInput v-model="employee.bankAddress" 
                    :data="{
                        'class':'m-popup__bankaddress',
                        'classTooltip':'tooltip-down tooltip-down-right',
                        'textInput':MISAResource[langCode].column.bankAddress,
                        'placeholder':MISAResource[langCode].input.placeholder.bankAddress,
                        'data-c-tooltip': MISAResource[langCode].tooltip.bankAddress,
                        'typeInput':'text',
                        'tabindex':16,
                    }"
                />
            </div>
            <div class="m-popup__button">
                <div class="m-popup__button--left">
                    <MButton :text="'Hủy'" @keydown="tabKeyDown" class="m-btn-secondary" tabindex="19" @click="btnCloseOnClick"/>
                </div>
                <div class="m-popup__button--right">
                    <MButton :text="'Cất và thêm'" 
                        v-actionpress="{
                            isCtrl :true,
                            isShift : true, 
                            key: 83,
                            fnc: clickSaveEmployeeAndAdd
                        }"
                        tabindex="17" :class="'m-btn-secondary tooltip-up'" style="position: relative;" :data-c-tooltip="MISAResource[langCode].tooltip.saveAndAdd" @click="clickSaveEmployeeAndAdd"/>
                    <MButton :text="'Cất'" 
                        v-actionpress="{
                            isCtrl :true,
                            isShift : false, 
                            key: 83,
                            fnc: clickSaveEmployee
                        }"
                        tabindex="18" :class="'m-btn-main tooltip-up'" style="position: relative;" :data-c-tooltip="MISAResource[langCode].tooltip.save" @click="clickSaveEmployee"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapMutations, mapState} from 'vuex';
import form from '@/js/form';
import MISAResource from '@/js/base/MISAResource';
import MISAEnum from '@/js/base/MISAEnum';
import MISAApi from '@/js/base/MISAApi'
import data from '@/js/data'

export default {
    name:"EmployeeForm",
    component:{},
    props:["text","employeeIdUpdate","filter"],
    data() {
        return {
            MISAEnum : MISAEnum,
            MISAResource: MISAResource,
            DepartmentList:[],
            comboboxDepartment: false,
            PositionList:[],
            comboboxPosition:false,
            employee:{
                gender:0
            },
            errors:{},
            rootData: null,
            urlDepartment : MISAApi.urlDepartment,
            urlPosition : MISAApi.urlPosition,
            titleForm:"",
            dataDialog:{},
            dialog: false,
            focus:"",
            value:0,
            formattedValue:'',
            ref:null,
            config : {
                wrap: true, // set wrap to true only when using 'input-group'
                altInput: true,
                dateFormat: 'd-m-Y',         
            }
        }
    },

    created(){
        this.createdFunction()
    },

    watch:{
        /**
         * Theo dõi sự thay đổi của employee
         * Nếu đơn vị đã được chọn thì sẽ xóa error đi
         * Author: Vũ Quốc Ah (18/03/2023)
        */
        employee:{
            handler:function(newData){
                try {
                    if(newData.departmentId){
                        this.errors.DepartmentId =""
                    }
                } catch (error) {
                    console.log(error);
                }
            },
            deep:true
        },

        /**
         * Khi nhập lương vào ô input thì ô input sẽ tự động chuyển về dạng tiền tiếng việt
         * Author: Vũ Quốc Ah (18/03/2023)
        */
        value(newData){
            if(newData){
                this.formattedValue = newData.toLocaleString('vi-VN');
            }
        },

    },

    computed:{
        ...mapState(['detailFormMode','langCode']),

        /**
         * Giới tính
        */
        gender(){
            var gender = [
                {
                    id:MISAEnum.Gender.Female,
                    name: MISAResource[this.langCode].gender.female
                },
                {
                    id:MISAEnum.Gender.Male,
                    name: MISAResource[this.langCode].gender.male
                },
                {
                    id:MISAEnum.Gender.Other,
                    name: MISAResource[this.langCode].gender.other
                }
            ]
            return gender
        },

        /**
         * Khi nhập lương vào ô input thì ô input sẽ tự động chuyển về dạng tiền tiếng việt
         * Author: Vũ Quốc Ah (18/03/2023)
        */
        inputFormatMoney(){
            return data.formatMoney(this.employee.Salary)
        }

    },

    methods:{
        ...mapMutations(['HIDE_FORM','SHOW_FORM','CHANGE_FORMMODE','SHOW_LOADING_TABLE','HIDE_LOADING_TABLE','SHOW_LOADING','HIDE_LOADING']),
        ...mapActions(['addEmployee','showToastSuccess','editEmployee','getEmployeeFilter']),


        /**
         * Khi mới khởi tạo component thì sẽ kiểm tra đây là form gì và gọi các dữ liệu cần thiết
         * Dùng ở đây để trong hook created mình sẽ gọi hàm này tránh việc sử dụng async/await trong hook 
         * có thể gây ra việc created hoạt động sau mounted khi created phải đợi kết quả trả về
         * Author: Vũ Quốc Anh (20/04/2023)
        */
        async createdFunction(){
            try {
                //Nếu formMode là Add thì lấy employeeCode mới thêm vào form
                if(this.detailFormMode==MISAEnum.FormMode.Add){
                    this.$store.commit('SHOW_LOADING')
                    await this.newEmployeeCode()
                    this.titleForm = MISAResource[this.langCode].form.titleFormAdd
                    this.rootData = JSON.stringify(this.employee)
                    this.employee = JSON.parse(this.rootData)
                    this.$store.commit('HIDE_LOADING')
                }

                if(this.detailFormMode==MISAEnum.FormMode.Duplicate){
                    const requestUrl = MISAApi.urlEmployee + this.employeeIdUpdate;
                    //Call API lấy dữ liệu
                    this.$store.commit('SHOW_LOADING')
                    const response = await fetch(requestUrl, {
                        method: 'get',
                    });
                    //Chuyển data về dạng object
                    let responseJson = await response.json();
                    //Nếu dữ liệu call thất bại thì thông báo cho người dùng
                    if(response.status ==404){
                        this.$store.commit('HIDE_FORM')
                        //Call lại dữ liệu cho vào bảng
                        await this.getEmployeeFilter(this.filter)
                        //Hiện thông báo cho người dùng
                        form.showToast(responseJson.userMsg,MISAEnum.ToastMode.Error)
                        //Ẩn loading
                        this.$store.commit('HIDE_LOADING')
                    }
                    //Còn nếu call dữ liệu thành công
                    else{
                        this.employee = responseJson
                        this.formatData()
                        await this.newEmployeeCode()
                        this.employee.employeeId = this.generateUUID()
                        //Format dữ liệu
                        //Lưu dữ liệu vào data gốc
                        this.rootData = JSON.stringify(this.employee)
                        //Chuyển dữ liệu lại cho employee xử lý form
                        this.employee = JSON.parse(this.rootData)
                        //Chỉnh title form
                        this.titleForm = MISAResource[this.langCode].form.titleFormAdd
                        //Ẩn loading
                        this.$store.commit('HIDE_LOADING')

                    }
                }
                //Nếu formMode là Update thì
                if(this.detailFormMode==MISAEnum.FormMode.Update){
                    //Lấy dữ liệu từ chỉnh sửa bàng Id
                    const requestUrl = MISAApi.urlEmployee + this.employeeIdUpdate;
                    //Call API lấy dữ liệu
                    this.$store.commit('SHOW_LOADING')
                    const response = await fetch(requestUrl, {
                        method: 'get',
                    });
                    //Chuyển data về dạng object
                    let responseJson = await response.json();
                    //Nếu dữ liệu call thất bại thì thông báo cho người dùng
                    if(response.status ==404){
                        this.$store.commit('HIDE_FORM')
                        //Call lại dữ liệu cho vào bảng
                        await this.getEmployeeFilter(this.filter)
                        //Hiện thông báo cho người dùng
                        form.showToast(responseJson.userMsg,MISAEnum.ToastMode.Error)
                        //Ẩn loading
                        this.$store.commit('HIDE_LOADING')
                    }
                    //Còn nếu call dữ liệu thành công
                    else{
                        this.employee = responseJson
                        //Format dữ liệu
                        this.formatData()
                        //Lưu dữ liệu vào data gốc
                        this.rootData = JSON.stringify(this.employee)
                        //Chuyển dữ liệu lại cho employee xử lý form
                        this.employee = JSON.parse(this.rootData)
                        //Chỉnh title form
                        this.titleForm = MISAResource[this.langCode].form.titleFormUpdate
                        //Ẩn loading
                        this.$store.commit('HIDE_LOADING')

                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Tạo 1 GUID mới dành riêng cho trường hợp nhân bản
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        generateUUID() { 
            var d = new Date().getTime()
            var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
                var r = Math.random() * 16
                if(d > 0){
                    r = (d + r)%16 | 0
                    d = Math.floor(d/16)
                } else {
                    r = (d2 + r)%16 | 0
                    d2 = Math.floor(d2/16)
                }
                return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16)
            });
        },


        //#region HÀM XỬ LÝ LIÊN QUAN ĐẾN NHẬP LIỆU

        /**
         * Nhập giá trị lương sẽ format luôn định dạng tiền việt
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        handleInput(event){
            const inputString = event.target.value;
            console.log(inputString);
            const inputValue = parseFloat(inputString.replace(/[^0-9,-]+/g, '').replace(',', '.'));
            console.log(inputValue);
            this.value = isNaN(inputValue) ? null : inputValue;
            this.employee.salary = this.value
        },

        /**
         * Hàm để lấy EmployeeCode mới tự tăng 1 đơn vị so với số lớn nhất và chuyển về dạng NV-XXXXXX
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        newEmployeeCode: async function(){
            try {
                const requestUrl = MISAApi.urlEmployee + 'newEmployeeCode';
                //Call API lấy dữ liệu
                const response = await fetch(requestUrl, {
                    method: 'get',
                    headers: ({
                    'Content-type': 'application/json',
                    'Accept':'application/json'
                    }),
                });
                //Chuyển data về dạng object
                let responseJson = await response.json();
                this.employee.employeeCode = responseJson
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Định dạng lại ngày tháng để nhận diện trên form và gửi API đi ko bị lỗi
         * Author : Vũ Quốc Anh (24/03/2023)
        */
        formatData(){
            try {
                if(this.employee.dateOfBirth){
                    let date = this.employee.dateOfBirth
                    date = new Date(date)
                    let dateValue = date.getDate()
                    let monthValue = date.getMonth()+1
                    let yearValue = date.getFullYear()
                    if(dateValue<10) dateValue =`0${dateValue}`
                    if(monthValue<10) monthValue=`0${monthValue}`
                    this.employee.dateOfBirth = `${yearValue}-${monthValue}-${dateValue}`
                }
                if(this.employee.identityDate){
                    let date = this.employee.identityDate
                    date = new Date(date)
                    let dateValue = date.getDate()
                    let monthValue = date.getMonth()+1
                    let yearValue = date.getFullYear()
                    if(dateValue<10) dateValue =`0${dateValue}`
                    if(monthValue<10) monthValue=`0${monthValue}`
                    this.employee.identityDate = `${yearValue}-${monthValue}-${dateValue}`
                }  
                if(this.employee.salary){
                    this.formattedValue = this.employee.salary.toLocaleString('it-IT');
                } 
            } catch (error) {
                console.log(error);  
            }
        },

        /**
         * Khi tab đến cuối sẽ tự động focus lại ô input đầu tiên
         * Author : Vũ Quốc Anh (24/03/2023)
        */
        tabKeyDown(event){
            const keyCode = event.key
            if(keyCode == "Tab"){
                event.preventDefault();
                document.querySelector('.m-popup__id .input').focus()
            }
        },

        /**
         * Khi ấn validate thì chạy để kiểm tra các trường dữ liệu đã nhập
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        async valiDate(){
            try {
                this.errors.EmployeeCode = await form.validateEmployeeCOde(this.employee.employeeCode)
                this.errors.FullName = form.validateName(this.employee.fullName)
                this.errors.DateOfBirth = form.validateBirthday(this.employee.dateOfBirth)
                this.errors.DepartmentId = form.validateDepartment(this.employee.departmentId)
                this.errors.IdentityDate = form.validateBirthday(this.employee.identityDate)
                this.errors.IdentityNumber = form.validateIdentityNumber(this.employee.identityNumber)
                this.errors.PhoneNumber = form.validatePhoneNumber(this.employee.phoneNumber)
                // this.errors.Salary = form.validateSalary(this.employee.salary)
                this.errors.Email = form.validateEmail(this.employee.email)
                if(this.errors.EmployeeCode != ""){
                    this.ref = 'EmployeeCode' 
                    return 
                }
                if(this.errors.FullName != ""){
                    this.ref = 'FullName'
                    return
                }
                if(this.errors.DepartmentId != ""){
                    this.ref = 'DepartmentId' 
                    return
                }
                if(this.errors.DateOfBirth != ""){
                    this.ref = 'DateOfBirth' 
                    return
                }
                if(this.errors.IdentityNumber != ""){
                    this.ref = 'IdentityNumber'
                    return
                }
                if(this.errors.IdentityDate != ""){
                    this.ref = 'IdentityDate' 
                    return
                }
                if(this.errors.PhoneNumber != ""){
                    this.ref = 'PhoneNumber' 
                    return
                }
                // if(this.errors.Salary != ""){
                //     this.$refs.Salary.focus()
                //     return
                // }
                if(this.errors.Email != ""){
                    this.ref = 'Email' 
                    return
                }

            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm validate
         * Nếu không có lỗi nào sẽ return true , ngược lại return false
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        isCheckValidate(){
            if(this.errors.EmployeeCode==""&&this.errors.FullName==""&&this.errors.DateOfBirth==""&&this.errors.DepartmentId==""&&this.errors.IdentityDate==""&&
                this.errors.IdentityNumber==""&&this.errors.PhoneNumber==""&&this.errors.Email==""){
                this.ref = null
                return true
            }
            else{
                this.ref = null
                return false
            }
        },

        /**
         * Khi blur ra khỏi ô input mà giá trị input trống thì sẽ báo lỗi
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        blurInputDepartment: function(){
            try {
                if(!this.employee.departmentId){
                    this.errors.DepartmentId = MISAResource[this.langCode].error.departmentEmpty
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi blur ra khỏi ô input mà giá trị input trống thì sẽ báo lỗi
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        blurInputFullname: function(){
            try {
                if(this.employee.fullName){
                    if(this.employee.fullName.trim() == ""){
                        this.errors.FullName = MISAResource[this.langCode].error.fullNameEmpty
                    }
                    if(this.employee.fullName.trim() != ""){
                        this.errors.FullName =""
                    }
                }
                else{
                    this.errors.FullName = MISAResource[this.langCode].error.fullNameEmpty
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi blur ra khỏi ô input mà giá trị input trống thì sẽ báo lỗi
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        blurInputEmployeeCode(){
            try {
                if(this.employee.employeeCode){
                    if(this.employee.employeeCode.trim() == ""){
                        this.errors.EmployeeCode = MISAResource[this.langCode].error.employeeCodeEmpty
                    }
                    if(this.employee.employeeCode.trim() != ""){
                        this.errors.EmployeeCode =""
                    }
                }
                else{
                    this.errors.EmployeeCode = MISAResource[this.langCode].error.employeeCodeEmpty
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Theo dõi thay đổi của gender và gán giá trị mới vào employee.Gender
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        changeValueGender(id){
            try {
                this.employee.gender = id
                if(id==0){
                    this.employee.GenderName= "Nữ"
                }else if(id==1){
                    this.employee.GenderName= "Nam"
                }else if(id==2){
                    this.employee.GenderName= "Không xác định"
                }
            } catch (error) {
                console.log(error);
            }
        },

        //#endregion

        //#region HÀM XỬ LÝ FORM 

        /**
         * Khi ấn vào "Cất" thì sẽ xem các trường dữ liệu có lỗi ko
         * Nếu có lỗi thì sẽ cho hiện dialog các lỗi tương ứng
         * Nếu không lỗi thì sẽ gửi dữ liệu lên để call API thêm nhân viên và hiện toast báo thành công
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        clickSaveEmployee: async function(){
            try {
                //Validate dữ liệu và truyền vào kiểu form
                // await this.valiDate()
                var bool = true
                //Nếu tất cả các lỗi đều rỗng
                // if(this.isCheckValidate()){
                if(bool){
                    //Nếu là form thêm nhân viên 
                    for (let key in this.employee) {
                        if(this.employee[key] === null || this.employee[key] === ''){
                            this.employee[key] = null
                        }
                        else{
                            if(!Number.isFinite(this.employee[key])){
                                this.employee[key] = this.employee[key]?.trim()
                            }
                        }
                    }
                    if(this.detailFormMode==MISAEnum.FormMode.Add ||this.detailFormMode==MISAEnum.FormMode.Duplicate){
                        //Gọi API thêm nhân viên
                        let response = await this.addEmployee(this.employee)
                        //Nếu API call không thành công thì show toast báo lỗi
                        const responseJson = await response.json()
                        if(response.status == 201 && responseJson.success == false){
                            this.errors = responseJson.result.data
                            this.$store.commit('HIDE_LOADING')
                        } 
                        //Nếu call API thành công thì show toast thành công
                        else if(response.status == 201 && responseJson.success == true){
                            //Call lại API lấy tất cả dữ liệu để render vào bảng  
                            setTimeout(async() => {
                                await this.getEmployeeFilter(this.filter)
                                this.$store.commit('HIDE_FORM')
                                this.$store.commit('HIDE_LOADING')
                                form.showToast(MISAResource[this.langCode].toast.toastAddSuccess,MISAEnum.ToastMode.Success)
                            }, 1000);
                        }
                        else{
                            setTimeout(async() => {
                                await this.getEmployeeFilter(this.filter)
                                this.$store.commit('HIDE_FORM')
                                this.$store.commit('HIDE_LOADING')
                                form.showToast(responseJson.result.userMsg,MISAEnum.ToastMode.Error)
                            }, 1000);
                        }
                    } 
                    //Nếu là form sửa nhân viên 
                    if(this.detailFormMode==MISAEnum.FormMode.Update){
                        let newDataJson = JSON.stringify(this.employee)
                        if(newDataJson != this.rootData){
                            let response = await this.editEmployee(this.employee)
                            const responseJson = await response.json()
                            //Nếu lỗi là trùng mã code
                            if(response.status == 200 && responseJson.success == false){
                                this.errors = responseJson.result.data
                                this.$store.commit('HIDE_LOADING')
                            }
                            //Nếu lỗi là không tồn tại tài nguyên
                            else if(response.status == 404) {
                                setTimeout(async() => {
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_FORM')
                                    this.$store.commit('HIDE_LOADING')
                                    form.showToast(responseJson.result.userMsg,MISAEnum.ToastMode.Error)
                                }, 1000);
                            }
                            //Nếu call API thành công thì show toast thành công
                            else if(response.status == 200 && responseJson.success == true){
                                //Call lại API lấy tất cả dữ liệu để render vào bảng  
                                setTimeout(async() => {
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_FORM')
                                    this.$store.commit('HIDE_LOADING')
                                    form.showToast(MISAResource[this.langCode].toast.toastEditSuccess,MISAEnum.ToastMode.Success)
                                }, 1000);
                            }
                            else{
                                setTimeout(async() => {
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_FORM')
                                    this.$store.commit('HIDE_LOADING')
                                    form.showToast(responseJson.result.userMsg,MISAEnum.ToastMode.Error)
                                }, 1000);
                            }
                        }
                        else{
                            await this.getEmployeeFilter(this.filter)
                            this.$store.commit('HIDE_FORM')
                            this.$store.commit('HIDE_LOADING')
                            form.showToast(MISAResource[this.langCode].toast.toastEditSuccess,MISAEnum.ToastMode.Success)
                        }
                        
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào "Cất và thêm" thì y như khi ấn vào "Cát"
         * Khi thành công thì sẽ hiện form mới để người dùng nhập tiếp
         * Author: Vũ Quốc Anh (24/03/2023)
        */
        async clickSaveEmployeeAndAdd(){
            try {
                //Validate dữ liệu và truyền vào kiểu form
                await this.valiDate()
                //Nếu tất cả các lỗi đều rỗng
                if(this.isCheckValidate()){
                    for (let key in this.employee) {
                        if(this.employee[key] === null || this.employee[key] === ''){
                            this.employee[key] = null
                        }
                        else{
                            if(!Number.isFinite(this.employee[key])){
                                this.employee[key] = this.employee[key]?.trim()
                            }
                        }
                    }
                    //Nếu là form thêm nhân viên 
                    if(this.detailFormMode==MISAEnum.FormMode.Add || this.detailFormMode==MISAEnum.FormMode.Duplicate){
                        //Gọi API thêm nhân viên
                        let response = await this.addEmployee(this.employee)
                        const responseJson = await response.json()
                        if(response.status == 201 && responseJson.success == false){
                            this.errors = responseJson.result.data
                            this.$store.commit('HIDE_LOADING')
                        } 
                        //Nếu call API thành công thì show toast thành công
                        else if(response.status == 201 && responseJson.success == true){
                            setTimeout(async() => {
                                form.showToast(MISAResource[this.langCode].toast.toastAddSuccess,MISAEnum.ToastMode.Success)
                                this.$store.commit('HIDE_FORM')
                                await this.getEmployeeFilter(this.filter)
                                this.$store.commit('HIDE_LOADING')
                                //Call lại API lấy tất cả dữ liệu để render vào bảng  
                                this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
                                this.$store.commit('SHOW_FORM')
                            }, 1000);
                        }
                        else if(response.status == 400){
                            setTimeout(async() => {
                                form.showToast(responseJson.result.userMsg,MISAEnum.ToastMode.Error)
                                this.$store.commit('HIDE_FORM')
                                await this.getEmployeeFilter(this.filter)
                                this.$store.commit('HIDE_LOADING')
                                this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
                                this.$store.commit('SHOW_FORM')
                            }, 1000);
                        }
                    } 
                    //Nếu là form sửa nhân viên 
                    if(this.detailFormMode==MISAEnum.FormMode.Update){
                        let newDataJson = JSON.stringify(this.employee)
                        if(newDataJson != this.rootData){
                            //Gọi API sửa nhân viên
                            let response = await this.editEmployee(this.employee)
                            //Nếu API call không thành công thì show toast báo lỗi
                            const responseJson = await response.json()
                            if(response.status == 200 && responseJson.success == false){
                                this.errors = responseJson.result.data
                                this.$store.commit('HIDE_LOADING')
                            }
                            //Nếu lỗi là không tồn tại tài nguyên
                            else if(response.status == 404) {
                                setTimeout(async() => {
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_FORM')
                                    this.$store.commit('HIDE_LOADING')
                                    form.showToast(responseJson.result.userMsg,MISAEnum.ToastMode.Error)
                                }, 1000);
                            } 
                            //Nếu call API thành công thì show toast thành công
                            else if(response.status == 200 && responseJson.success == true){
                                setTimeout(async() => {
                                    form.showToast(MISAResource[this.langCode].toast.toastEditSuccess,MISAEnum.ToastMode.Success)
                                    this.$store.commit('HIDE_FORM')
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_LOADING')
                                    //Call lại API lấy tất cả dữ liệu để render vào bảng  
                                    this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
                                    this.$store.commit('SHOW_FORM')
                                }, 1000);
                            }
                            else{
                                setTimeout(async() => {
                                    form.showToast(MISAResource[this.langCode].toast.toastEditSuccess,MISAEnum.ToastMode.Success)
                                    this.$store.commit('HIDE_FORM')
                                    await this.getEmployeeFilter(this.filter)
                                    this.$store.commit('HIDE_LOADING')
                                    //Call lại API lấy tất cả dữ liệu để render vào bảng  
                                    this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
                                    this.$store.commit('SHOW_FORM')
                                }, 1000);
                            }
                        }
                        else{
                            form.showToast(MISAResource[this.langCode].toast.toastEditSuccess,MISAEnum.ToastMode.Success)
                            this.$store.commit('HIDE_FORM')
                            await this.getEmployeeFilter(this.filter)
                            this.$store.commit('HIDE_LOADING')
                            //Call lại API lấy tất cả dữ liệu để render vào bảng  
                            this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
                            this.$store.commit('SHOW_FORM')
                        }
                        
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào close hoặc nút HỦY ở form, xem dữ liệu có bị thay đổi không
         * Nếu dữ liệu không thay đổi cho người dùng đóng form
         * Nếu dữ liệu có thay đổi thì sẽ hiện dialog thông báo có muốn cất dữ liệu không
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        btnCloseOnClick(){
           try {
                let newDataJson = JSON.stringify(this.employee)
                if(newDataJson != this.rootData){
                    this.dataDialog={
                        dialogMode:MISAEnum.DialogMode.dataChange,
                    }
                    this.dialog = true
                }
                else{
                    this.$store.commit('HIDE_FORM')
                }
           } catch (error) {
                console.log(error);
           }
        },

        //#endregion

        //#region HÀM XỬ LÝ DIALOG

        /**
         * Ẩn dialog
         * Khi ấn nút "HỦY" ở dialog hoặc ấn close, là sẽ đóng dialog nhưng không đóng form
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        closeDialog(){
            try {
                this.dialog = false
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn nút "KHÔNG" ở dialog, là sẽ đóng dialog và đóng form
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        changeNoAccept(){
            try {
                this.dialog = false
                this.$store.commit('HIDE_FORM')
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn nút "CÓ" ở dialog, là sẽ đóng dialog và đóng form và cất dữ liệu
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        async changeAccept(){
           try {
             this.dialog = false
             await this.clickSaveEmployee()
           } catch (error) {
            console.log(error);
           }
        },

        //#endregion

    },
}
</script>

<script setup>
// import flatPickr from 'vue-flatpickr-component';
    import 'flatpickr/dist/flatpickr.css';
</script>

<style>
</style>