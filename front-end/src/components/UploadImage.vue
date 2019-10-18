<template>
    <label class="file-select">
    <div class="select-button">
      <span v-if="value">Selected File: {{value.name}}</span>
      <span v-else>Select File</span>
    </div>
    <input id="files" name="files" type="file" size="1" multiple @change="uploadImage('files');" />
  </label>

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

<style>
.file-select > .select-button {
  padding: 1rem;

  color: white;
  background-color: #1E90FF;

  border-radius: .3rem;

  text-align: center;
  font-weight: bold;
}

.file-select > input[type="file"] {
  display: none;
}
</style>