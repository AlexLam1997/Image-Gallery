<template>
  <div class="upload">
    <form id="form" name="form" enctype="multipart/form-data" method="post">
      <div class="buttons">
        <div class="upload-button">
          <div class="label">Click me!</div>
          <input id="files" name="files" type="file" size="1" multiple @change="uploadImage('files');" />
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import axios from 'axios'

let onFileSelected = function(event){
  this.selectedImage = event.target.files[0]
}
let uploadImage = async function(inputId){
  var input = document.getElementById(inputId);
  var files = input.files;
  var formData = new FormData();
  for (var i = 0; i != files.length; i++) {
    formData.append("files", files[i]);
  }
  this.axiosInstance.post("/upload", formData)
  this.notifyGallery()
}

export default {
  name: 'ImageUpload',
  data(){
    return {
      selectedImage : null,
      axiosInstance : axios.create({
          baseURL: 'https://localhost:44394/api/Images/',
          headers: {"Authorization" : `Bearer ${this.$store.getters.accessKey}`}
      })
    }
  },
  methods: {
    onFileSelected : onFileSelected, 
    uploadImage : uploadImage,
    notifyGallery(){
        this.$root.$emit('gallery')
    }
  }
}
</script>