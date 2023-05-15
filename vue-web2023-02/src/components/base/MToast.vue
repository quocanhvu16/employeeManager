<template>
  <div id="toast">
    <div class="toast toast--success">
      <div class="toast__icon">
          <i class="fa-solid fa-circle-check"></i>
      </div>
      <div class="toast__body">
          <p><span>{{toast.type}}!</span> {{toast.title}}</p>
      </div>
      <div class="toast__action">
          
      </div>
      <div class="toast__close" @click="HIDE_TOAST">
          <i class="fa-solid fa-xmark"></i>
      </div> 
    </div>
  </div>
</template>

<script>
import { mapActions, mapMutations, mapState } from 'vuex';

export default {
  name:"MToast",
  computed:{
    ...mapState(['toast']),
  },
  methods:{
    ...mapMutations(['HIDE_TOAST']),
    ...mapActions(['getEmployee']),
  }
}
</script>

<style>

</style>