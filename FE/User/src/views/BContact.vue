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
								<h3>{{ this.$BResource.ADDRESS.CAPTION2 }}</h3>
								<div
									class="icon-box featured-box icon-box-left text-left"
									style="margin: 0px 0px 10px 0px"
								>
									<div class="icon-box-img" style="width: 17.5px">
										<div class="icon">
											<div class="icon-inner">
												<i class="fa-solid fa-location-dot fa-xl"></i>
											</div>
										</div>
									</div>
									<div class="icon-box-text last-reset">
										<p>
											{{ this.$BResource.ADDRESS.TITLE }}
										</p>
									</div>
								</div>
								<div
									class="icon-box featured-box icon-box-left text-left"
									style="margin: 0px 0px 10px 0px"
								>
									<div class="icon-box-img">
										<div class="icon">
											<div class="icon-inner">
												<i class="fa-solid fa-phone fa-lg"></i>
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
								<div
									class="icon-box featured-box icon-box-left text-left"
									style="margin: 0px 0px 10px 0px"
								>
									<div class="icon-box-img">
										<div class="icon">
											<div class="icon-inner">
												<i class="fa-solid fa-envelope fa-lg"></i>
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
								<div
									class="icon-box featured-box icon-box-left text-left"
									style="margin: 0px 0px 10px 0px"
								>
									<div class="icon-box-img">
										<div class="icon">
											<div class="icon-inner">
												<i class="fa-brands fa-skype fa-lg"></i>
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
						<div class="col medium-6 small-12 large-6">
							<div class="col-inner">
								<div class="wpcf7">
									<div class="wpcf7-form">
										<div
											class="form-ct clear"
											:class="{ processing: isSendingForm }"
										>
											<div class="form-row w50">
												<span class="wpcf7-form-control-wrap text-name">
													<DxTextBox
														v-model="form.fullName"
														label="Họ và tên"
														labelMode="floating"
													/>
												</span>
											</div>
											<div class="form-row w50">
												<span class="wpcf7-form-control-wrap text-name">
													<DxTextBox
														v-model="form.email"
														label="Email"
														labelMode="floating"
													/>
												</span>
											</div>
											<div class="form-row w50">
												<span class="wpcf7-form-control-wrap text-name">
													<DxTextBox
														v-model="form.phone"
														label="Số điện thoại"
														labelMode="floating"
													/>
												</span>
											</div>
											<div class="form-row w50">
												<span class="wpcf7-form-control-wrap text-name">
													<DxTextBox
														v-model="form.address"
														label="Địa chỉ"
														labelMode="floating"
													/>
												</span>
											</div>
											<div class="form-row">
												<span class="wpcf7-form-control-wrap text-name">
													<DxTextArea
														v-model="form.content"
														:height="90"
														value=""
														label="Nội dung"
														labelMode="floating"
													/>
												</span>
											</div>
											<div class="form-row center-txt">
												<DxButton
													text="GỬI"
													:width="280"
													:height="38"
													type="danger"
													styling-mode="contained"
													@click="submitFormHandler"
												/>
											</div>
										</div>
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
	import DxTextBox from "devextreme-vue/text-box";
	import DxTextArea from "devextreme-vue/text-area";
	import DxButton from "devextreme-vue/button";
	import DxMap from "devextreme-vue/map";
	import { notification } from "ant-design-vue";
	import { markersData } from "@/js/constant";
	import { feedback } from "@/api";

	const markerUrl =
		"https://js.devexpress.com/Demos/WidgetsGallery/JSDemos/images/maps/map-marker.png";

	const defaultForm = { fullName: "", email: "", phone: "", address: "", content: "" };

	export default {
		name: "Contact",
		components: { DxMap, DxTextBox, DxTextArea, DxButton },
		data() {
			return {
				markersData,
				markersIcon: markerUrl,
				form: { ...defaultForm },
				isSendingForm: false,
			};
		},

		methods: {
			async submitFormHandler() {
				console.log("Submit: ", this.form);
				this.isSendingForm = true;

				const form = new FormData();

				form.append("FullName", this.form.fullName);
				form.append("Email", this.form.email);
				form.append("PhoneNumber", this.form.phone);
				form.append("Address", this.form.address);
				form.append("Description", this.form.content);

				try {
					const response = await feedback.sendFeedBack(form);
					console.log("Response: ", response);
					if (response.status === 201) {
						notification.success({
							message:
								"Phản hồi của bạn đã được tiếp nhận, chúng tôi sẽ đọc và phản hồi lại bạn sớm nhất có thể",
						});
						this.form = { ...defaultForm };
					} else {
						throw new Error();
					}
				} catch (error) {
					notification.error({ message: "Có lỗi xảy ra, thử lại sau" });
				}

				this.isSendingForm = false;
			},
		},
	};
</script>
<style scoped>
	@import url(@/css/views/BContact.css);
</style>
