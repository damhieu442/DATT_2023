<template>
  <main id="main">
    <div id="content">
      <div id="page-header" class="page-header-wrapper">
        <div class="page-title dark featured-title">
          <div class="page-title-bg">
            <div
              class="title-bg fill bg-fill parallax-active"
              data-parallax-container=".page-title"
              data-parallax-background
              data-parallax="-"
            ></div>
            <div class="title-overlay fill"></div>
          </div>
          <div
            class="page-title-inner container align-center text-center flex-row-col medium-flex-wrap"
          >
            <div class="title-wrapper uppercase flex-col">
              <h1 class="entry-title mb-0">LIÊN HỆ</h1>
            </div>
            <div class="title-content flex-col">
              <div class="title-breadcrumbs pb-half pt-half">
                <nav class="woocommerce-breadcrumb breadcrumbs">
                  <a href="#">TRANG CHỦ</a>
                  <div class="divider">/</div>
                  LIÊN HỆ
                </nav>
              </div>
            </div>
          </div>
        </div>
      </div>
      <section class="section sec_lien_he" id="section_contact">
        <div class="bg section-bg fill bg-fill bg-loaded"></div>
        <div class="section-content relative">
          <div
            class="gap-element clearfix"
            style="display: block; height: auto; padding-top: 60px"
          ></div>
          <div class="row">
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <h3>THÔNG TIN LIÊN HỆ</h3>
                <div
                  class="icon-box featured-box icon-box-left text-left"
                  style="margin: 0px 0px 10px 0px"
                >
                  <div class="icon-box-img" style="width: 20px">
                    <div class="icon">
                      <div class="icon-inner">
                        <i class="fa-solid fa-location-dot location"></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <p>
                      Ngõ 70 Xuân Phương, Phường Phương Canh, Quận Nam Từ Liêm,
                      Hà Nội
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="row row-collapse row-full-width">
            <div class="col small-12 large-12">
              <div class="col-inner" style="margin: 0px 0px -25px 0px">
                <p>
                  <DxMap
                    :zoom="16"
                    :height="400"
                    :controls="true"
                    v-model:markers="markersData"
                    v-model:marker-icon-src="markersIcon"
                    width="100%"
                    provider="bing"
                  />
                </p>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  </main>
</template>
<script>
import DxMap from "devextreme-vue/map";
import { markersData } from "@/js/constant";
const markerUrl =
  "https://js.devexpress.com/Demos/WidgetsGallery/JSDemos/images/maps/map-marker.png";

export default {
  name: "Contact",
  components: { DxMap },
  data() {
    return {
      markersData,
      markersIcon: markerUrl,
    };
  },
};
</script>
<style scoped>
@import url(@/css/views/BContact.css);
</style>
