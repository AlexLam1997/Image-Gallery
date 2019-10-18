export default {
    getHeader(){
        var options = {};
        options.type = "GET";
        options.dataType = "json";
        options.contentType = "application/json";
        options.headers = {"Authorization" : `Bearer ${this.$store.getters.accessKey}`}

    }
}