<template>
  <div id="wrapper">
    <Header />
    <Introduce />
    <Footer />
  </div>
</template>

<script>
import Header from "@/components/layout/BHeader.vue";
import Footer from "@/components/layout/BFooter.vue";
import Introduce from "@/views/BIntroduce.vue";
import Contact from "@/views/BContact.vue";

export default {
  name: "App",
  components: { Header, Footer, Contact, Introduce },
};
</script>

<style>
@import url(@/css/app.css);
</style>
