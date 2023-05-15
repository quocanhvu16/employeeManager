<template>
    <div :class="['menu', collapseMenu ? 'collapse' : '']">
        <div class="menu__category">
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.overview" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.overview" 
                        :classIcon="'frame__icon1'" 
                        :name="'/'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.cash" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.cash" 
                        :classIcon="'frame__icon2'" 
                        :name="'/money'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.deposits" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.deposits" 
                        :classIcon="'frame__icon3'" 
                        :name="'/money1'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.buy" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.buy" 
                        :classIcon="'frame__icon4'" 
                        :name="'/money2'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.sell" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.sell" 
                        :classIcon="'frame__icon5'" 
                        :name="'/money3'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.invoiceManagement" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.invoiceManagement" 
                        :classIcon="'frame__icon6'" 
                        :name="'/money4'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.warehouse" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.warehouse" 
                        :classIcon="'frame__icon7'" 
                        :name="'/money10'
                        "/>
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.fixedAssets" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.fixedAssets" 
                        :classIcon="'frame__icon8'" 
                        :name="'/money5'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.price" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.price" 
                        :classIcon="'frame__icon9'" 
                        :name="'/money6'"
                        />
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.budget" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.budget" 
                        :classIcon="'frame__icon10'"
                         :name="'/money7'
                         "/>
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.report" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.report" 
                        :classIcon="'frame__icon11'"
                         :name="'/money8'
                         "/>
            <MenuCategory :text="MISAResource[this.langCode].categoryMenu.financialAnalysis" 
                        :data-c-tooltip="MISAResource[this.langCode].categoryMenu.financialAnalysis" 
                        :classIcon="'frame__icon12'"
                         :name="'/money9'
                         "/>
        </div>
        <div :class="['menu__collect', collapseMenu ? 'collapse' : '']" :data-c-tooltip="MISAResource[this.langCode].categoryMenu.enlarge" @click='collapse'>
            <div :class="['menu__collect--frame']">
                <div class="icon"></div>
            </div>
            <div class="menu__collect--text">
                <p>{{MISAResource[this.langCode].categoryMenu.collapse}}</p>
            </div>
        </div>
    </div>
</template>

<script>
import { mapMutations, mapState } from 'vuex'
import MenuCategory from './layout/menu/MenuCategory.vue'
import MISAResource from '@/js/base/MISAResource'

export default {
  name: 'TheMenu',
  components: {
    MenuCategory,
  },
  data(){
    return {
        MISAResource : MISAResource
    }
  },
  computed: {
    ...mapState(['collapseMenu','langCode'])
  },
  methods:{
    ...mapMutations(['CHANGE_COLLAPSE_MENU']),
    /**
     * Thu gọn, phóng to menu
     * Author : Vũ Quốc Anh (17/03/2023)
    */
    collapse: function(){
        try {
            this.$store.commit('CHANGE_COLLAPSE_MENU')
            /**
             * Thêm class vào icon hiện sang trái, kiểm tra xem nếu menu
             * đang thu gọn thì sẽ là icon hiện sang phải và ngược lại
             */
            const icon = document.querySelector('.menu__collect--frame')
            icon.classList.toggle('collapse')
            if(icon.classList.contains('collapse')){
                icon.style.animation = 'turn180Right linear 0.3s forwards'
            }
            if(!icon.classList.contains('collapse')){
                icon.style.animation = 'turn180Left linear 0.3s forwards'
            }       
        } catch (error) {
            console.log(error);
        }
    }
  }
}


</script>

<style>

</style>