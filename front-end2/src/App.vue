<template>
  <div id="app">
    <ImageUpload/>
    <ImageDisplay/>
  </div>
</template>

<script>
import ImageUpload from './components/ImageUpload.vue'
import ImageDisplay from './components/ImageDisplay.vue'

export default {
  name: 'app',
  components: {
    ImageUpload, 
    ImageDisplay
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
