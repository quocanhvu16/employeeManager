<template>
    <div :class="['main', collapseMenu ? 'collapse' : '']">
          <router-view></router-view>
    </div>
  </template>
  
  <script>
import { mapState } from 'vuex';

  export default {
      name: 'TheMain',
      components:{
      },
      computed:{
        ...mapState(['collapseMenu']),
    },
  }
  </script>
  
  <style>
  
  </style>