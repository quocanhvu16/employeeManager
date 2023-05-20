import { createStore } from 'vuex'
import MISAEnum from './../js/base/MISAEnum'
import MISAApi from './../js/base/MISAApi'

export default createStore({
  state: {
    //Trạng thái form show hoặc không show
    form:false,
    //Trạng thái loading show hoặc không show
    loading: false,
    loadingTable :false,
    //Form Mode
    detailFormMode : MISAEnum.FormMode.Add,
    //Dialog Mode
    dialogMode: MISAEnum.DialogMode.dataInvalid,
    //Nơi lưu trữ id các checkbox được check
    datasCheckbox:[],
    //Lưu trữ dữ liệu đầu vào
    datas:[],
    //Lưu trữ các lỗi khi validate
    errors:{},
    //Trạng thái thu gọn menu
    collapseMenu : false,
    //Số lượng bản ghi 1 trang
    totalRecord: null,

    filter:{
      pageSize:20,
      pageNumber: 1,
      keyword: null,
    },

    settingsDefault:{
      employeeCode: true,
      fullName:true,
      gender:true,
      dateOfBirth: true,
      identityNumber:true,
      identityDate:true,
      identityPlace:true,
      position:true,
      department:true,
      phoneNumber:true,
      salary:true,
      email:true,
      address:true,
      bankId:true,
      bankName:true,
      bankAddress:true
    },

    dataDepartment: null,
    dataPosition: null,

    langCode : 'VI'
  },
  getters: {
    formatSettings : state => {
      var formatSetting=[]
      for (const key of Object.keys(state.settingsDefault)) {
          if(state.settingsDefault[key] == true){
              if(!formatSetting.includes(key)){
                  formatSetting.push(key)
              }
          }
      }
      return formatSetting
    },
    /**
     * Nếu ko checkbox nào được check thì giá trị này là false
     * Chỉ cần 1 checkbox được check giá trị này sẽ là true
     * Để set check cho checkboxAll
     * Author : Vũ Quốc Anh (25/03/2023)
     */
    checkboxAllGetter: state => {
      if(state.datasCheckbox.length == 0){
          return false
      }
      if(state.datasCheckbox.length > 0){
        return true
      }
    },
  },

  mutations: {
    UPDATE_DEPARTMENT(state,department){
      state.dataDepartment = department
    },
    UPDATE_POSITION(state,position){
      state.dataPosition = position
    },
    CHANGE_LANGCODE(state,langCode){
      state.langCode = langCode
    },

    CHANGE_FILTER(state,filter){
      state.filter = {...filter}
    },

    //#region HÀM XỬ LÝ ẨN HIỆN
    
    CHANGE_COLLAPSE_MENU(state){
      state.collapseMenu = !state.collapseMenu
    },
    //Show hoặc hide form
    SHOW_FORM(state){state.form = true},
    HIDE_FORM(state){state.form = false},

    //Show hoặc hide loading
    SHOW_LOADING(state){state.loading = true},
    HIDE_LOADING(state){state.loading = false},
    SHOW_LOADING_TABLE(state){state.loadingTable = true},
    HIDE_LOADING_TABLE(state){state.loadingTable = false},

    //#endregion

    //Set dữ liệu đầu vào vào state để cho vào table
    SET_EMPLOYEES(state , datas){
      state.datas = datas
    },

    //Thay đổi form mode
    CHANGE_FORMMODE(state,formMode){
      state.detailFormMode = formMode
    },

    //#region HÀM XỬ LÝ CHECKBOX

    //Thêm id được check vào state
    ADD_DATA_CHECKBOX(state,id){
      state.datasCheckbox.push(id)
    },

    //Xóa id bị uncheck ra khỏi state
    REMOVE_DATA_CHECKBOX(state,id){
      let newState = state.datasCheckbox.filter(data => data !=id)
      state.datasCheckbox = newState
    },

    ADD_ALL_DATA_CHECKBOX_ONE_PAGE(state,ids){
      ids.map(id=>{
        if(!state.datasCheckbox.includes(id)){
          state.datasCheckbox.push(id)
        }
      })
    },

    REMOVE_ALL_DATA_CHECKBOX_ONE_PAGE(state,ids){
      ids.map(id=>{
        if(state.datasCheckbox.includes(id)){
          let newState = state.datasCheckbox.filter(idCheckbox => {return idCheckbox != id })
          state.datasCheckbox = newState
        }
      })
    },

    //Thêm tất cả id được check vào state
    ADD_ALL_DATA_CHECKBOX(state,ids){
      state.datasCheckbox = []
      ids.map(id=>{
        state.datasCheckbox.push(id)
      })
    },

    //Xóa tất cả id bị uncheck ra khỏi state
    REMOVE_ALL_DATA_CHECKBOX(state){
      state.datasCheckbox = []
    },

    //#endregion

    /**
     * Thay đổi setting theo tùy chọn người dùng
     * Author: Vũ Quốc Anh (10/04/2023)
    */
    CHANGE_SETTING(state,settings){
      state.settingsDefault = {...settings}
    },
  },
  actions: {
    async getEmployeeAll({commit},filter){
      try {
        commit('SHOW_LOADING_TABLE')
        const requestUrl = MISAApi.urlEmployee + "paging"
        //Call API lấy dữ liệu
        const response = await fetch(requestUrl, {
            method: 'post',
            headers: ({
              'Accept':'application/json',
              'content-type': 'application/json'
            }),
            body: JSON.stringify(filter),
          });
        //Chuyển data về dạng object
        let responseJson = await response.json();
        return responseJson
      } catch (error) {
        console.log(error);
      }
    },
    
    async getEmployeeFilter({commit},filter){
      try {
          const requestUrl = MISAApi.urlEmployee + "paging"
          const response = await fetch(requestUrl, {
              method: 'post',
              headers: ({
                'Accept':'application/json',
                'content-type': 'application/json'
              }),
              body: JSON.stringify(filter),
          });
          //Chuyển data về dạng object
          let responseJson = await response.json();
          commit('SET_EMPLOYEES',responseJson.data)
          this.state.totalRecord = responseJson.totalRecord
          return responseJson
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm lấy dữ liệu
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    // async getEmployee({commit},filter){
    //   try {
    //     const requestUrl = MISAApi.urlEmployee + "paging"
    //     //Call API lấy dữ liệu
    //     const response = await fetch(requestUrl, {
    //         method: 'post',
    //         headers: ({
    //           'Accept':'application/json',
    //           'content-type': 'application/json'
    //         }),
    //         body: JSON.stringify(filter),
    //       });
    //     //Chuyển data về dạng object
    //     let responseJson = await response.json();
    //     commit('SET_EMPLOYEES',responseJson.data)
    //     this.state.totalRecord = responseJson.totalRecord
    //     return response.status
    //   } catch (error) {
    //     console.log(error);
    //   }
    // },

     /**
     * Hàm thay đổi số bản ghi 1 trang
     * Author: Vũ Quốc Anh (25/03/2023)
     */
     changeLimit({commit},limit){
      try {
        commit('CHANGE_LIMIT',limit)
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm thêm mới nhân viên
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    async addEmployee({commit},newEmployee){
      try {
        commit('SHOW_LOADING')
        const requestUrl = MISAApi.urlEmployee;
        const response = await fetch(requestUrl, {
            method: 'post',
            headers: ({
              'Accept':'application/json',
              'content-type': 'application/json'
            }),
            body: JSON.stringify(newEmployee)
        });
        return response
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm sửa nhân viên
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    async editEmployee({commit},editEmployee){
      try {
        commit('SHOW_LOADING')
        const requestUrl = MISAApi.urlEmployee + editEmployee.employeeId;
        const response = await fetch(requestUrl, {
            method: 'put',
            headers: ({'content-type': 'application/json'}),
            body:JSON.stringify(editEmployee)
        });
        return response
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xóa nhân viên
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    async removeEmployee({commit},employeeId){
      try {
        commit('SHOW_LOADING_TABLE')
        const requestUrl = MISAApi.urlEmployee + employeeId
        const response = await fetch(requestUrl, {
          headers: ({'content-type': 'application/json'}),
          method: 'delete',
        });
        return response
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hàm xóa nhiều nhân viên
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    async removeEmployees({commit},employees){
      try {
        commit('SHOW_LOADING_TABLE')
        const requestUrl = MISAApi.urlEmployee
        const response = await fetch(requestUrl, {
          headers: ({'content-type': 'application/json'}),
          method: 'delete',
          body:JSON.stringify(employees)
        });
        commit('REMOVE_ALL_DATA_CHECKBOX')
        return response
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * khi check một hàng thì sẽ thêm id vào mảng datasCheckbox
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    addDataCheckbox({commit},id){
      try {
        commit('ADD_DATA_CHECKBOX',id)
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * khi uncheck một hàng thì sẽ xóa id khỏi mảng datasCheckbox
     * Author: Vũ Quốc Anh (25/03/2023)
     */
    removeDataCheckbox({commit},id){
      try {
        commit('REMOVE_DATA_CHECKBOX',id)
      } catch (error) {
        console.log(error);
      }
    },

  },

  modules: {
  }
})

