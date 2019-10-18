<template>
<div>
  <Gallery />
  <ImageUpload/>
</div>
</template>

<script>
import Gallery from '@/components/Gallery.vue';
import ImageUpload from '@/components/UploadImage.vue' 

export default {
  name: 'home',
  components: {
    Gallery,
    ImageUpload
  },
};
</script>