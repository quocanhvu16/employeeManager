<template>
    <tr :class="['table--body',loadingTable ? 'skeleton' :'']" @dblclick="clickEditEmployee">
        <td :class="['table__checkbox',checkbox==true ? 'active' : '']">
            <input v-show="!loadingTable" type="checkbox" class="checkbox" v-model="checkbox" @change="changeCheckbox" id="input-checkbox">
            <span v-show="!loadingTable" class="checkmark"></span>
            <p v-show="loadingTable"></p>
        </td>
        <td :class="['table__MNV left',checkbox==true ? 'active' : '']" v-show="this.settingsDefault.employeeCode">
            <p :data-c-tooltip="data.employeeCode" class="tooltip-right">{{loadingTable==false ? data.employeeCode:''}}</p>
        </td>
        <td :class="['table__TNV left',checkbox==true ? 'active' : '']" v-show="this.settingsDefault.fullName">
            <p :data-c-tooltip="data.fullName" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.fullName : ''}}</p>
        </td>
        <td :class="['table__GT left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.gender">
            <p :data-c-tooltip="formatGender" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? formatGender : ''}}</p>
        </td>
        <td :class="['table__NS center',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.dateOfBirth">
            <p :data-c-tooltip="formatDate" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? formatDate : ''}}</p>
        </td>
        <td :class="['table__CMND left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.identityNumber">
            <p :data-c-tooltip="data.identityNumber" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.identityNumber : ''}}</p>
        </td>
        <td :class="['center',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.identityDate">
            <p :data-c-tooltip="formatIdentityDate" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? formatIdentityDate : ''}}</p>
        </td>
        <td :class="['left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.identityPlace">
            <p :data-c-tooltip="data.identityPlace" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.identityPlace : ''}}</p>
        </td>
        <td :class="['table__CD left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.position" >
            <p :data-c-tooltip="data.positionName" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.positionName : ''}}</p>
        </td>
        <td :class="['table__TDV left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.department" >
            <p :data-c-tooltip="data.departmentName" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.departmentName : ''}}</p>
        </td>
        <td :class="['left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.phoneNumber">
            <p :data-c-tooltip="data.phoneNumber" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.phoneNumber : ''}}</p>
        </td>
        <td :class="['table__SALARY right',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.salary">
            <p :data-c-tooltip="formatMoney" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? formatMoney : ''}}</p>
        </td>
        <td :class="['left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.email">
            <p :data-c-tooltip="data.email" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.email : ''}}</p>
        </td>
        <td :class="['left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.address">
            <p :data-c-tooltip="data.address" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.address : ''}}</p>
        </td>
        <td :class="['table__STK left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.bankId">
            <p :data-c-tooltip="data.bankId" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.bankId : ''}}</p>
        </td>
        <td :class="['table__TNH left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.bankName">
            <p :data-c-tooltip="data.bankName" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.bankName : ''}}</p>
        </td>
        <td :class="['table__CNTKNH left',checkbox==true ? 'active' : '']"  v-show="this.settingsDefault.bankAddress">
            <p :data-c-tooltip="data.bankAddress" class="tooltip-down1 tooltip-down-right">{{loadingTable==false ? data.bankAddress : ''}}</p>
        </td>
        <div class="table__CN--edit" v-show="!loadingTable">
            <div class="edit" data-c-tooltip="Chỉnh sửa" @click="clickEditEmployee">
                <div class="edit-icon"></div>
            </div>
            <div class="duplicate" data-c-tooltip="Nhân bản" @click="clickDuplicateEmployee">
                <div class="duplicate-icon"></div>
            </div>
            <div class="remove" data-c-tooltip="Xóa" @click="clickRemoveEmployee">
                <div class="remove-icon">
                    <i class="fa-solid fa-trash-can"></i>
                </div>
            </div>
        </div>
    </tr>

</template>

<script>
import { mapActions,mapMutations, mapState } from 'vuex';
import data from '@/js/data';
import MISAEnum from '@/js/base/MISAEnum';

export default {
    name:"MTableRow",
    component:{},
    props:["data"],
    data() {
        return {
            checkbox:false,
        }
    },
    created() {
        //Khi mới render ra thì lấy các id đã checkbox
        this.datasCheckbox.map(id=>{
            if(id == this.data.employeeId ){
                this.checkbox = true
            }
        })
    },

    computed:{
        ...mapState(['datasCheckbox','checkboxAll','settingsDefault','loadingTable']),
        formatDate(){
            return data.formatDate(this.data.dateOfBirth)
        },
        formatIdentityDate(){
            return data.formatDate(this.data.identityDate)
        },
        formatMoney(){
            return data.formatMoney(this.data.salary)
        },
        formatGender(){
            return data.formatGender(this.data.gender)
        }
    },
    watch:{
        /**
         * Theo dõi sự thay đổi của checkbox và gán giá trị tương ứng
         * Author: Vũ Quốc Anh (18/03/2023)
        */
        datasCheckbox: {
            handler: function(){
                var bool = false
                this.datasCheckbox.map(id=>{
                    if(id == this.data.employeeId ){
                        bool = true
                    }
                })
                this.checkbox = bool
            },
            deep:true
        }
    },

    methods:{
        ...mapMutations(['SHOW_FORM','SET_EMPLOYEES','SHOW_FORM_EDIT','CHANGE_FORMMODE','HIDE_FORM']),
        ...mapActions(['getEmployee','removeEmployee','showToastSuccess','addDataCheckbox','removeDataCheckbox']),

        /**
         * Ấn vào Xóa thì ta sẽ gọi tới hàm xóa nhân viên
         * Sau đó sẽ gọi load lại dữ liệu và cho vào bảng
         * Hiện toast thành công
         * Author : Vũ Quốc Anh (24/03/2023)
        */
        async clickRemoveEmployee(){
            try {
                this.$emit('show-remove-dialog',this.data)
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Khi ấn vào sửa dữ liệu, ta sẽ gửi id cần update ra component cha, show form sửa nhân viên và chuyển formMode sang update
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        clickEditEmployee(event){
            try {
                // Nếu dblick ngoài checkbox thì mới show form
                if (!event.target.closest('#input-checkbox')){
                    this.$emit('employeeId-update',this.data.employeeId)
                    this.$store.commit('SHOW_FORM')
                    this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Update)
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào nhân bản dữ liệu, ta sẽ gửi id cần nhân bản ra component cha, show form thêm nhân viên và chuyển formMode sang Add
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        clickDuplicateEmployee(){
            try {
                this.$emit('employeeId-update',this.data.employeeId)
                this.$store.commit('SHOW_FORM')
                this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Duplicate)
            } catch (error) {
                console.log(error)
            }
        },

         /**
         * Lắng nghe sự thay đổi ở ô checkbox, 
         * nếu check = true thì sẽ thêm id của dữ liệu này vào trên mảng VueX
         * nếu check = false thì sẽ xóa id của dữ liệu này vào trên mảng VueX
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        changeCheckbox(){
            try {
                if(this.checkbox == true){
                    this.addDataCheckbox(this.data.employeeId)
                }
                if(this.checkbox == false){
                    this.removeDataCheckbox(this.data.employeeId)
                }
            } catch (error) {
                console.log(error)
            }
        },
    },
}
</script>

<style>

</style>