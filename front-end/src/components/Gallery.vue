<template>
  <div class="gallery">
    <div class="gallery-panel"
         v-for="photo in Images"
         :key="photo.id">
      <router-link :to="`/photo/${photo.id}`">
        <img :src="getImageUrl(photo.storageName)">
      </router-link>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

let loadImages = function(){
  this.axiosInstance.get("/get").then( (resp) => {
      this.Images = resp.data.payload
  })
}

export default {
  name: 'Gallery',
  data() {
    return {
      Images: [],
      axiosInstance : axios.create({
          baseURL: 'https://localhost:44394/api/Images/',
          headers: {"Authorization" : `Bearer ${this.$store.getters.accessKey}`}
      })
    };
  },
  methods: {
    loadImages: loadImages,
    getImageUrl(imagePath){
      return `https://localhost:44394/api/Images/get/${imagePath}`
    }
  },
  mounted(){
    this.loadImages()
    this.$root.$on('gallery', () => {
     this.loadImages()
    })
  }
};
</script>

<style>
  .gallery {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    grid-gap: 1rem;
    max-width: 80rem;
    margin: 5rem auto;
    padding: 0 5rem;
  }
  .gallery-panel img {
    width: 100%;
    height: 22vw;
    object-fit: cover;
    border-radius: 0.75rem;
  }
</style>