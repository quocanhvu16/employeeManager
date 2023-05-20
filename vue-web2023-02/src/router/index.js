import {createRouter,createWebHistory} from 'vue-router'
import MainEmployee from './../components/layout/employee/MainEmployee.vue'
import MainMoney from './../components/layout/money/MainMoney.vue'

//Gán các component vào router
const routes = [
    {
        path: '/overview',
        name: 'home',
        component: MainMoney
    },
    {
        path : "/",
        name: 'employee',
        component: MainEmployee
    },
    {
        path : "/money1",
        name: 'money1',
        component: MainMoney
    },
    {
        path : "/money2",
        name: 'money2',
        component: MainMoney
    },
    {
        path : "/money3",
        name: 'money3',
        component: MainMoney
    },
    {
        path : "/money4",
        name: 'money4',
        component: MainMoney
    },
    {
        path : "/money5",
        name: 'money5',
        component: MainMoney
    },
    {
        path : "/money6",
        name: 'money6',
        component: MainMoney
    },
    {
        path : "/money7",
        name: 'money7',
        component: MainMoney
    },
    {
        path : "/money8",
        name: 'money8',
        component: MainMoney
    },
    {
        path : "/money9",
        name: 'money9',
        component: MainMoney
    },
    {
        path : "/money10",
        name: 'money10',
        component: MainMoney
    },

]

//Khởi tạo router
const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router;