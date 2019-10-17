<template>
  <div class="hello">
    <!-- <input type = "file" @change="onFileSelected">
    <button v-on:click="uploadImage()">Upload Image</button> -->

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

  // let fd = new FormData()
  // fd.append('image', this.selectedImage, this.selectedImage.name)
  axios.post("https://localhost:44394/api/Images/upload", formData)
}

export default {
  name: 'ImageUpload',
  data(){
    return {
      selectedImage : null
    }
  },
  methods: {
    onFileSelected : onFileSelected, 
    uploadImage : uploadImage
  }
}
</script>

