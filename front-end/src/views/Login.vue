<template>
  <Login />
</template>

<script>
import Login from '@/components/Login.vue';

export default {
  name: 'login',
  components: {
    Login,
  },
};
</script>

<style>
    body {
        background-color: #F0F0F0;
    }
    h1 {
        padding: 0;
        margin-top: 0;
    }
    #app {
        width: 1024px;
        margin: auto;
    }
</style>