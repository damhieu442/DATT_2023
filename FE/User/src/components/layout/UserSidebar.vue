<template>
	<aside class="sidebar">
		<div class="sidebar__user-info">
			<img
				v-if="!!avatar"
				:src="avatar"
				alt=""
				class="sidebar__user-info__avatar"
				width="48"
				height="48"
			/>
			<i v-else class="sidebar__user-info__avatar fa-solid fa-user"></i>
			<div class="sidebar__user-info__name">
				<p>Tài khoản của</p>
				<p>{{ username }}</p>
			</div>
		</div>
		<ul class="sidebar__nav">
			<li>
				<router-link to="/tai-khoan" exactActiveClass="active">
					<i class="icon fa-solid fa-user"></i>
					<span>Thông tin tài khoản</span>
				</router-link>
			</li>

			<li>
				<router-link activeClass="active" to="/tai-khoan/don-hang">
					<i class="icon fa-solid fa-bag-shopping"></i>
					<span>Quản lý đơn hàng</span>
				</router-link>
			</li>
			<li>
				<router-link activeClass="active" to="/tai-khoan/doi-mat-khau">
					<i class="icon fa-solid fa-lock"></i>
					<span>Đổi mật khẩu</span>
				</router-link>
			</li>
		</ul>
	</aside>
</template>

<script setup>
	import { computed } from "vue";
	import { useStore } from "vuex";

	const store = useStore();

	const username = computed(() => store.state.user.username);
	const avatar = computed(() => store.state.user.image);
</script>

<style lang="scss" scoped>
	.sidebar {
		min-width: 250px;

		&__user-info {
			display: flex;
			margin-bottom: 1.25rem;

			&__name {
				> p {
					font-size: 0.75rem;
					margin: 0;
					line-height: 1.5;

					&:last-child {
						font-size: 1rem;
						font-weight: 700;
					}
				}
			}

			&__avatar {
				height: 3rem;
				width: 3rem;
				object-fit: cover;
				border-radius: 1.5rem;
				margin-right: 1rem;
				font-size: 3rem;
				text-align: center;
			}
		}

		&__nav {
			> li {
				> a {
					display: flex;
					align-items: center;
					color: currentColor;
					cursor: pointer;
					padding: 0.5rem 1.25rem;
					transition: 0.5s;

					&:hover {
						background-color: rgb(94 234 212);
					}

					&.active {
						background-color: rgb(94 234 212);
					}

					> .icon {
						width: 1rem;
						height: 1rem;
						margin-right: 1rem;
						line-height: 1;
					}
				}
			}
		}
	}
</style>
