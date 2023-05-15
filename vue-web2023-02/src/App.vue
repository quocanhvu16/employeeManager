<template>
  <div class="container" v-ctrl1="showForm">
    <TheHeader/>
    <TheMenu/>
    <TheMain/>
  </div>
</template>

<script>
import TheHeader from './components/TheHeader.vue';
import TheMenu from './components/TheMenu.vue';
import TheMain from './components/TheMain.vue';
import { mapMutations} from 'vuex';
export default {
name: 'App',
components: {
  TheHeader,
  TheMenu,
  TheMain
},

methods: {
  ...mapMutations(['SHOW_FORM']),
  showForm(){
    this.$store.commit('SHOW_FORM')
  }
},
}
</script>

<style>

</style>
