<template>
    <MLoading v-show="loading||loadings"/>
    <MDialog v-if="dialog" :dataDialog="dataDialog" @close-dialog="closeDialog" @removes-accept="removesAccept" @removes-noaccept="removesNoAccept" 
        @setting-noaccept="settingNoaccept" @setting-accept="saveSetting" @remove-accept="(employeeCode)=>removeAccept(employeeCode)"
        @default-setting="saveDefaultSetting"/>
    <EmployeeForm v-if="form" :employeeIdUpdate="employeeIdUpdate" :filter="this.filter"/>
    <div id="toast"></div>
    <div class="main__title">
        <p>{{MISAResource[langCode].categoryMenu.employee}}</p>
        <div>
            <i class="fa-solid fa-chevron-down"></i>
        </div>
        <MButton :text="MISAResource[langCode].button.addEmployee" :class="'m-btn-icon'" @click="clickAddBtn"/>
    </div>
    <div class="main__content">
        <div class="main__content--header">
            <div class="header__left">
                <div class="textField">
                    <input type="text" name="input" :placeholder="MISAResource[langCode].input.placeholder.search" v-model="filter.keyword">
                    <div class="search">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </div>
                </div>
                <div class="text" v-if="datasCheckbox.length >=1 ? true :false">
                    <p style="font-size: 14px;font-family: RobotoRegular;">
                        {{ MISAResource[langCode].checkbox.selected }}: 
                        <span style="font-family: RobotoBold;">{{datasCheckbox.length}}</span>
                    </p>
                    <p style="font-size: 14px;font-family: RobotoMedium;color: #FF2B2B;cursor: pointer;" @click="uncheckedAll">
                        {{ MISAResource[langCode].checkbox.unSelected }}
                    </p>
                    <p v-if="this.lengthCheckbox==this.datas.length && datasCheckbox.length != totalRecord" @click="clickCheckboxAll"
                        style="font-size: 14px;font-family: RobotoMedium;color: #1565C0;cursor: pointer;">
                        {{ MISAResource[langCode].checkbox.selectedAllPage }}
                    </p>
                    <div style="height:24px;width:24px;border-radius:8px;display:flex;align-items: center;justify-content:center;border: 1px solid #E0E0E0;cursor: pointer;background-color: #E0E0E0;" 
                        @click="clickRemoveAll" v-ctrld="clickRemoveAll" class="tooltip-down"
                        :data-c-tooltip="datasCheckbox.length == totalRecord  ? MISAResource[langCode].checkbox.removeAll : MISAResource[langCode].checkbox.removeSelected"
                    >
                        <div style="margin-left: 7px;margin-right: 7px">
                            <div>
                                <i style="color: #FF2B2B" class="fa-solid fa-trash-can"></i>
                            </div>
                        </div>
                        <!-- <p style="font-size: 14px;margin-right: 16px;">
                            {{datasCheckbox.length == totalRecord  ? MISAResource[langCode].checkbox.removeAll : MISAResource[langCode].checkbox.removeSelected}}
                        </p> -->
                    </div>
                </div>
            </div>
            <div class="header__right">
                <MCombobox :api="urlDepartment" :propText="'departmentName'" :id="'company'"
                            :placeholder="MISAResource[langCode].input.placeholder.departmentId " v-model="filter.departmentId" 
                            :propValue="'departmentId'" :refs="'Company'"/>
                <a class="icon icon--excel"  @click="downloadFile" target="_blank" :data-c-tooltip="MISAResource[langCode].tooltip.exportExcel">
                    <div class="svg"></div>
                </a>
                <div :class="['icon icon--setting',settings==true?'active':'']" @click="showSetting" :data-c-tooltip="MISAResource[langCode].tooltip.customizeColumn">
                    <div class="svg"></div>
                    <div class="custom-setting" v-if="settings" v-click-outside="hideSetting">
                        <div class="custom-setting-header">
                            <div class="title">
                                <p>{{MISAResource[langCode].setting.header}}</p>
                            </div>
                            <div class="close tooltip-down" @click="hideSetting" v-esc="hideSetting" :data-c-tooltip="MISAResource[langCode].tooltip.exitAndQuickKeys"> 
                                <i class="fa-solid fa-xmark"></i>
                            </div>
                        </div>
                        <div class="custom-setting-body" id="style-1">
                            <EmployeeSettingItem :name="MISAResource[langCode].column.employeeCode" :propText="'employeeCode'" v-model="customSettings.employeeCode"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.fullName" :propText="'fullName'" v-model="customSettings.fullName"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.gender" :propText="'gender'"  v-model="customSettings.gender"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.dateOfBirth" :propText="'dateOfBirth'"  v-model="customSettings.dateOfBirth"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.identityNumber" :propText="'identityNumber'"  v-model="customSettings.identityNumber"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.identityDate" :propText="'identityDate'"  v-model="customSettings.identityDate"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.identityPlace" :propText="'identityPlace'"  v-model="customSettings.identityPlace"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.positionId" :propText="'position'"  v-model="customSettings.position"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.departmentId" :propText="'department'"  v-model="customSettings.department"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.phoneNumber" :propText="'phoneNumber'"  v-model="customSettings.phoneNumber"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.salary" :propText="'salary'"  v-model="customSettings.salary"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.email" :propText="'email'"  v-model="customSettings.email"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.address" :propText="'address'"  v-model="customSettings.address"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.bankId" :propText="'bankId'"  v-model="customSettings.bankId"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.bankName" :propText="'bankName'"  v-model="customSettings.bankName"/>
                            <EmployeeSettingItem :name="MISAResource[langCode].column.bankAddress" :propText="'bankAddress'"  v-model="customSettings.bankAddress"/>
                        </div>
                        <div class="custom-setting-footer">
                            <MButton :text="MISAResource[langCode].button.saveDefaultSetting" :class="'m-btn-secondary'" @click="clickDefaultSetting"/>
                            <MButton :text="MISAResource[langCode].button.saveSetting" :class="'m-btn-main'" @click="saveSetting"/>
                        </div>
                    </div>
                </div>
                <div class="icon icon--reload" @click="reloadOnClick" :data-c-tooltip="MISAResource[langCode].tooltip.reload">
                    <div class="svg"></div>
                </div>
            </div>
        </div>
        <div class="main__content--filter" v-if="this.filter.dataFilter.length > 0">
                <div class="data-filter" id="style-1">
                    <EmployeeFilterItem v-for="data in this.filter.dataFilter" :key="data" :data="data" @removeFilter="(column)=>removeFilter(column)"/>
                </div>
            <p class="filter-delete" @click="clickRemoveAllFilter">
                {{ MISAResource[langCode].checkbox.removeAll }}
            </p>
        </div>
        <div class="main__content--table" :style="[this.filter.dataFilter.length > 0 ? 'height: 440.8px;':'']" id="style-1">
            <table class="table">
                <thead>
                    <tr class="table--head">
                        <td class="table__checkbox" type="checkbox" name="table__checkbox">
                            <input type="checkbox" v-model="checkboxAll"  @change="changeCheckboxAll">
                            <span :class="['checkmark',this.lengthCheckbox == this.datas.length ? 'full': '']"></span>
                        </td>
                        <td class="table__MNV left" v-show="this.settingsDefault.employeeCode">
                            <div class="wrapper">
                                <p @click="clickSort('EmployeeCode')">{{ MISAResource[langCode].column.employeeCode }}</p>
                                <MSort :sort="sort.EmployeeCode"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'EmployeeCode',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.employeeCode,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__TNV left" v-show="this.settingsDefault.fullName">
                            <div class="wrapper">
                                <p @click="clickSort('FullName')">{{ MISAResource[langCode].column.fullName }}</p>
                                <MSort :sort="sort.FullName"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'FullName',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.fullName,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__GT left" v-show="this.settingsDefault.gender">
                            <div class="wrapper">
                                <p @click="clickSort('Gender')">{{ MISAResource[langCode].column.gender }}</p>
                                <MSort :sort="sort.Gender"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'Gender','filterModes': this.filterMode3,
                                        'placeholder': MISAResource[langCode].input.filter.gender,'type': 'combobox',
                                        'propValue':'Gender','propText': 'GenderName',
                                        'id':'gender','refs':'Gender',
                                        'data':[{'Gender':MISAEnum.Gender.Female,'GenderName': MISAResource[langCode].gender.female},
                                                {'Gender':MISAEnum.Gender.Male,'GenderName': MISAResource[langCode].gender.male},
                                                {'Gender':MISAEnum.Gender.Other,'GenderName': MISAResource[langCode].gender.other}]
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__NS center" v-show="this.settingsDefault.dateOfBirth">
                            <div class="wrapper">
                                <p @click="clickSort('DateOfBirth')">{{ MISAResource[langCode].column.dateOfBirth }}</p>
                                <MSort :sort="sort.DateOfBirth"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'DateOfBirth',
                                        'filterModes': this.filterMode2,
                                        'placeholder': MISAResource[langCode].input.filter.dateOfBirth,
                                        'type': 'date'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__CMND left" v-show="this.settingsDefault.identityNumber">
                            <div class="wrapper">
                                <p :data-c-tooltip="MISAResource[langCode].tooltip.identityNumber" @click="clickSort('IdentityNumber')">{{ MISAResource[langCode].column.identityNumber }}</p>
                                <MSort :sort="sort.IdentityNumber"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'IdentityNumber',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.identityNumber,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="center" v-show="this.settingsDefault.identityDate">
                            <div class="wrapper">
                                <p :data-c-tooltip="MISAResource[langCode].tooltip.identityDate" @click="clickSort('IdentityDate')">{{ MISAResource[langCode].column.identityDate }}</p>
                                <MSort :sort="sort.IdentityDate"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'IdentityDate',
                                        'filterModes': this.filterMode2,
                                        'placeholder': MISAResource[langCode].input.filter.identityDate,
                                        'type': 'date'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="left" v-show="this.settingsDefault.identityPlace">
                            <div class="wrapper">
                                <p :data-c-tooltip="MISAResource[langCode].tooltip.identityPlace" @click="clickSort('IdentityPlace')">{{ MISAResource[langCode].column.identityPlace }}</p>
                                <MSort :sort="sort.IdentityPlace"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'IdentityPlace',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.identityPlace,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__CD left" v-show="this.settingsDefault.position">
                            <div class="wrapper">
                                <p @click="clickSort('PositionId')">{{ MISAResource[langCode].column.positionId }}</p>
                                <MSort :sort="sort.PositionId"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'PositionId','filterModes': this.filterMode3,
                                        'placeholder': MISAResource[langCode].input.filter.positionId,'type': 'combobox',
                                        'propValue':'positionId','propText': 'positionName',
                                        'id':'position','refs':'Position',
                                        'api': this.urlPosition               
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__TDV left" v-show="this.settingsDefault.department">
                            <div class="wrapper">
                                <p @click="clickSort('DepartmentId')">{{ MISAResource[langCode].column.departmentId }}</p>
                                <MSort :sort="sort.DepartmentId"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'DepartmentId','filterModes': this.filterMode3,
                                        'placeholder': MISAResource[langCode].input.filter.departmentId,'type': 'combobox',
                                        'propValue':'departmentId','propText': 'departmentName',
                                        'id':'department','refs':'DepartmentId',
                                        'api': this.urlDepartment            
                                    }"
                                />
                            </div>
                        </td>
                        <td class="left" v-show="this.settingsDefault.phoneNumber">
                            <div class="wrapper">
                                <p @click="clickSort('PhoneNumber')">{{ MISAResource[langCode].column.phoneNumber }}</p>
                                <MSort :sort="sort.PhoneNumber"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'PhoneNumber',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.phoneNumber,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__SALARY right" v-show="this.settingsDefault.salary">
                            <div class="wrapper">
                                <p @click="clickSort('Salary')">{{ MISAResource[langCode].column.salary }}</p>
                                <MSort :sort="sort.Salary"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'Salary',
                                        'filterModes': this.filterMode2,
                                        'placeholder': MISAResource[langCode].input.filter.salary,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="left" v-show="this.settingsDefault.email">
                            <div class="wrapper">
                                <p @click="clickSort('Email')">{{ MISAResource[langCode].column.email }}</p>
                                <MSort :sort="sort.Email"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'Email',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.email,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="left" v-show="this.settingsDefault.address">
                            <div class="wrapper">
                                <p @click="clickSort('Address')">{{ MISAResource[langCode].column.address }}</p>
                                <MSort :sort="sort.Address"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'Address',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.address,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__STK left" v-show="this.settingsDefault.bankId">
                            <div class="wrapper">
                                <p @click="clickSort('BankId')">{{ MISAResource[langCode].column.bankId }}</p>
                                <MSort :sort="sort.BankId"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'BankId',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.bankId,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__TNH left" v-show="this.settingsDefault.bankName">
                            <div class="wrapper">
                                <p @click="clickSort('BankName')">{{ MISAResource[langCode].column.bankName }}</p>
                                <MSort :sort="sort.BankName"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'BankName',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.bankName,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                        <td class="table__CNTKNH left" v-show="this.settingsDefault.bankAddress">
                            <div class="wrapper">
                                <p :data-c-tooltip="MISAResource[langCode].tooltip.bankAddress" @click="clickSort('BankAddress')">{{ MISAResource[langCode].column.bankAddress }}</p>
                                <MSort :sort="sort.BankAddress"/>
                                <MFilter v-model="dataFilter" 
                                    :datas="{
                                        'columnName': 'BankAddress',
                                        'filterModes': this.filterMode1,
                                        'placeholder': MISAResource[langCode].input.filter.bankAddress,
                                        'type': 'text'
                                    }"
                                />
                            </div>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <MTableRow v-for="data in datas" :key="data" :data="data" v-model="checkboxAll" 
                            @show-remove-dialog="(employee)=>showRemoveDialog(employee)"
                            @employeeId-update="(employeeId)=>this.employeeIdUpdate = employeeId"/>
                </tbody>
            </table>
        </div>
        <div class="main__content--pagination">
            <p class="record">
                {{ MISAResource[langCode].pagination.total }}: {{ formatTotalRecord }} {{ MISAResource[langCode].pagination.record }}
            </p>
            <div class="record-one-page">
                <p>{{ MISAResource[langCode].pagination.recordOnePage }}</p>
                <MCombobox :data="[20,50,80]" :id="'pagination'" v-model="filter.pageSize" :refs="'Pagination'"/>
            </div>
            <div class="number-record" v-show="this.totalRecord==0"><p>0 {{ MISAResource[langCode].pagination.record }}</p></div>
            <div class="number-record" v-show="this.totalRecord>0"><p>{{(filter.pageSize*(filter.pageNumber-1)+1).toLocaleString('it-IT')}}-{{filter.pageNumber==Math.ceil(totalRecord/filter.pageSize) ? totalRecord.toLocaleString('it-IT'):(filter.pageSize*filter.pageNumber).toLocaleString('it-IT')}} {{ MISAResource[langCode].pagination.record }}</p></div>
            <div class="navigation-page">
                <div :class="['pre-page',filter.pageNumber==1?'unactive':'']" @click="clickPrePage">
                    <i class="fa-solid fa-chevron-left"></i>
                </div>
                <div :class="['next-page',filter.pageNumber==Math.ceil(totalRecord/filter.pageSize)?'unactive':'']" @click="clickNextPage">
                    <i class="fa-solid fa-chevron-right"></i>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import EmployeeForm from './EmployeeForm.vue'
import { mapActions, mapGetters, mapMutations, mapState } from 'vuex'
import form from '@/js/form'
import MISAEnum from '@/js/base/MISAEnum'
import MISAResource from '@/js/base/MISAResource'
import EmployeeSettingItem from './EmployeeSettingItem.vue'
import EmployeeFilterItem from './EmployeeFilterItem.vue'
import MISAApi from '@/js/base/MISAApi'

export default {
    name: 'MainEmployee',
    components: {
        EmployeeForm,
        EmployeeSettingItem,
        EmployeeFilterItem
    },
    data() {
        return {
            MISAEnum : MISAEnum,
            MISAResource: MISAResource,
            loadings:false,
            checkboxAll: false,
            remove: null,
            keyword:null,
            pagination: 20,
            customSettings:{},
            rootSettings:{},
            formatSetting:[],
            settings:false,
            dialog:false,
            dataDialog:{},
            employeeIdUpdate:null,
            timer: null,
            departmentId:null,
            urlDepartment : MISAApi.urlDepartment,
            urlPosition: MISAApi.urlPosition,
            filter:{
                pageSize:20,
                pageNumber: 1,
                keyword: null,
                departmentId:null,
                sort:[],
                dataFilter : [],
                columns: ["employeeCode","fullName","gender","dateOfBirth","identityNumber","position","department","phoneNumber","salary"]
            },
            prevFilter:null,
            sort:{},
            dataFilter:{},
            lengthCheckbox: null
        }
    },
    created() {
        this.createdFunction()
    },
    

    computed:{
        ...mapState(['form','datasCheckbox','datas','collapseMenu','totalRecord','settingsDefault','loading','loadingTable','langCode']),
        ...mapGetters(['checkboxAllGetter','formatSettings']),
        formatTotalRecord(){
            if(this.totalRecord){
                return this.totalRecord.toLocaleString('it-IT')
            }
            return 0
        },
        /**
         * Bộ lọc dữ liệu cho những dữ liệu là text
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        filterMode1(){
            var filterMode = [
                {'filterId':MISAEnum.FilterMode.empty,'filterName': MISAResource[this.langCode].filterName.empty},
                {'filterId':MISAEnum.FilterMode.notEmpty,'filterName': MISAResource[this.langCode].filterName.notEmpty},
                {'filterId':MISAEnum.FilterMode.equal,'filterName': MISAResource[this.langCode].filterName.equal},
                {'filterId':MISAEnum.FilterMode.different,'filterName': MISAResource[this.langCode].filterName.different},
                {'filterId':MISAEnum.FilterMode.contain,'filterName': MISAResource[this.langCode].filterName.contain},
                {'filterId':MISAEnum.FilterMode.notContain,'filterName': MISAResource[this.langCode].filterName.notContain},
                {'filterId':MISAEnum.FilterMode.startWith,'filterName': MISAResource[this.langCode].filterName.startWith},
                {'filterId':MISAEnum.FilterMode.endWith,'filterName': MISAResource[this.langCode].filterName.endWith},
            ]
            return filterMode
        },

        /**
         * Bộ lọc dữ liệu cho những dữ liệu là dạng ngày tháng , hoặc dạng số như lương
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        filterMode2(){
            var filterMode = [
                {'filterId':MISAEnum.FilterMode.empty,'filterName': MISAResource[this.langCode].filterName.empty},
                {'filterId':MISAEnum.FilterMode.notEmpty,'filterName': MISAResource[this.langCode].filterName.notEmpty},
                {'filterId':MISAEnum.FilterMode.equal,'filterName': MISAResource[this.langCode].filterName.equal},
                {'filterId':MISAEnum.FilterMode.different,'filterName': MISAResource[this.langCode].filterName.different},
                {'filterId':MISAEnum.FilterMode.less,'filterName': MISAResource[this.langCode].filterName.less},
                {'filterId':MISAEnum.FilterMode.lessOrEqual,'filterName': MISAResource[this.langCode].filterName.lessOrEqual},
                {'filterId':MISAEnum.FilterMode.bigger,'filterName': MISAResource[this.langCode].filterName.bigger},
                {'filterId':MISAEnum.FilterMode.biggerOrEqual,'filterName': MISAResource[this.langCode].filterName.biggerOrEqual},
            ]
            return filterMode
        },

        /**
         * Bộ lọc dữ liệu cho những dữ liệu có value là dạng chỉ định : như chức danh, đơn vị , giới tính
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        filterMode3(){
            var filterMode = [
                {'filterId':MISAEnum.FilterMode.empty,'filterName': MISAResource[this.langCode].filterName.empty},
                {'filterId':MISAEnum.FilterMode.notEmpty,'filterName': MISAResource[this.langCode].filterName.notEmpty},
                {'filterId':MISAEnum.FilterMode.equal,'filterName': MISAResource[this.langCode].filterName.equal},
                {'filterId':MISAEnum.FilterMode.different,'filterName': MISAResource[this.langCode].filterName.different},
            ]
            return filterMode
        },
    },

    watch:{
        /**
         * Hàm theo dõi sự thay đổi của bộ lọc dữ liệu
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        dataFilter:{
            handler: function(){
                try {
                    //Nếu bộ lọc dữ liệu chưa có dữ liệu nào
                    if(this.filter.dataFilter.length==0){
                        //Thì lặp qua tất cả các bộ lọc rồi push vào trong filter để đưa vào API
                        for (const key in this.dataFilter) {
                            let value = key + '-' + this.dataFilter[key]
                            this.filter.dataFilter.push(value)
                        }
                    }
                    //Nếu bộ lọc dữ liệu đã có dữ liệu
                    else{
                        //Reset bộ lọc dữ liệu về rỗng
                        this.filter.dataFilter = []
                        let value =''
                        //Rồi lặp qua tất cả các bộ lọc rồi push vào trong filter để đưa vào API
                        for (const key in this.dataFilter) {
                            value = key + '-' + this.dataFilter[key]
                            this.filter.dataFilter.push(value)
                        }
                    }
                } catch (error) {
                    console.log(error)
                }
            },
            deep: true
        },

        /**
         * Hàm theo dõi sự thay đổi của filter(bao gồm theo dõi bộ lọc, số trang , số bản ghi trên trang , từ khóa, các cột dữ liệu)
         * Nếu như có sự thay đổi thì sẽ call API theo những dữ liệu đã được thay đổi
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        filter:{
            handler:async function(newValue){
                try {
                    if (this.prevFilter === null) {
                        this.prevFilter = JSON.parse(JSON.stringify(newValue));
                        return;
                    }
                    if(this.filter.keyword != this.prevFilter.keyword){
                        this.filter.pageNumber = 1
                        if(newValue.keyword.trim() ==""){
                            this.filter.keyword = ""
                        }    
                        this.debounce(async ()=>{
                            this.$store.commit('SHOW_LOADING_TABLE')
                            setTimeout(async () => {
                                await this.getEmployeeFilter(this.filter)
                                this.$store.commit('HIDE_LOADING_TABLE')
                            }, 500);
                        },500)
                    }
                    else if(this.filter.departmentId != this.prevFilter.departmentId){
                        this.$store.commit('SHOW_LOADING_TABLE')
                        this.filter.pageNumber = 1
                        setTimeout(async () => {
                            await this.getEmployeeFilter(this.filter)
                            this.$store.commit('HIDE_LOADING_TABLE')
                        }, 500);
                    }
                    else if(this.filter.pageSize != this.prevFilter.pageSize){
                        this.$store.commit('SHOW_LOADING_TABLE')
                        this.filter.pageNumber = 1
                        setTimeout(async () => {
                            await this.getEmployeeFilter(this.filter)
                            this.$store.commit('HIDE_LOADING_TABLE')
                        }, 500);
                    }
                    else{
                        this.$store.commit('SHOW_LOADING_TABLE')
                        setTimeout(async () => {
                            await this.getEmployeeFilter(this.filter)
                            this.$store.commit('HIDE_LOADING_TABLE')
                        }, 500);
                    }
                    this.prevFilter = JSON.parse(JSON.stringify(newValue));
                } catch (error) {
                    console.log(error)
                }
            },
            deep:true,
            immediate:true
        },

        /**
         * Khi tùy chọn trở về mặc định thì gán lại các mục tùy chọn
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        settingsDefault:{
            handler: function(){
                try {
                    this.rootSettings = JSON.stringify(this.settingsDefault)
                    this.customSettings = JSON.parse(this.rootSettings)
                    this.filter.columns = this.formatSettings
                } catch (error) {
                    console.log(error)
                }
            },
            deep:true
        },

         /**
         * Theo dõi sự thay đổi của các checkbox
         * Author: Vũ Quốc Anh (26/03/2023)
        */
        datasCheckbox:{
            handler:function(){
                try {
                    this.lengthCheckbox = 0
                    this.datasCheckbox.map(a=>{
                        if(this.datas.filter(b=>{return b.employeeId==a}).length != 0){
                            this.lengthCheckbox ++
                        }
                    })
                    if(this.lengthCheckbox>0){
                        this.checkboxAll = true
                    }
                    if(this.lengthCheckbox==0){
                        this.checkboxAll = false
                    }
                } catch (error) {
                    console.log(error)
                }
            },
            deep:true
        },

        /**
         * Theo dõi sự thay đổi của các bản ghi trong 1 trang
         * Author: Vũ Quốc Anh (26/03/2023)
        */
        datas:{
            handler:function(){
                try {
                    this.lengthCheckbox = 0
                    this.datasCheckbox.map(a=>{
                        if(this.datas.filter(b=>{return b.employeeId==a}).length != 0){
                            this.lengthCheckbox ++
                        }
                    })
                    if(this.lengthCheckbox>0){
                        this.checkboxAll = true
                    }
                    if(this.lengthCheckbox==0){
                        this.checkboxAll = false
                    }
                } catch (error) {
                    console.log(error)
                }
            },
            deep: true
        },
    },

    methods:{
        ...mapMutations(['SHOW_FORM','SET_EMPLOYEES','CHANGE_FORMMODE','ADD_ALL_DATA_CHECKBOX','REMOVE_ALL_DATA_CHECKBOX',
                        'SHOW_LOADING_TABLE','HIDE_LOADING_TABLE','SHOW_LOADING','HIDE_LOADING','ADD_ALL_DATA_CHECKBOX_ONE_PAGE',
                        'REMOVE_ALL_DATA_CHECKBOX_ONE_PAGE']),
        ...mapActions(['removeEmployees','getEmployeeFilter','removeEmployee','getEmployeeAll']),

        /**
         * Hàm để chạy trong hook created
         * Author: Vũ Quốc Anh (18/04/2023)
        */
        async createdFunction(){
            //Mới vào thì call API lấy dữ liệu và đổ vào table
            this.loadings = true
            setTimeout(async() => {
                await this.getEmployeeFilter(this.filter)
                this.loadings = false
                this.rootSettings = JSON.stringify(this.settingsDefault)
                this.customSettings = JSON.parse(this.rootSettings)
            }, 2000);
        },

        //#region CÁC HÀM XỬ LÝ LỌC DỮ LIỆU

        /**
         * Khi ấn vào dấu X ở từng mục để bỏ lọc mục mà người dùng muốn
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        removeFilter(column){
            try {
                delete this.dataFilter[column]
            } catch (error) {
                console.log(error)
            }
        },

        /**
         * Khi ấn vào dấu "Xóa tất cả" để loại bỏ các bộ lọc dữ liệu
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        clickRemoveAllFilter(){
            try {
                this.dataFilter = {}
            } catch (error) {
                console.log(error)
            }
        },

        //#endregion

        //#region CÁC HÀM XỬ LÝ SẮP XẾP

        /**
         * Hàm sắp xếp dữ liệu , sẽ format lại dữ liệu để cho vào API 
         * Author: Vũ Quốc Anh(29/04/2023)
        */
        clickSort(key){
            if(this.sort[key] == undefined){
                this.sort[key] = null
            }
            switch(this.sort[key]){
                case 1:
                    this.sort[key] = 2
                    break;
                case 2:
                    this.sort[key] = null
                    break;
                case null:
                    this.sort[key] = 1
                    break;
            }
            if(this.filter.sort.length ==0){
                if(this.sort[key]==1){
                    key ="-"+key
                }
                else if (this.sort[key]==2){
                    key ="+"+key
                }
                this.filter.sort.push(key)
            }
            else{
                var bool
                var index
                var value = this.sort[key]
                for(var i = 0;i < this.filter.sort.length;i++ ){
                    bool = this.filter.sort[i].includes(key)
                    if(bool==true){
                        index = i
                        break
                    }
                }
                if(value==1){
                    key ="-"+key
                }
                else if (value==2){
                    key ="+"+key
                }
                if(bool){
                    if(value==null){
                        this.filter.sort.splice(index,1)
                    }
                    else{
                        this.filter.sort[index] = key
                    }
                }
                else{
                    this.filter.sort.push(key)
                }
            }
        },

        //#endregion

        //#region CÁC HÀM LIÊN QUAN TỚI TÙY CHỌN SETTING

        /**
         * Khi đồng ý "Lưu lại mặc định"
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        saveDefaultSetting(){
            try {
                this.customSettings = { 
                    employeeCode: true,
                    fullName:true,
                    gender:true,
                    dateOfBirth: true,
                    identityNumber:true,
                    identityDate:false,
                    identityPlace:false,
                    position:true,
                    department:true,
                    phoneNumber:true,
                    salary:true,
                    email:false,
                    address:false,
                    bankId:false,
                    bankName:false,
                    bankAddress:false
                }
                this.$store.commit('CHANGE_SETTING',this.customSettings)
                this.dialog=false
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào "Lưu lại mặc định"
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        clickDefaultSetting(event){
            try {
                this.dialog=true
                this.dataDialog ={
                    dialogMode : MISAEnum.DialogMode.settingsDefault
                }
                event?.stopPropagation()
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Nếu dữ liệu bị thay đổi mà chưa lưu đã tắt setting thì sẽ hiện ra dialog 
         * Khi ấn vào nút "Không" sẽ không thực hiện thay đổi gì cả
         * Author: Vũ Quốc Anh (08/04/2023)
        */
        settingNoaccept(){
            try {
                this.customSettings = {...this.settingsDefault}
                this.settings = false
                this.dialog = false
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào "Lưu" những tùy chọn đã chọn
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        saveSetting(event){
            try {
                this.$store.commit('CHANGE_SETTING',this.customSettings)
                this.settings = false
                this.dialog = false
                event?.stopPropagation()
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Bật setting
         * Author: Vũ Quốc Anh (04/04/2023)
        */
        showSetting(){
            try {
                this.settings = true
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Tắt setting
         * Author: Vũ Quốc Anh (04/04/2023)
        */
        hideSetting(event){
            try {
                //Kiểm tra xem tùy chọn có bị thay đổi không , nếu không bị thay đổi thì ẩn setting
                if(JSON.stringify(this.customSettings) == this.rootSettings){
                    this.settings = false
                }
                //Nếu như có thay đổi
                else{
                    this.dialog = true
                    this.dataDialog = {
                        dialogMode:MISAEnum.DialogMode.settingsChange
                    }
                }
                event?.stopPropagation()
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi click ra ngoài setting sẽ tự ẩn setting
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        clickOutSideSetting(){
            try {
                //Kiểm tra xem tùy chọn có bị thay đổi không , nếu không bị thay đổi thì ẩn setting
                if(JSON.stringify(this.customSettings) == this.rootSettings){
                    this.settings = false
                }
                //Nếu như có thay đổi
                else{
                    this.dialog = true
                    this.dataDialog = {
                        dialogMode:MISAEnum.DialogMode.settingsChange
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        //#endregion

        //#region XỬ LÝ PHÂN TRANG

        /**
         * Khi số lùi trang thì số trang giảm đi 1 và call lại API
         * Author: Vũ Quốc Anh (04/04/2023)
        */
        async clickPrePage(){
            try {
                if(this.filter.pageNumber > 1){
                    this.filter.pageNumber = this.filter.pageNumber -1
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi số tiến trang thì số trang tăng lên 1 và call lại API
         * Author: Vũ Quốc Anh (04/04/2023)
        */
        async clickNextPage(){
            try {
                if(this.filter.pageNumber < Math.ceil(this.totalRecord/this.filter.pageSize)){
                    this.filter.pageNumber = this.filter.pageNumber +1
                }
            } catch (error) {
                console.log(error);
            }
        },

        //#endregion
        
        //#region CÁC HÀM LIÊN QUAN TỚI XÓA NHIỀU NHÂN VIÊN

        /**
         * Khi ấn chọn tất cả các trang để checkbox toàn bộ các bản ghi
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        async clickCheckboxAll(){
            try {
                let idDatas =[]
                this.$store.commit('SHOW_LOADING_TABLE')
                let response = await this.getEmployeeAll({
                    ...this.filter,
                    pageNumber:1,
                    pageSize:this.totalRecord
                })
                this.$store.commit('HIDE_LOADING_TABLE')
                response.data.map(data=>{
                    idDatas.push(data.employeeId)
                })
                this.$store.commit('ADD_ALL_DATA_CHECKBOX',idDatas)
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Hàm theo dõi sự thay đổi của checkboxAll
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        async changeCheckboxAll(){
            try {
                //Nếu như checkboxAll = false và số lượng đã checkbox bằng đúng số lượng bản ghi của 1 trang thì sẽ uncheck tất cả các bản ghi của trang đó
                if(this.checkboxAll == false && this.lengthCheckbox==this.datas.length){
                    let idDatas =[]
                    this.datas.map(data=>{
                        idDatas.push(data.employeeId)
                    })
                    this.$store.commit('REMOVE_ALL_DATA_CHECKBOX_ONE_PAGE',idDatas)
                }

                //Nếu như checkboxAll = false và số lượng đã checkbox khác số lượng bản ghi của 1 trang 
                //hoặc checkboxAll = true 
                //thì sẽ check tất cả các bản ghi của trang đó
                if((this.checkboxAll == false && this.lengthCheckbox!=this.datas.length)||(this.checkboxAll == true)){
                    this.checkboxAll == true
                    let idDatas =[]
                    this.datas.map(data=>{
                        idDatas.push(data.employeeId)
                    })
                    this.lengthCheckbox = 0
                    this.datasCheckbox.map(a=>{
                        if(this.datas.filter(b=>{return b.employeeId==a}).length != 0){
                            this.lengthCheckbox ++
                        }
                    })
                    if(this.lengthCheckbox>0){
                        this.checkboxAll = true
                    }
                    if(this.lengthCheckbox==0){
                        this.checkboxAll = false
                    }
                    this.$store.commit('ADD_ALL_DATA_CHECKBOX_ONE_PAGE',idDatas)
                }
            } catch (error) {
                console.log(error);
            }
        },


        /**
         * Khi ấn vào "Bỏ chọn" thì sẽ xóa check của các checkbox đã check
         * Author: Vũ Quốc Anh (27/03/2023)
        */
        uncheckedAll: function(){
            try {
                this.$store.commit('REMOVE_ALL_DATA_CHECKBOX')
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn vào "Xóa tất cả" hoặc "Xóa đã chọn" thì sẽ cho hiện dialog để chọn lựa chọn có xóa hay không
         * Author: Vũ Quốc Anh (27/03/2023)
        */
        clickRemoveAll(){
            try {
                this.dialog = true
                this.dataDialog={
                    dialogMode:MISAEnum.DialogMode.removeEmployees,
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Nếu đồng ý xóa các mục đã chọn thì
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        async removesAccept(){
            try {
                //Xóa các nhân viên có id trong datasCheckbox
                let countRecordRemove = this.datasCheckbox.length
                this.dialog=false
                let response = await this.removeEmployees(this.datasCheckbox)
                const responseJson = await response.json()
                if(this.filter.pageNumber > Math.ceil((this.totalRecord-countRecordRemove)/this.filter.pageSize)){
                    this.filter.pageNumber = Math.ceil((this.totalRecord-countRecordRemove)/this.filter.pageSize)
                }
                //Call lại dữ liệu và render vào table
                setTimeout(async() => {
                    await this.getEmployeeFilter(this.filter)
                    this.checkboxAll = false
                    this.$store.commit('HIDE_LOADING_TABLE')
                    if(response.status==200){
                        form.showToast(this.MISAResource[this.langCode].toast.toastRemoveSuccess,MISAEnum.ToastMode.Success)
                    }
                    if(response.status == 404){
                        form.showToast(responseJson.userMsg,MISAEnum.ToastMode.Error)
                    }
                }, 500);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Nếu không đồng ý xóa các mục đã chọn thì
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        removesNoAccept(){
            try {
                //Clear các mục đã chọn ra khỏi datasCheckbox
                this.$store.commit('REMOVE_ALL_DATA_CHECKBOX')
                this.dialog=false
            } catch (error) {
                console.log(error);
            }
        },

        //#endregion

        //#region CÁC HÀM LIÊN QUAN TỚI XÓA 1 NHÂN VIÊN

        /**
         * Khi nhấn vào icon "Xóa" 
         * Author: Vũ Quốc Anh (12/04/2023)
        */
        showRemoveDialog(employee){
            try {
                this.dialog=true
                this.dataDialog={
                    dialogMode:MISAEnum.DialogMode.removeEmployee,
                    data:employee
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Nếu đồng ý xóa 1 nhân viên thì call API
         * Author: Vũ Quốc Anh (12/04/2023)
        */
        async removeAccept(employeeId){
            try {
                this.dialog=false
                let response = await this.removeEmployee(employeeId)
                let responseJson = await response.json()
                //Call lại dữ liệu và render vào table
                if(this.filter.pageNumber > Math.ceil((this.totalRecord-1)/this.filter.pageSize)){
                    this.filter.pageNumber = Math.ceil((this.totalRecord-1)/this.filter.pageSize)
                }
                this.$store.commit('REMOVE_DATA_CHECKBOX',employeeId);
                setTimeout(async() => {
                    await this.getEmployeeFilter(this.filter)
                    this.$store.commit('HIDE_LOADING_TABLE')
                    if(response.status==200){
                        form.showToast(this.MISAResource[this.langCode].toast.toastRemoveSuccess,MISAEnum.ToastMode.Success)
                    }
                    if(response.status == 404){
                        form.showToast(responseJson.userMsg,MISAEnum.ToastMode.Error)
                    }
                }, 500);
            } catch (error) {
                console.log(error);
            }
        },

        //#endregion

        //#region CÁC HÀM XỬ LÝ ĐƠN LẺ

        /**
         * Sau một khoảng thời gian nhất định không thay đổi keyword mới call API
         * Author: Vũ Quốc Anh (19/04/2023)
        */
        debounce(fn, ms) {
            try {
                clearTimeout(this.timer);
                
                this.timer = setTimeout(async () => {
                    fn.apply(this, arguments);
                }, ms)
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Sự kiện khi ấn vào thêm mới nhân viên thì sẽ mở form và đổi formmode thành thêm nhân viên
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        clickAddBtn(){
            try {
                this.$store.commit('SHOW_FORM')
                this.$store.commit('CHANGE_FORMMODE',MISAEnum.FormMode.Add)
            } catch (error) {
                console.log(error);
            }
        },
        
        /**
         * Ẩn dialog
         * Author: Vũ Quốc Anh (10/04/2023)
        */
        closeDialog(){
            try {
                this.dialog=false
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi ấn reload sẽ load lại data 
         * Author: Vũ Quốc Anh(25/03/2023)
        */
        async reloadOnClick(){
            try {
                this.$store.commit('SHOW_LOADING_TABLE')
                setTimeout(async() => {
                    await this.getEmployeeFilter(this.filter)
                    this.$store.commit('HIDE_LOADING_TABLE')
                }, 1000);
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi muốn xuất dữ liệu ra file excel
         * Author: Vũ Quóc Anh (20/04/2023)
        */
        async downloadFile(){
            try {
                const requestUrl = "https://localhost:7244/api/v1/employees/export";
                this.$store.commit('SHOW_LOADING')
                const response = await fetch(requestUrl, {
                    method: 'post',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(this.filter),
                });
                const blob = await response.blob()
                this.$store.commit('HIDE_LOADING')
                const url = window.URL.createObjectURL(new Blob([blob])) 
                const link = document.createElement('a') 
                link.href = url
                link.setAttribute('download', 'Danh_Sach_Nhan_Vien.xlsx')
                document.body.appendChild(link)
                link.click() 
            } catch (error) {
                console.log(error)
            }
        },

        //#endregion
    },
}
</script>

<style>

</style>