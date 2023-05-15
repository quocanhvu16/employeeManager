<template>
    <div :class="data.class">
        <p :class="data.classTooltip" :data-c-tooltip="data['data-c-tooltip']">{{data.textInput}} <span v-show="data.class.includes('require')" style="color: #E81E1E;">*</span></p>
        <input :class="['input',data.error ? 'errorBorder' : '']" :tabindex="data.tabindex" :maxlength="data.maxLength"
            :type="data.typeInput" :placeholder="data.placeholder" v-model="input" @input="inputOnChange" :ref="data.ref" @blur="blurInput">
        <div class="error__text"><p>{{ data.error }}</p></div>
    </div>
</template>

<script>
export default {
    name: 'MInput',
    props:['data','modelValue'],
    data() {
        return {
            input:null,
            value : 0
        }
    },
    methods: {
        /**
         * Khi input thay đổi , cập nhật giá trị vào modelValue
         * Author: Vũ Quốc Anh (18/03/2023)
        */
        inputOnChange(){
            this.$emit("update:modelValue",this.input)
        },

        /**
         * Khi blur ra khỏi input , gọi sự kiện blur, áp dụng cho các input mặc định là không được để trống
         * Author: Vũ Quốc Anh (18/03/2023)
        */
        blurInput(){
            this.$emit('blur-input')
        },
    },
    watch: {
        /**
         * Theo dõi modelValue thay đổi thì gán giá trị vào ô input
         * Author: Vũ Quốc Anh (18/03/2023)
        */
        modelValue: function(newData){
            this.input = newData
        },

        /**
         * Theo dõi nếu ref thay đổi thì chuyển ô focus
         * Author: Vũ Quốc Anh (18/03/2023)
        */
        'data.refArray' : function(){
            if(this.data.refArray && this.data.refArray !==""){
                if(this.data.refArray == this.data.ref){
                    this.$refs[this.data.ref].focus()
                }
                if(this.data.refArray == this.data.ref){
                    this.$refs[this.data.ref].focus()
                }
            }
        },
    },
    mounted() {
        /**
         * Lúc mở form focus vào ô input đầu tiền và có ref là EmployeeCode
        */
        if(this.data.ref == "EmployeeCode"){
            this.$refs.EmployeeCode.focus()
        }
    },
}
</script>

<style>

</style>