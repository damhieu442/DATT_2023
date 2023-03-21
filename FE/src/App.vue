<template>
  <div id="wrapper">
    <Header />
    <Contact />
    <Footer />
  </div>
</template>

<script>
import Header from "@/components/layout/BHeader.vue";
import Footer from "@/components/layout/BFooter.vue";
import Contact from "@/views/BContact.vue";
export default {
  name: "App",
  components: { Header, Footer, Contact },
};
</script>

<style>
@import url(@/css/app.css);
</style>
