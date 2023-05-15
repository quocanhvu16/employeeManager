<template>
    <div class="filter-item">
        <div class="filter-text">
            <p>{{column}}</p>
            <p>{{filterMode}}</p>
            <p>{{filterText}}</p>
        </div>
        <div class="filter-close" data-c-tooltip="Thoát ( ESC )" @click="clickRemoveFilter">
            <i class="fa-solid fa-xmark"></i>
        </div>
    </div>
</template>
  
<script>
import MISAEnum from '@/js/base/MISAEnum'
import MISAResource from '@/js/base/MISAResource'
import { mapState } from 'vuex'
  export default {
    name:"EmployeeFilterItem",
    props:['modelValue','data'],
    data() {
        return {
            MISAEnum : MISAEnum,
            MISAResource: MISAResource
        }
    },
    computed: {
        ...mapState(['dataDepartment','dataPosition','langCode']),

        /**
         * Hiển thị tên cột
         * Author: Vũ Quốc Anh (04/05/2023)
        */
        column(){
            var string = this.data.split("-")
            var column
            switch(string[0]){
                case 'EmployeeCode':
                    column = MISAResource[this.langCode].column.employeeCode
                    break
                case 'Gender':
                    column = MISAResource[this.langCode].column.gender
                    break
                case 'FullName':
                    column = MISAResource[this.langCode].column.fullName
                    break
                case 'DateOfBirth':
                    column = MISAResource[this.langCode].column.dateOfBirth
                    break
                case 'IdentityNumber':
                    column = MISAResource[this.langCode].column.identityNumber
                    break
                case 'IdentityDate':
                    column = MISAResource[this.langCode].column.identityDate
                    break
                case 'IdentityPlace':
                    column = MISAResource[this.langCode].column.identityPlace
                    break
                case 'PositionId':
                    column = MISAResource[this.langCode].column.positionId
                    break
                case 'DepartmentId':
                    column = MISAResource[this.langCode].column.departmentId
                    break
                case 'PhoneNumber':
                    column = MISAResource[this.langCode].column.phoneNumber
                    break
                case 'Salary':
                    column = MISAResource[this.langCode].column.salary
                    break
                case 'Email':
                    column = MISAResource[this.langCode].column.email
                    break
                case 'Address':
                    column = MISAResource[this.langCode].column.address
                    break
                case 'BankId':
                    column = MISAResource[this.langCode].column.bankId
                    break
                case 'BankName':
                    column = MISAResource[this.langCode].column.bankName
                    break
                case 'BankAddress':
                    column = MISAResource[this.langCode].column.bankAddress
                    break
            }
            return column
        },

        /**
         * Hiển thị dữ liệu được lọc theo kiểu nào
         * Author: Vũ Quốc Anh (04/05/2023)
        */
        filterMode(){
            var string = this.data.split("-")
            var filterMode
            switch(Number(string[1])){
                case MISAEnum.FilterMode.equal:
                    filterMode = MISAResource[this.langCode].filterName.equal.toLowerCase()
                    break
                case MISAEnum.FilterMode.empty:
                    filterMode = MISAResource[this.langCode].filterName.empty.toLowerCase()
                    break
                case MISAEnum.FilterMode.notEmpty:
                    filterMode = MISAResource[this.langCode].filterName.notEmpty.toLowerCase()
                    break
                case MISAEnum.FilterMode.different:
                    filterMode = MISAResource[this.langCode].filterName.different.toLowerCase()
                    break
                case MISAEnum.FilterMode.contain:
                    filterMode = MISAResource[this.langCode].filterName.contain.toLowerCase()
                    break
                case MISAEnum.FilterMode.notContain:
                    filterMode = MISAResource[this.langCode].filterName.notContain.toLowerCase()
                    break
                case MISAEnum.FilterMode.startWith:
                    filterMode = MISAResource[this.langCode].filterName.startWith.toLowerCase()
                    break
                case MISAEnum.FilterMode.endWith:
                    filterMode = MISAResource[this.langCode].filterName.endWith.toLowerCase()
                    break
                case MISAEnum.FilterMode.less:
                    filterMode = MISAResource[this.langCode].filterName.less.toLowerCase()
                    break
                case MISAEnum.FilterMode.lessOrEqual:
                    filterMode = MISAResource[this.langCode].filterName.lessOrEqual.toLowerCase()
                    break
                case MISAEnum.FilterMode.bigger:
                    filterMode = MISAResource[this.langCode].filterName.bigger.toLowerCase()
                    break
                case MISAEnum.FilterMode.biggerOrEqual:
                    filterMode = MISAResource[this.langCode].filterName.biggerOrEqual.toLowerCase()
                    break
            }
            return filterMode
        },

        /**
         * Hiển thị dữ liệu được lọc
         * Author: Vũ Quốc Anh (04/05/2023)
        */
        filterText(){
            var string = this.data.split("-")
            var index1 = this.data.indexOf("-")
            var index2 = this.data.indexOf("-",index1 +1)
            var filterText = this.data.substring(index2+1)
            switch(string[0]){
                case 'Gender':
                    if(filterText == MISAEnum.Gender.Female){
                        filterText = MISAResource[this.langCode].gender.female
                    }
                    if(filterText == MISAEnum.Gender.Male){
                        filterText = MISAResource[this.langCode].gender.male
                    }
                    if(filterText == MISAEnum.Gender.Other){
                        filterText = MISAResource[this.langCode].gender.other
                    }
                    break
                case 'DepartmentId':
                    this.dataDepartment.map(department=>{
                        if(department.departmentId == filterText){
                            filterText = department.departmentName
                        }
                    })
                    break
                case 'PositionId':
                    this.dataPosition.map(position=>{
                        if(position.positionId == filterText){
                            filterText = position.positionName
                        }
                    })
                    break
            }
            if(string[1] == MISAEnum.FilterMode.empty || string[1] == MISAEnum.FilterMode.notEmpty){
                filterText = ""
            }
            return filterText
        }
    },

    methods: {
        /**
         * Khi ấn vào nút X để xóa bộ lọc dữ liệu tương ứng
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        clickRemoveFilter(){
            try {
                //Truyền tên bộ lọc dữ liệu cần xóa ra component cha
                var string = this.data.split("-")
                this.$emit('removeFilter',string[0])
            } catch (error) {
                console.log(error)
            }
        }
    }
  }
</script>
  
<style>
  
</style>