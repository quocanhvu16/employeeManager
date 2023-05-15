<template>
  <div class="sort" v-if="classIcon == 'desc' ||classIcon == 'asc' ? true : false">
    <div :class="['sort-icon', classIcon]">

    </div>
  </div>
</template>

<script>
import MISAEnum from '@/js/base/MISAEnum'
export default {
    name:"MSort",
    props:['sort'],
    data() {
        return {
            classIcon: null
        }
    },
    watch: {
        /**
         * Theo dõi sự thay đổi của sắp xếp để thay đổi icon tương ứng
         * Author: Vũ Quốc Anh (28/04/2023)
        */
        sort: function(newSort){
            this.classIcon = newSort==MISAEnum.SortMode.desc? 'desc' : newSort==MISAEnum.SortMode.asc? 'asc' : ''
        }
    },
}
</script>

<style>

</style>