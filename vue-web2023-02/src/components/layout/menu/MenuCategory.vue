<template>
    <router-link :to="name" :class="['menu__category--list', collapseMenu ? 'collapse' : '']">
        <div class="frame">
            <div class="frame__icon" :class="classIcon"></div>
        </div>
        <div class="name">
            <p>{{text}}</p>
        </div>
    </router-link>
</template>

<script>
import { mapState } from 'vuex';

export default {
    name:"MenuCategory",
    component:{},
    props:["text","classIcon","name"],
    computed: {
        ...mapState(['collapseMenu'])
    },
}
</script>

<style>
</style>