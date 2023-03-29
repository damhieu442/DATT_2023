<template>
  <footer id="footer" class="footer-wrapper">
    <section id="section_footer" class="section sec_footer dark">
      <div class="bg section-bg fill bg-fill bg-loaded"></div>
      <div class="section-content relative">
        <div
          class="gap-element clearfix"
          style="display: block; height: auto; padding-top: 40px"
        >
          <div class="row align-center row-top">
            <div class="col medium-6 small-12 large-3">
              <div class="col-inner">
                <h3>{{ this.$BResource.INTRODUCE.CAPTION }}</h3>
                <div
                  class="is-divider divider clearfix"
                  style="max-width: 35px; background-color: rgb(195, 0, 5)"
                ></div>
                <p>
                  {{ this.$BResource.INTRODUCE.TITLE }}
                </p>
              </div>
            </div>
            <div class="col medium-6 small-12 large-3">
              <div class="col-inner">
                <h3>{{ this.$BResource.ADDRESS.CAPTION }}</h3>
                <div
                  class="is-divider divider clearfix"
                  style="max-width: 35px; background-color: rgb(195, 0, 5)"
                ></div>
                <div class="icon-box featured-box icon-box-left text-left">
                  <div class="icon-box-img">
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-location-dot"
                          style="color: #ffffff"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <p>
                      {{ this.$BResource.ADDRESS.TITLE }}
                    </p>
                  </div>
                </div>
                <div class="icon-box featured-box icon-box-left text-left">
                  <div class="icon-box-img">
                    <div class="icon">
                      <div class="icon-inner">
                        <i class="fa-solid fa-phone" style="color: #ffffff"></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <p>
                      <a href="tel:0384144442">{{
                        this.$BResource.ADDRESS.PHONE
                      }}</a>
                    </p>
                  </div>
                </div>
                <div class="icon-box featured-box icon-box-left text-left">
                  <div class="icon-box-img">
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-solid fa-envelope"
                          style="color: #ffffff"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <p>
                      <a href="mailto:hieudam442@gmail.com">{{
                        this.$BResource.ADDRESS.EMAIL
                      }}</a>
                    </p>
                  </div>
                </div>
                <div class="icon-box featured-box icon-box-left text-left">
                  <div class="icon-box-img">
                    <div class="icon">
                      <div class="icon-inner">
                        <i
                          class="fa-brands fa-skype"
                          style="color: #ffffff"
                        ></i>
                      </div>
                    </div>
                  </div>
                  <div class="icon-box-text last-reset">
                    <p>
                      <a href="skype:live:.cid.c2563e8ec37aad03">{{
                        this.$BResource.ADDRESS.SKYPE
                      }}</a>
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <div class="col medium-6 small-12 large-3">
              <div class="col-inner">
                <h3>{{ this.$BResource.MENU.CAPTION }}</h3>
                <div
                  class="is-divider divider clearfix"
                  style="max-width: 35px; background-color: rgb(195, 0, 5)"
                ></div>
                <ul class="sidebar-wrapper ul-reset">
                  <div class="mona_sidebar_footer">
                    <div class="menu-menu-footer-company-container">
                      <ul id="menu-menu-footer-company" class="menu">
                        <li><a href="#">Trang chủ</a></li>
                        <li><a href="#">Giới thiệu</a></li>
                        <li><a href="#">Cửa hàng</a></li>
                        <li><a href="#">Liên hệ</a></li>
                      </ul>
                    </div>
                  </div>
                </ul>
              </div>
            </div>
            <div class="col medium-6 small-12 large-3">
              <div class="col-inner">
                <h3>{{ this.$BResource.SOCIAL.CAPTION }}</h3>
                <div
                  class="is-divider divider clearfix"
                  style="max-width: 35px; background-color: rgb(195, 0, 5)"
                ></div>
                <div class="social-icons follow-icons" style="font-size: 50%">
                  <a
                    href="https://www.facebook.com/hieudam2kx/"
                    target="_blank"
                    data-label="Facebook"
                    rel="noopener noreferrer nofollow"
                    class="icon primary button circle facebook tooltip tooltipstered"
                    ><i
                      class="fa-brands fa-facebook-f icon-facebook"
                      style="color: #ffffff"
                    ></i
                  ></a>
                  <a
                    href="https://www.instagram.com/vp_melody/"
                    target="_blank"
                    data-label="Facebook"
                    rel="noopener noreferrer nofollow"
                    class="icon primary button circle facebook tooltip tooltipstered"
                  >
                    <i
                      class="fa-brands fa-instagram icon-instagram"
                      style="color: #ffffff"
                    ></i>
                  </a>
                </div>
              </div>
            </div>
          </div>
          <div class="row align-equal">
            <div class="col medium-6 small-12 large-4">
              <div class="col-inner text-right">
                <div
                  class="row large-columns-5 medium-columns-6 small-columns-5 row-collapse"
                >
                  <div class="gallery-col col">
                    <div class="col-inner">
                      <a class="image-lightbox lightbox-gallery">
                        <div class="box has-hover gallery-box box-none">
                          <div class="box-image">
                            <img src="@/assets/img/paypal.png" alt="" />
                          </div>
                        </div>
                      </a>
                    </div>
                  </div>
                  <div class="gallery-col col">
                    <div class="col-inner">
                      <a class="image-lightbox lightbox-gallery">
                        <div class="box has-hover gallery-box box-none">
                          <div class="box-image">
                            <img
                              src="@/assets/img/momo.svg"
                              alt=""
                              class="momo"
                            />
                          </div>
                        </div>
                      </a>
                    </div>
                  </div>
                  <div class="gallery-col col">
                    <div class="col-inner">
                      <a class="image-lightbox lightbox-gallery">
                        <div class="box has-hover gallery-box box-none">
                          <div class="box-image">
                            <img src="@/assets/img/vnpay.svg" alt="" />
                          </div>
                        </div>
                      </a>
                    </div>
                  </div>
                  <div class="gallery-col col">
                    <div class="col-inner">
                      <a class="image-lightbox lightbox-gallery">
                        <div class="box has-hover gallery-box box-none">
                          <div class="box-image">
                            <img src="@/assets/img/visa.png" alt="" />
                          </div>
                        </div>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <div class="absolute-footer dark medium-text-center text-center">
      <div class="container clearfix">
        <div class="footer-primary pull-left">
          <div class="copyright-footer">
            {{ this.$BResource.COPYRIGHT.CAPTION }}
            <a href="https://www.facebook.com/hieudam2kx/">{{
              this.$BResource.COPYRIGHT.NAME
            }}</a>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "Footer",
  components: {},
};
</script>

<style scoped>
@import url(@/css/components/layout/BFooter.css);
</style>
