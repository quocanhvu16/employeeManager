import { createApp } from 'vue'
import App from './App.vue'

//Import các component dùng chung
import MButton from './components/base/MButton.vue'
import MTableRow from './components/base/MTableRow.vue'
import MTable from './components/base/MTable.vue'
import MLoading from './components/base/MLoading.vue'
import MToast from './components/base/MToast.vue'
import MCombobox from './components/base/MCombobox.vue'
import MDialog from './components/base/MDialog.vue'
import MSort from './components/base/MSort.vue'
import VueNumberFormat from 'vue-number-format'
import MInput from './components/base/MInput.vue'
import MFilter from './components/base/MFilter.vue'
// import Microtip from 'microtip'

//Import các directive dùng chung
import {ctrl1,clickOutside,enter,esc,insert,ctrld,actionPress} from './js/directive'

//Import các thư viện ngoài
import router from './router/index'
import store from './store'

createApp(App)  
    .component("MButton",MButton)
    .component("MTableRow",MTableRow)
    .component("MTable",MTable)
    .component("MLoading",MLoading)
    .component("MToast",MToast)
    .component("MCombobox",MCombobox)
    .component("MDialog",MDialog)
    .component("MFormatMoney",VueNumberFormat)
    .component("MSort",MSort)
    .component("MInput",MInput)
    .component("MFilter",MFilter)
    .use(store)
    .use(router)
    .directive('click-outside',clickOutside)
    .directive('esc',esc)
    .directive('enter',enter)
    .directive('ctrl1',ctrl1)
    .directive('insert',insert)
    .directive('ctrld',ctrld)
    .directive('actionpress',actionPress)
    // .directive('VTooltip',VTooltip)
    .mount('#app')
