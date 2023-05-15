<template>
    <div class="combobox" :id="id" v-click-outside="hideCombobox" :class="[error ==''||error==null||error==undefined ? '' : 'error']">
        <input type="text" class="combobox__input" v-model="selectValue" 
            :placeholder="placeholder" @input="inputOnChange" @keydown="inputOnKeyDown" @blur="blurInput"
            :ref="this.refs" :style="[this.id=='company'||this.id=='pagination'||this.id=='filter'? 'cursor:default;':'']" 
            :readonly="this.id=='company'||this.id=='pagination'||this.id=='filter' ? true:false" :tabindex="tabindexs"
        >
        <button class="combobox__button" @click="btnOnClickShowData" @mousedown="mousedownBtn" tabindex="-1" ref="btn">
            <i class="fa-solid fa-chevron-down"></i>
        </button>
        <div class="combobox__data" v-show="showData">
            <div class="scrollbar">
                <a class="combobox__data-item"
                    :class="[(propValue ? item[propValue] : item) == modelValue ? 'active' : '', 
                            index == indexSelected ? 'combobox__data-item--selected': '',
                            selectValue == item ? 'active' : '']"
                    @mousedown="mouseDownSelect(item)"
                    @click.prevent="selectDataItem(item)" 
                    v-for="(item,index) in dataArray" :key="index"
                >
                    {{ propText ? item[propText] : item }}
                    <div class="check" hidden>
                        <i class="fa-solid fa-check"></i>
                    </div>
                </a>
            </div>
        </div>
    </div>
</template>

<script>
import form from '@/js/form'
import { mapMutations} from 'vuex'
import MISAEnum from '@/js/base/MISAEnum'
import MISAResource from '@/js/base/MISAResource'

export default {
    name:"MCombobox",
    props:{
        api:{
            type: String,
            default: null,
            required: false
        },
        data:{
            type: Array,
            default: null,
            required: false
        },
        propText:{
            type: String,
            default: null,
            required: false
        },
        id:{
            type: String,
            default: null,
            required: false
        },
        placeholder:{
            type: String,
            default: null,
            required: false
        },
        modelValue: {
            type: [String, Boolean, Number, Array, Object],
            default: null,
            required: false
        },
        propValue:{
            type: String,
            default: null,
            required: false
        },
        refArray:{
            type: String,
            default: null,
            required: false
        },
        error:{
            type: String,
            default: null,
            required: false
        },
        tabindexs:{
            type: Number,
            default: null,
            required:false
        },
        refs:{
            type: String,
            default: null,
            required:false
        },
    },

    async created() {
       try {
        //Mếu như có API thì sẽ ưu tiên lấy dữ liệu từ API đổ vào dataRoot
        if(this.api){
            this.$store.commit('SHOW_LOADING')
            var res = await fetch(this.api)
            let resJson = await res.json()
            if(this.id == "company"){
                resJson = [
                    {
                        departmentId:null,
                        departmentName: "- Chọn đơn vị công tác -"
                    },
                    ...resJson
                ]
            }
            this.dataArray = resJson
            this.dataRoot = resJson
            if(this.id == 'department'){
                this.$store.commit('UPDATE_DEPARTMENT',resJson)
            }
            if(this.id == 'position'){
                this.$store.commit('UPDATE_POSITION',resJson)
            }
            this.$store.commit('HIDE_LOADING')
        }
        //Nếu không có API thì sẽ lấy dữ liệu từ data truyền xuống từ component cha
        else if(this.data){
            this.dataArray = this.data
            this.dataRoot = this.data
        }

        //Khi là form sửa thông tin , ta sẽ phải theo dõi các lựa chọn selectedValue từ trên API trả về và đưa giá trị đó vào ô input
        //Nếu không có thì giá trị ô input sẽ bị rỗng dù dữ liệu vẫn có
        if(this.modelValue){
            this.dataRoot.forEach(data=>{
                if(data[this.propValue] == this.modelValue){
                    this.selectValue = data[this.propText]
                }
            })
        }
        if(this.id == "pagination"){
            this.selectValue = 20
        }
       } catch (error) {
        console.log(error);
       }
    },
    watch: {
        data: function(newData){
            this.dataArray = newData
        },
        showData: function(newData){
            console.log(newData);
            if(newData == false && this.modelValue == null){
                this.indexSelected = null
            }
            if(newData == false){
                if(this.api){
                    var bool = false
                    this.dataRoot.forEach(data => {
                        if(data?.[this.propText] == this.selectValue){
                            bool = true
                        }
                    });
                    if(bool == false){
                        let itemSelect = this.dataRoot.filter(data=>data?.[this.propValue]==this.modelValue)
                        this.selectValue = itemSelect[0]?.[this.propText]
                    }
                }
            }
            var myComboboxSelected = document.querySelector('.combobox__data .scrollbar .combobox__data-item.active.combobox__data-item--selected')
            myComboboxSelected?.scrollIntoView(true);
        },

        //Nếu mạng bị chậm , khi form khởi tạo rồi nhưng combobox chưa kịp call API xong để đưa giá trị vào combobox
        //Thì khi call API xong sẽ gán giá trị vào combobox
        modelValue: function(){
            try {
                if(this.api){
                    if(this.modelValue==null){
                        this.selectValue = null
                    }
                    else{
                        this.dataRoot.forEach(data=>{
                            if(data[this.propValue] == this.modelValue){
                                this.selectValue = data[this.propText]
                            }
                        })
                    }
                }
                if(this.data && this.propValue){
                    if(this.modelValue==null){
                        this.selectValue = null
                    }
                    else{
                        this.dataRoot.forEach(data=>{
                            if(data[this.propValue] == this.modelValue){
                                this.selectValue = data[this.propText]
                            }
                        })
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
        
        /**
         * Theo dõi sự thay đổi của giá trị được chọn
         * Author: Vũ QUốc Anh (28/04/2023)
        */
        selectValue: function(){
            if(this.id == "company"){
                if(this.selectValue == "- Chọn đơn vị công tác -" || this.selectValue == "- Hủy chọn đơn vị -"){
                    this.dataRoot.shift()
                    this.dataRoot = [
                        {
                            departmentId:null,
                            departmentName: "- Chọn đơn vị công tác -"
                        },
                        ...this.dataRoot
                    ]
                    this.selectValue = null
                }
                if(this.selectValue != "- Chọn đơn vị công tác -" && this.selectValue != null){
                    this.dataRoot.shift()
                    this.dataRoot = [
                        {
                            departmentId:null,
                            departmentName: "- Hủy chọn đơn vị -"
                        },
                        ...this.dataRoot
                    ]
                }
            }
        },

        /**
         * Theo dõi sự thay đổi của ref dể set ô focus
         * Author: Vũ QUốc Anh (28/04/2023)
        */
        refArray: function(){
            try {
                if(this.id == "department"){
                    if(this.refArray == 'DepartmentId'){
                        this.$refs[this.refs].focus()
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },
    },
    data() {
        return {
            MISAEnum : MISAEnum,
            MISAResource: MISAResource,
            showData: false,
            selectValue: null,
            dataArray: [],
            dataRoot: [],
            indexSelected: null
        }
    },
    methods: {
        ...mapMutations(['SHOW_LOADING','HIDE_LOADING','UPDATE_DEPARTMENT','UPDATE_POSITION']),

        /**
         * Bắt sự kiện khi ấn chuột xuống chọn vào giá trị người dùng muốn
         * Author: Vũ Quốc Anh (24/04/2023)
        */
        mouseDownSelect(item){
            this.selectValue = this.propText?item[this.propText]:item
            //Lấy id của dữ liệu và binding 2 chiều
            const value = this.propValue ? item[this.propValue]:item
            //Gửi dữ liệu lên component cha
            this.$emit("update:modelValue",value)
        },

        /**
         * Bắt sự kiện khi ấn chuột xuống để mở combobox
         * Author: Vũ Quốc Anh (24/04/2023)
        */
        mousedownBtn(){
            var arrayId = []
            this.dataArray = this.dataRoot
            arrayId = this.dataArray.map(a=>{
                return this.propValue ? a?.[this.propValue] : a
            })
            if(arrayId.indexOf(this.modelValue) == -1){
                this.indexSelected = null
            }
            else{
                this.indexSelected = arrayId.indexOf(this.modelValue)
            }
            if(this.showData==false){
                if(this.id == 'department'){
                    this.$refs.DepartmentId.blur()
                }
                else{
                    this.$refs[this.refs].blur()
                }
            }
            this.showData= !this.showData
        },

        /**
         * Khi click vào để showCombobox thì chuyển focus từ button sang bên input
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        btnOnClickShowData(){
            try {
                if(this.id == 'department'){
                    this.$refs.DepartmentId.focus() 
                }
                else{
                    this.$refs[this.refs].focus()
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi click outside thì ẩn combobox
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        hideCombobox(){
            try {
                this.showData=false
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Lựa chọn item trong combobox
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        selectDataItem(item){
            try {
                //Hiện thị text vừa chọn lên trên input
                this.selectValue = this.propText?item[this.propText]:item
                //Lấy id của dữ liệu và binding 2 chiều
                const value = this.propValue ? item[this.propValue]:item
                //Gửi dữ liệu lên component cha
                this.$emit("update:modelValue",value)
                //Ẩn combobox
                this.showData= false
                
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Autocompleted combobox
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        inputOnChange(){
            try {
                if(this.api){
                    //Khi nhập text vào ô input thì lọc ra các phần tử trùng với dữ liệu ban đầu
                    //Đã convert sang tiếng anh và loại bỏ các khoảng trắng
                    this.dataArray = this.dataRoot.filter(item =>
                        form.removeVietnameseTones(item[this.propText].toLowerCase().split(" ").join(""))
                            .includes(form.removeVietnameseTones(this.selectValue.toLowerCase().split(" ").join(""))))
                    //Hiện combobox
                    this.showData = true
                    //Nếu như trước đó đã nhập dữ liệu mà xóa hết ô input đi thì sẽ set lại modelValue là rỗng
                    if(this.selectValue == ""){
                        this.$emit("update:modelValue","")
                    }
                    if(this.dataArray.length == 1){
                        this.indexSelected = 0
                    }
                    else{
                        this.indexSelected = null
                    }
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Các nghiệp vụ khác ấn nút lên xuống, tab ,enter
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        inputOnKeyDown(){
            try {
                const keyCode = event.key
                var arrayId=[]
                var myCombobox = document.querySelector(`#${this.id} .combobox__data .scrollbar .combobox__data-item.combobox__data-item--selected`)
                var myScroll = document.querySelector(`#${this.id} .combobox__data .scrollbar`)
                // var myComboboxSelected = document.querySelector('.combobox__data .scrollbar .combobox__data-item.active')
                // var myCombobox 
                // // var myScroll = document.querySelector('.combobox__data .scrollbar')
                // var myComboboxSelected
                switch (keyCode) {
                    //Khi ấn nút xuống
                    case 'ArrowDown':
                        if(this.showData == false){
                            this.showData = true
                            if(this.modelValue != null && this.modelValue !=""){
                                this.dataArray = this.dataRoot
                                arrayId = this.dataArray.map(a=>{
                                    return this.propValue ? a?.[this.propValue] : a
                                })
                                this.indexSelected = arrayId.indexOf(this.modelValue)
                            }
                            else{
                                this.indexSelected = 0
                            }
                        }
                        else{
                            if(this.indexSelected==null || this.indexSelected == this.dataArray.length - 1){
                                this.indexSelected = 0
                            }
                            //Fix bug: Khi dataArray thay đổi mà nhỏ hơn giá trị index đang set thì đổi index về 0
                            else if(this.indexSelected > this.dataArray.length - 1){
                                this.indexSelected = 0
                            }
                            //Chọn select tiếp theo bên dưới
                            else{
                                this.indexSelected++
                            }
                        }
                        console.log(myScroll);
                        if(myCombobox?.offsetTop >= 188){
                            myScroll.scrollTop = myScroll.scrollTop + 36
                        }
                        if(myCombobox?.offsetTop == ((this.dataRoot.length-1)*36+8)){
                            myScroll.scrollTop = 0
                        }
                        break;
    
                    //Khi ấn nút lên
                    case 'ArrowUp':
                        //Hiện combobox
                        if(this.showData == false){
                            if(this.modelValue != null && this.modelValue !=""){
                                this.dataArray = this.dataRoot
                                arrayId = this.dataArray.map(a=>{
                                    return this.propValue ? a?.[this.propValue] : a
                                })
                                this.indexSelected = arrayId.indexOf(this.modelValue)
                            }
                            else{
                                this.indexSelected = 0
                            }
                            this.showData = true
                        }
                        else{
                            if(this.indexSelected==null || this.indexSelected == 0 ){
                                this.indexSelected = this.dataArray.length - 1
                            }
                            else{
                                this.indexSelected--
                            }

                        }
                        if(myCombobox?.offsetTop == 8){
                            myScroll.scrollTop = (this.dataRoot.length-1)*36+8
                        }
                        if(myCombobox?.offsetTop <= ((this.dataRoot.length-6)*36+8) && myCombobox?.offsetTop !=8 ){
                            myScroll.scrollTop = myScroll.scrollTop - 36
                        }
                        // myCombobox = document.querySelector('.combobox__data .scrollbar .combobox__data-item.combobox__data-item--selected')
                        // myCombobox.scrollIntoView(true);
                        break;
    
                    //Khi ấn enter hoặc tab thì gán giá trị đã chọn vào ô input và đóng combobox
                    case 'Enter':
                        if(this.showData==false){
                            this.showData=true
                            this.dataArray = this.dataRoot
                            arrayId = this.dataArray.map(a=>{
                                return this.propValue ? a?.[this.propValue] : a
                            })
                            if(arrayId.indexOf(this.modelValue)==-1){
                                this.indexSelected = null
                            }
                            else{
                                this.indexSelected = arrayId.indexOf(this.modelValue)
                            }
                        }
                        else{
                            if(this.indexSelected || this.indexSelected==0){
                                this.selectDataItem(this.dataArray[this.indexSelected])
                            }
                        }
                        // console.log(this.showData,myScroll);
                        // myCombobox = document.querySelector('.combobox__data .scrollbar .combobox__data-item.combobox__data-item--selected')
                        // myScroll.scrollTop = this.indexSelected*36 +8
                        // myComboboxSelected.scrollIntoView();
                        break;
                    case 'Tab':
                        if(this.indexSelected || this.indexSelected==0){
                            this.selectDataItem(this.dataArray[this.indexSelected])
                        }
                        break;
                    default:
                        break;
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi blur ra ngoài thì t gửi một sự kiện lên component cha và đóng combobox
         * Author: Vũ Quốc Anh (01/04/2023)
        */
        blurInput(){
            this.$emit('blurInput')
            this.showData = false
        },

        changeScroll(){
            console.log("A");
        }
    },
}
</script>

<style>

</style>