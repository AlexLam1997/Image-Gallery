<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>

<template>
  <div class = "row">
      <img height="400px" width = "400px" v-for="img in images" :key= "img" v-bind:src="img"/>
  </div>
</template>

<script>
import axios from 'axios'

let loadImages = function(){
    var options = {};
    options.url = "https://localhost:44394/api/Images/1";
    options.type = "GET";
    options.dataType = "json";
    options.contentType = "application/json";

    axios(options).then(function(value){
        $("#imgContainer").append("<img src='https://localhost:44394/api/Images/get'"+ " /> <br />");
        // document.getElementById("ItemPreview").src = "https://localhost:44394/api/Images/get";
        console.log(value)
    })

    // axios.get("https://localhost:44394/api/Images/get")
}

export default {
  name: 'ImageDisplay',
  data(){
    return {
      selectedImage : null,
      images: ["https://localhost:44394/api/Images/get"]
    }
  },
  methods: {
      loadImages : loadImages
  }, 
  mounted(){
      this.loadImages()
  }
}
</script>

