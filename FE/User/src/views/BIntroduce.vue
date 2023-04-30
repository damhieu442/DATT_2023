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
              <h1 class="entry-title mb-0">GIỚI THIỆU</h1>
            </div>
            <div class="title-content flex-col">
              <div class="title-breadcrumbs pb-half pt-half">
                <nav class="woocommerce-breadcrumb breadcrumbs">
                  <router-link to="/">TRANG CHỦ</router-link>
                  <div class="divider">/</div>
                  GIỚI THIỆU
                </nav>
              </div>
            </div>
          </div>
        </div>
      </div>
      <section class="section sec_about" id="section_about">
        <div class="bg section-bg fill bg-fill bg-loaded"></div>
        <div class="section-content relative">
          <div
            class="gap-element clearfix"
            style="display: block; height: auto; padding-top: 50px"
          ></div>
          <div class="row align-center">
            <div class="col medium-6 small-12 large-4">
              <div class="col-inner">
                <div class="img" id="img_about">
                  <div class="img-inner dark">
                    <img
                      src="@/assets/img/about.jpg"
                      width="800"
                      height="1035"
                      alt=""
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-12 small-12 large-8">
              <div class="col-inner">
                <h1>GIỚI THIỆU</h1>
                <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                <h3>SẢN PHẨM GIÀY TỐT NHẤT</h3>
                <div class="row row-inner">
                  <div class="col col-left medium-6 small-12 large-6">
                    <div class="col-inner">
                      <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                    </div>
                  </div>
                  <div class="col col-right medium-6 small-12 large-6">
                    <div class="col-inner">
                      <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
      <section class="section sec_reason" id="section-reason">
        <div class="bg section-bg fill bg-fill bg-loaded"></div>
        <div class="section-content relative">
          <div
            class="gap-element clearfix"
            style="display: block; height: auto; padding-top: 50px"
          ></div>
          <div class="row">
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-truck fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Miễn phí giao hàng</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-hand-holding-dollar fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Đổi trả trong vòng 7 ngày</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-box-archive fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Sản phẩm mới 100%</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-comments fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Chăm sóc khách hàng</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-shield-halved fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Hàng chính hãng</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-6">
              <div class="col-inner">
                <div class="icon-box featured-box icon-box-left text-left">
                  <div
                    class="icon-box-img"
                    style="width: 50px; padding-top: 16px"
                  >
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-cart-shopping fa-2xl"
                          style="color: #c30005; font-size: 40px"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <h3>Thanh toán đa dạng</h3>
                    <p>{{ this.$BResource.INTRODUCE.TITLE }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div
            class="gap-element clearfix"
            style="display: block; height: auto; padding-top: 50px"
          ></div>
        </div>
      </section>
    </div>
  </main>
</template>

<script>
export default {
  name: "Introduce",
  components: {},
};
</script>

<style scoped>
@import url(@/css/views/BIntroduce.css);
</style>
