<template>
  <div>
    <vue-select-image :dataImages="Images"
        :is-multiple="true"
        @onselectmultipleimage="onSelectMultipleImage"
        :rootClass="'custom-style'">
    </vue-select-image>

    <div class="gallery">
      <div class ="gallery-panel" v-for="img in Images" :key="img.id">
          <img :src="img.src"/>
      </div>
      
    </div>
    <div class="delete-button" v-on:click="DeleteImages">Delete Selected Images</div>
  </div>
</template>

<script>
import axios from 'axios'
import VueSelectImage from 'vue-select-image'
require('vue-select-image/dist/vue-select-image.css')

let loadImages = function(){
  this.axiosInstance.get("/get").then( (resp) => {
    this.Images = resp.data.payload
    this.Images.forEach( (image) => {
      image.src = this.getImageUrl(image.storageName)
    })
  })
}

export default {
  name: 'Gallery',
  data() {
    return {
      Images: [],
      selectedImages: [],
      axiosInstance : axios.create({
          baseURL: 'https://localhost:44394/api/Images/',
          headers: {
            "Authorization" : `Bearer ${this.$store.getters.accessKey}`
            }
      })
    };
  },
  components:{
    VueSelectImage
  },
  methods: {
    loadImages: loadImages,
    getImageUrl(imagePath){
      return `https://localhost:44394/api/Images/get/${imagePath}`
    },
    onSelectMultipleImage (data) {
      this.selectedImages = data
    },
    DeleteImages(){
      let deletedImageGuids = []
      this.selectedImages.forEach( (image) => {
        deletedImageGuids.push(image.storageName)
        let imageIndex = this.Images.indexOf(image)
        if(imageIndex > -1){
          this.Images.splice(imageIndex, 1)
        }
      })
      this.axiosInstance.post("/delete", deletedImageGuids)

      // remove deleted images or reload
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

<style lang="scss">
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

  .vue-select-image {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    grid-gap: 1rem;
    max-width: 80rem;
    /* margin: auto auto; */
    padding: 0 5rem;
  }

  .delete-button {
  padding: 1rem;
  color: white;
  background-color: #FF0000;
  border-radius: .3rem;
  text-align: center;
  font-weight: bold;
  }

.custom-style {
  // display: flex;
  // justify-content: center;
  
  &__wrapper {
     overflow: auto;
    // list-style-image: none;
    // list-style-position: outside;
    // list-style-type: none;
    // padding: 0px;
    // margin: 0px;
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(20rem, 1fr));
    grid-gap: 1rem;
    max-width: 80rem;
    margin: 5rem auto;
    padding: 0 5rem;
  }
  &__item {
    margin: 0px 12px 12px 0px;
    float: left;
    @media only screen and (min-width: 1200px) {
      margin-left: 30px;
    }
    // width: 100%;
    // height: 22vw;
    // object-fit: cover;
    // border-radius: 0.75rem;
  }
  &__thumbnail{
    padding: 6px;
    border: 1px solid #dddddd;
    display: block;
    padding: 4px;
    line-height: 20px;
    border: 1px solid #ddd;
    // -webkit-border-radius: 50%;
    // -moz-border-radius: 50%;
    border-radius: 0.75rem;
    // -webkit-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.055);
    // -moz-box-shadow: 0 1px 3px rgba(0, 0, 0, 0.055);
    // box-shadow: 0 1px 3px rgba(0, 0, 0, 0.055);
    -webkit-transition: all 0.2s ease-in-out;
    -moz-transition: all 0.2s ease-in-out;
    -o-transition: all 0.2s ease-in-out;
    transition: all 0.2s ease-in-out;
    &--selected{
      background: #0088cc;
      .custom-style__img{
        zoom: 1.1;
      }
    }
  }
  &__img{
    -webkit-user-drag: none;
    display: block;
    margin-right: auto;
    margin-left: auto;
    -webkit-border-radius: 50%;
    -moz-border-radius: 50%;
    border-radius: 0.75rem;

    width: 100%;
    height: 22vw;
    object-fit: cover;
    // border-radius: 0.75rem;
  }
}
  </style>