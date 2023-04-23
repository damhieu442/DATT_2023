<template>
	<a-table
		:data-source="data"
		:pagination="false"
		class="customer-agency-table"
		:loading="loading"
		row-key="id"
	>
		<a-table-column
			key="column-index"
			align="center"
			title="Mã khách hàng"
			data-index="id"
			width="300px"
		>
			<template #default="{ value: id }">
				<router-link
					:to="'/khach-hang/' + id"
					class="text-current underline hover:text-blue-500"
				>
					{{ id }}
				</router-link>
			</template>
		</a-table-column>
		<a-table-column
			key="column-customer-username"
			align="center"
			title="Tài khoản"
			data-index="username"
			min-width="150px"
		>
			<template #default="{ value: name }">
				<span>{{ name }}</span>
			</template>
		</a-table-column>
		<a-table-column
			key="column-customer-name"
			align="center"
			title="Họ tên"
			data-index="fullName"
			min-width="250px"
		>
			<template #default="{ value: name }">
				<span>{{ name }}</span>
			</template>
		</a-table-column>
		<a-table-column
			key="column-customer-email"
			align="center"
			title="Email"
			data-index="email"
			width="150px"
		>
			<template #default="{ value: email }">
				<span>{{ email }}</span>
			</template>
		</a-table-column>
		<a-table-column
			key="column-customer-phone"
			align="center"
			title="Số điện thoại"
			data-index="phone"
			width="150px"
		>
			<template #default="{ value: phone }">
				<span>{{ phone }}</span>
			</template>
		</a-table-column>
		<a-table-column
			key="column-customer-role"
			align="center"
			title="Quyền"
			data-index="role"
			width="150px"
		>
			<template #default="{ value: role }">
				<span>{{ role }}</span>
			</template>
		</a-table-column>
		<a-table-column
			key="column-time"
			align="center"
			title="Thời gian đăng ký"
			data-index="time"
			width="200px"
		>
			<template #default="{ value: time }">
				<span>{{ time }}</span>
			</template>
		</a-table-column>
		<a-table-column key="column-actions" width="100px">
			<template #default="customer">
				<div class="flex justify-between items-center text-base">
					<div class="hover:cursor-pointer" @click="navigateToDetail(customer)">
						<i class="fa-solid fa-eye" />
					</div>
					<div class="hover:cursor-pointer" @click="navigateToEdit(customer)">
						<i class="fas fa-pen" />
					</div>
					<div class="hover:cursor-pointer" @click="deleteUser(customer)">
						<i class="fa-solid fa-trash"></i>
					</div>
				</div>
			</template>
		</a-table-column>
	</a-table>
</template>

<script>
	import { Modal } from "ant-design-vue";

	export default {
		name: "UserListTable",
		emits: ["delete"],
		props: {
			data: {
				type: Array,
				required: true,
			},
			loading: {
				type: Boolean,
				required: true,
			},
		},

		methods: {
			navigateToDetail({ value: { id } }) {
				this.$router.push(`/khach-hang/${id}`);
			},

			navigateToEdit({ value: { id } }) {
				this.$router.push(`/khach-hang/${id}/chinh-sua`);
			},

			confirmDeleteUser(uid) {
				this.$emit("delete", uid);
			},

			deleteUser({ value: user }) {
				Modal.confirm({
					content: `Bạn có chắc muốn xóa người dùng ${user.username}?`,
					icon: null,
					maskClosable: true,
					onOk: () => {
						this.confirmDeleteUser(user.id);
						return Promise.resolve();
					},
					okText: "Xóa",
					okButtonProps: { danger: true },
					cancelText: "Hủy",
				});
			},
		},
	};
</script>

<style lang="scss">
	.customer-agency-table {
		.ant-table-thead {
			@apply bg-cyan-300;
			th {
				@apply bg-transparent #{!important};
			}
		}
	}
</style>
