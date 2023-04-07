<template>
	<div id="header" class="header has-sticky sticky-jump">
		<div class="header-wrapper">
			<div class="header-main show-logo-center nav-dark">
				<div class="header-inner flex-row container logo-center medium-logo-center">
					<div id="logo" class="flex-col logo">
						<router-link to="/" title="Converse">
							<img
								width="200"
								height="80"
								src="http://mauweb.monamedia.net/converse/wp-content/uploads/2019/05/logo-mona.png"
								class="header_logo header-logo"
								alt="Converse"
							/>
						</router-link>
					</div>
					<div class="flex-col flex-left">
						<ul class="header-nav header-nav-main nav nav-left nav-uppercase">
							<li class="account-item has-icon">
								<Authentication />
							</li>
						</ul>
					</div>
					<div class="flex-col flex-right">
						<div style="display: flex; justify-content: center; align-items: center">
							<ul class="header-nav header-nav-main nav nav-right nav-uppercase">
								<li
									class="cart-item has-icon has-dropdown"
									:class="false ? 'current-dropdown' : ''"
								>
									<a href="#" title="Giỏ hàng" class="header-cart-link is-small">
										<span class="header-cart-title">
											Giỏ hàng /&nbsp;
											<span class="cart-price">
												<span class="woocommerce-Price-amount amount">
													0&nbsp;
													<span class="woocommerce-Price-currencySymbol"
														>₫</span
													>
												</span></span
											>
										</span>
										<span class="cart-icon image-icon">
											<strong>0</strong>
										</span>
									</a>
								</li>
								<div class="header-divider"></div>
							</ul>
						</div>
					</div>
				</div>
			</div>
			<div class="header-bottom wide-nav flex-has-center">
				<div class="flex-row container">
					<div class="flex-col flex-center">
						<ul
							class="nav header-nav header-bottom-nav nav-center nav-size-medium nav-spacing-xlarge nav-uppercase"
						>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<router-link
									to="/"
									class="nav-top-link"
									exact-active-class="current-menu-item"
								>
									Trang chủ
								</router-link>
							</li>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<router-link
									class="nav-top-link"
									:to="{ name: 'Introduce' }"
									active-class="current-menu-item"
								>
									Giới thiệu
								</router-link>
							</li>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<CategoryItem categoryCode="nu" />
							</li>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<CategoryItem categoryCode="nam" />
							</li>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<CategoryItem categoryCode="tre-em" />
							</li>
							<li
								class="menu-item menu-item-type-post_type menu-item-object-page menu-item-home page_item page-item-16 current_page_item active menu-item-24"
							>
								<router-link
									class="nav-top-link"
									:to="{ name: 'Contact' }"
									active-class="current-menu-item"
								>
									Liên hệ
								</router-link>
							</li>
						</ul>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script setup>
	import CategoryItem from "./Header/CategoryItem.vue";
	import Authentication from "./Header/Authentication.vue";
</script>

<style scoped>
	@import url(@/css/components/layout/BHeader.css);
</style>
