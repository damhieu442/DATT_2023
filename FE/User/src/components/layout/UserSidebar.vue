<template>
	<aside class="sidebar">
		<div class="sidebar__user-info">
			<!-- <img src="{avatar}" alt="" class="h-12 w-12 rounded-3xl object-cover" /> -->
			<div class="sidebar__user-info__name">
				<p>Tài khoản của</p>
				<p>{{ username }}</p>
			</div>
		</div>
		<ul class="sidebar__nav">
			<li>
				<router-link to="/tai-khoan" activeClass="active">
					<!-- <FontAwesomeIcon
                    class="w-4 h-4 mr-4 leading-none"
                    icon={['fas', 'user']}
                /> --><i class="icon fa-solid fa-user"></i>
					<span>Thông tin tài khoản</span>
				</router-link>
			</li>

			<li>
				<router-link activeClass="active" to="/tai-khoan/don-hang">
					<!-- <FontAwesomeIcon
                        class="w-4 h-4 mr-4 leading-none"
                        icon={['fas', 'bag-shopping']}
                    /> --><i class="icon fa-solid fa-bag-shopping"></i>
					<span>Quản lý đơn hàng</span>
				</router-link>
			</li>
			<li>
				<router-link activeClass="active" to="/tai-khoan/doi-mat-khau">
					<!-- c 
                <FontAwesomeIcon
                    class="w-4 h-4 mr-4 leading-none"
                    icon={['fas', 'lock']}
                /> --><i class="icon fa-solid fa-lock"></i>
					<span>Đổi mật khẩu</span>
				</router-link>
			</li>
		</ul>
	</aside>
</template>

<script setup>
	import { useStore } from "vuex";

	const store = useStore();

	const username = store.state.user.username; //useSelector((store) => store.user.username);
	// const avatar = useSelector((store) => store.user.avatar);
</script>

<style lang="scss" scoped>
	.sidebar {
		min-width: 250px;

		&__user-info {
			display: flex;
			margin-bottom: 1.25rem;

			&__name {
				margin-left: 1rem;

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
		}

		&__nav {
			/* flex items-center text-current cursor-pointer px-5 py-2 hover:bg-gray-300 hover:text-current */

			> li {
				> a {
					display: flex;
					align-items: center;
					color: currentColor;
					cursor: pointer;
					padding: 0.5rem 1.25rem;

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
