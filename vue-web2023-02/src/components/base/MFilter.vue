<template>
    <div class="filter" :data-c-tooltip="MISAResource[langCode].tooltip.filterData" @click="clickFilter" v-click-outside="hideFilter">
        <div :class="['filter-icon',isFilter==true?'active':'']"></div>
        <div class="filter-wrapper" style="">
            <div class="filter-header">
                {{ MISAResource[langCode].filter.header }}
            </div>
            <div class="filter-content">
                <MCombobox :propText="'filterName'" :propValue="'filterId'" :id="'filter'" :refs="'Filter'" v-model="filterMode" :placeholder="this.datas.placeholder"
                    :data="this.datas.filterModes"
                />
                <input v-if="this.datas.type != 'combobox'" :type="this.datas.type" 
                        class="filtet-text" :placeholder="MISAResource[langCode].input.placeholder.filter" :readonly="[null,1,2].includes(filterMode)" 
                        v-model="filterText">
                <MCombobox v-if="this.datas.type == 'combobox'" :propText="this.datas.propText" 
                            :propValue="this.datas.propValue" :id="this.datas.id" :refs="this.datas.refs" 
                            v-model="filterText" :placeholder="MISAResource[langCode].input.placeholder.filter" :data="this.datas.data" :api="this.datas.api"
                />
            </div>
            <div class="filter-button">
                <MButton :text="MISAResource[langCode].button.removeFilter" :class="'m-btn-secondary'" @click.stop.prevent="clickUnfiltered"/>
                <MButton :text="MISAResource[langCode].button.applyFilter" :class="'m-btn-main'" @click.stop.prevent="clickApply"/>
            </div>
        </div>
    </div>
</template>
  
<script>
import MISAEnum from '@/js/base/MISAEnum'
import MISAResource from '@/js/base/MISAResource';
import { mapState } from 'vuex';
  export default {
    name:"MFilter",
    props:['modelValue','datas'],
    data() {
        return {
            isFilter: false,
            filterMode:null,
            filterText:null,
            filterRoot:{
                filterMode:null,
                filterText:null
            },
            MISAEnum : MISAEnum,
            MISAResource: MISAResource
        }
    },
    computed: {
        ...mapState(['langCode'])
    },
    watch: {
        /**
         * Theo dõi sự thay đổi của mode bộ lọc , nếu như là lọc kiểu trống hoạc không trống thì sẽ ko cho nhập giá trị lọc
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        filterMode:function(){
            try {
                if([null,1,2].includes(this.filterMode)){
                    this.filterText = null
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Khi xóa 1 bộ lọc thì component cha , thì theo dõi xem xóa bộ lọc nào sẽ cho mode với giá trị lọc về null
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        modelValue:{
            handler: function(newData){
                try {
                    var bool = false
                    for(const key in newData){
                        if(key == this.datas.columnName){
                            bool = true
                        }
                    }
                    if(bool == false){
                        this.filterMode=null
                        this.filterText = null
                        this.filterRoot = {
                            filterMode: this.filterMode,
                            filterText: this.filterText
                        }
                    }
                } catch (error) {
                    console.log(error);
                }
            },
            deep: true
        }
    },
    methods: {
        /**
         * Hiện bộ lọc dữ liệu
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        clickFilter(){
            try {
                this.isFilter = true
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Ẩn bộ lọc dữ liệu và gán giá trị lọc vào root
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        hideFilter(){
            try {
                this.isFilter = false
                this.filterMode = this.filterRoot.filterMode
                this.filterText = this.filterRoot.filterText
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Án vào bỏ lọc thì sẽ set tất cả về nill
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        clickUnfiltered(){
            try {
                this.isFilter = false
                this.filterMode=null
                this.filterText = null
                this.filterRoot = {
                    filterMode: this.filterMode,
                    filterText: this.filterText
                }
                var value = {
                    ...this.modelValue,
                }
                delete value[this.datas.columnName]
                this.$emit("update:modelValue",value)
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Ấn vào áp dụng lọc thì sẽ set vào modelValue mode và giá trị của bộ lọc
         * Tuy nhiên có một số trường hợp ngoại lệ như nhập giá trị mà ko nhập mode thì sẽ đưa cả 2 về null
         * Author: Vũ Quốc Anh (29/04/2023)
        */
        clickApply(){
            try {
                this.isFilter = false
                var value
                if(this.datas.columnName != 'Gender'){
                    if(this.filterText === null || this.filterText?.trim() === ''){
                        this.filterText = null
                        if(this.filterMode !=1 &&this.filterMode !=2 ){
                            this.filterMode = null
                        }
                    }
                }
                if(this.filterMode == null){
                    this.filterText = null
                }
                this.filterRoot = {
                    filterMode: this.filterMode,
                    filterText: this.filterText
                }
                value = {
                    ...this.modelValue,
                    [this.datas.columnName] : this.filterRoot.filterMode + (this.filterRoot.filterText !==null ? `-${this.filterRoot.filterText}` : '')
                }
                if(this.filterMode == null){
                    delete value[this.datas.columnName]
                }
                this.$emit("update:modelValue",value)
            } catch (error) {
                console.log(error);
            }
        }
    },
  }
</script>
  
<style>
  
</style>