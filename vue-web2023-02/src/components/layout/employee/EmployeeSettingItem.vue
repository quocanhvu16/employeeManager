<template>
    <div :class="['item',checkbox==true ? 'active':'']">
        <input type="checkbox" class="checkbox" v-model="checkbox" @change="changeCheckbox">
        <span class="checkmark"></span>
        <p>{{name}}</p>
    </div>
</template>

<script>
import { mapMutations, mapState } from 'vuex';

export default {
    name: 'EmployeeSettingItem',
    components: {
    },
    props:['name','propText','modelValue'],
    data() {
        return {
            checkbox:false
        }
    },
    mounted() {
        this.checkbox = this.modelValue
    },
    watch: {
        /**
         * Theo dõi sự thay đổi của checkbox và gán giá trị mới
         * Author: Vũ Quốc Anh (12/03/2023)
        */
        modelValue:{
            handler:function(newData){
                this.checkbox = newData
            },
            deep:true
        }
    },
    computed:{
        ...mapState(['settings']),
    },
    methods: {
        ...mapMutations(['SHOW_SETTING','HIDE_SETTING','CHANGE_SETTING']),

        /**
         * Thay đổi checkbox ở bên trong thì truyên ra bên ngoài để component cha update giá trị mới
         * Author: Vũ Quốc Anh (12/03/2023)
        */
        changeCheckbox(){
            try {
                this.$emit("update:modelValue",this.checkbox)
            } catch (error) {
                console.log(error)
            }
        }
    },
}
</script>

<style>

</style>