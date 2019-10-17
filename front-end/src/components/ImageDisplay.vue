<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>

<template>
  <div>
    <div id="imgContainer"></div>
    <img id="ItemPreview" src="" />
  </div>
</template>

<script>
import axios from 'axios'

let loadImages = function(){
    // axios.get("https://localhost:44394/api/Images/1").then(function(value){
    //   document.getElementById("ItemPreview").src = "data:image/png;base64," + value;
    //   console.log(value)
    // })

    var options = {};
  options.url = "https://localhost:44394/api/Images/1";
  options.type = "GET";
  options.dataType = "json";
  options.contentType = "application/json";
// //   options.success = function (results) {
// //   $("#imgContainer").empty();
// //     for (var i = 0; i < results.length; i++) {
// //       $("#imgContainer").append("<img src='" + "api/Images/" + results[i] + "' /> <br />");
// //     }
// //   };
//   options.success = function () {
//     $(document).ready(function () {
//     $("#imgContainer").empty();
//     $("#imgContainer").append("<img src='" + "api/Images/1" + "' /> <br />");    });
//   };
//   options.error = function (err) { alert(err.statusText); };

  axios(options).then(function(value){
      document.getElementById("ItemPreview").src = "data:image/png;base64," + value.data.payload.data;
      console.log(value)
    })
}

export default {
  name: 'ImageDisplay',
  data(){
    return {
      selectedImage : null
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

