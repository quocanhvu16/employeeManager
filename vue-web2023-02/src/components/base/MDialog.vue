<template>
    <div class="overlay-dialog active">
        <div class="dialog">
            <div class="dialog__header">
                <p>{{header}}</p>
                <div class="close tooltip-down" @click="clickClose" :data-c-tooltip="MISAResource[langCode].tooltip.exitAndQuickKeys" v-esc="clickClose">
                    <i class="fa-solid fa-xmark"></i>
                </div>
            </div>
            <div class="dialog__body">
                <div :class="['dialog__body--icon',classIcon]"></div>
                <div class="dialog__body--content">
                    <p>
                        {{ content }} <span style="font-family: RobotoBold;" v-html="dataDialog?.data?.employeeCode"></span> ?
                    </p>
                </div>
            </div>
            <div class="dialog__footer">
                <div class="dialog__footer--left">
                    <MButton class="m-btn-secondary" :text="btn.btnLeft.text" @click="clickBtnLeft" v-show="btn.btnLeft.active"/>
                </div>
                <div class="dialog__footer--right">
                    <MButton class="m-btn-secondary" :text="btn.btnRight1.text" @click="clickBtn1" v-show="btn.btnRight1.active"/>
                    <MButton class="m-btn-main" :text="btn.btnRight2.text" @click="clickBtn2" v-show="btn.btnRight2.active"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import MISAEnum from '@/js/base/MISAEnum';
import MISAResource from '@/js/base/MISAResource';
// import form from '@/js/form';
import { mapActions, mapGetters, mapMutations, mapState } from 'vuex';

export default {
    name:"MDialog",
    component:{},
    props:[ "dataDialog"],
    data() {
        return {
            MISAEnum : MISAEnum,
            MISAResource: MISAResource,
            header:"",
            content:"",
            btn:{
                btnLeft: {
                    active:false,
                    text:""
                },
                btnRight1: {
                    active:false,
                    text:""
                },
                btnRight2: {
                    active:false,
                    text:""
                }
            },
            classIcon: ""
        }
    },
    created(){
        //Nếu dialogMode là dữ liệu không hợp lệ thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.dataInvalid){
            this.header = MISAResource[this.langCode].headerDialogModeDataInvalid
        }
        //Nếu dialogMode là xóa nhân viên thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.removeEmployee){
            this.header =  MISAResource[this.langCode].headerDialogModeRemoveEmployee
            this.content = MISAResource[this.langCode].contentDialogModeRemoveEmployee
            this.btn.btnLeft.active = true
            this.btn.btnLeft.text = MISAResource[this.langCode].button.no
            this.btn.btnRight1.active = false
            this.btn.btnRight2.active = true
            this.btn.btnRight2.text = MISAResource[this.langCode].button.yes
            this.classIcon = "warning"
        }
        //Nếu dialogMode là xóa nhiều nhân viên thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.removeEmployees){
            this.header =  MISAResource[this.langCode].headerDialogModeRemoveEmployees
            this.content = MISAResource[this.langCode].contentDialogModeRemoveEmployees
            this.btn.btnLeft.active = true
            this.btn.btnLeft.text = MISAResource[this.langCode].button.exit
            this.btn.btnRight1.active = true
            this.btn.btnRight1.text = MISAResource[this.langCode].button.no
            this.btn.btnRight2.active = true
            this.btn.btnRight2.text = MISAResource[this.langCode].button.yes
            this.classIcon = "warning"
        }
        //Nếu dialogMode là dữ liệu bị thay đổi thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.dataChange){
            this.header =  MISAResource[this.langCode].headerDialogModeDataChange
            this.content = MISAResource[this.langCode].contentDialogModeDataChange
            this.btn.btnLeft.active = true
            this.btn.btnLeft.text = MISAResource[this.langCode].button.exit
            this.btn.btnRight1.active = true
            this.btn.btnRight1.text = MISAResource[this.langCode].button.no
            this.btn.btnRight2.active = true
            this.btn.btnRight2.text = MISAResource[this.langCode].button.yes
            this.classIcon = "notification"
        }
        //Nếu dialogMode là thay đổi tùy chỉnh cột thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.settingsChange){
            this.header =  MISAResource[this.langCode].headerDialogModeSettingsChange
            this.content = MISAResource[this.langCode].contentDialogModeSettingsChange
            this.btn.btnLeft.active = true
            this.btn.btnLeft.text = MISAResource[this.langCode].button.exit
            this.btn.btnRight1.active = true
            this.btn.btnRight1.text = MISAResource[this.langCode].button.no
            this.btn.btnRight2.active = true
            this.btn.btnRight2.text = MISAResource[this.langCode].button.yes
            this.classIcon = "notification"
        }
        //Nếu dialogMode là thay đổi setting về mặc định thì khai báo header với content
        if(this.dataDialog.dialogMode == MISAEnum.DialogMode.settingsDefault){
            this.header =  MISAResource[this.langCode].headerDialogModeSettingsDefault
            this.content = MISAResource[this.langCode].contentDialogModeSettingsDefault
            this.btn.btnLeft.active = true
            this.btn.btnLeft.text = MISAResource[this.langCode].button.exit
            this.btn.btnRight1.active = false
            this.btn.btnRight1.text = MISAResource[this.langCode].button.no
            this.btn.btnRight2.active = true
            this.btn.btnRight2.text = MISAResource[this.langCode].button.yes
            this.classIcon = "notification"
        }
    },
    computed:{
        ...mapState(['dialogMode','employeeRemove','employeeEdit','langCode']),
        ...mapGetters(['formatErrors'])
    },
    methods:{
        ...mapMutations(['HIDE_DIALOG','HIDE_FORM','SHOW_DIALOG','HIDE_SETTING','SET_ERRORS']),
        ...mapActions(['getEmployee','removeEmployee','showToastSuccess','editEmployee']),

        /**
         * Click vào icon CLOSE
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        clickClose(){
            try {
                this.$emit('close-dialog')
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Click vào button ở bên trái
         * Author: Vũ Quốc Anh (25/03/2023)
        */
        clickBtnLeft(){
            try {
                this.$emit('close-dialog')
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Ấn vào button thứ nhất sẽ ẩn form và ẩn dialog
         * Author: Vũ Quốc Anh (28/03/2023)
        */
        clickBtn1(){
            try {
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.dataChange){
                    this.$emit('change-noaccept')
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.removeEmployees){
                    this.$emit('removes-noaccept')
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.settingsChange){
                    this.$emit('setting-noaccept')
                }
            } catch (error) {
                console.log(error);
            }
        },

        /**
         * Ấn vào button thứ hai
         * Author: Vũ Quốc Anh (28/03/2023)
        */
        async clickBtn2(){
            try {
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.dataChange){
                    this.$emit('change-accept')
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.removeEmployees){
                    this.$emit('removes-accept')
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.removeEmployee){
                    this.$emit('remove-accept',this.dataDialog.data.employeeId)
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.settingsChange){
                    this.$emit('setting-accept')
                }
                if(this.dataDialog.dialogMode == MISAEnum.DialogMode.settingsDefault){
                    this.$emit('default-setting')
                }
            } catch (error) {
                console.log(error);
            }
        },
    }
}
</script>

<style>

</style>