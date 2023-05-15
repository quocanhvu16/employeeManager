<template>
    <button class="m-btn">
        <i class="fa-sharp fa-solid fa-plus"></i>
        <p>{{text}}</p>
    </button>
</template>

<script>
export default {
    name:"MButton",
    component:{},
    props:["text"],
    data() {
        return {
        }
    },
}
</script>

<style>
@import url('./../../css/base/button.css');
</style>