<template>
	<div>
		<h1 class="text-3xl">Danh sách khách hàng</h1>
		<a-row class="my-6">
			<a-col :span="14">
				<Search placeholder="Nhập họ tên khách hàng muốn tìm kiếm" />
			</a-col>
			<a-col :span="4">
				<router-link
					to="/khach-hang/them-moi"
					class="inline-block m-auto bg-[#40a9ff] text-white h-full px-6 ml-4 align-middle leading-8 hover:text-white hover:bg-sky-500"
					>Thêm mới</router-link
				>
			</a-col>
		</a-row>

		<UserList :loading="isLoading" :data="user" @delete="deleteUser" />

		<ThePagination :data="pagination" />
	</div>
</template>

<script setup>
	import Search from "@/components/shared/Search.vue";
	import { reactive, ref, watch } from "vue";
	import { useRoute } from "vue-router";

	import UserList from "@/components/template/CustomerPage/UserList.vue";
	import ThePagination from "@/components/shared/ThePagination.vue";
	import { customer } from "@/api";
	import { RoleLabels } from "@/constants/config";
	import dayjs from "dayjs";
	import { notification } from "ant-design-vue";

	const MAX_RECORD = 12;
	const DEFAULT_PAGE = 1;

	const route = useRoute();

	const pagination = reactive({
		page: route.query.pageNum || DEFAULT_PAGE,
		total: 0,
		perPage: route.query.pageSize || MAX_RECORD,
	});

	const user = ref([]);
	const isLoading = ref(false);

	const getUserList = async () => {
		isLoading.value = true;
		try {
			const { pageNum: page, pageSize: limit, search } = route.query;
			const query = {
				pageNumber: page || DEFAULT_PAGE,
				pageSize: limit || MAX_RECORD,
				keyWord: search || "",
			};

			const response = await customer.getList(query, []);

			if (response.status > 199 && response.status < 300) {
				if ("Data" in response.data) {
					const users = response.data.Data;
					Object.assign(pagination, {
						page: response.data.CurrentPage,
						perPage: response.data.CurrentPageRecords,
						total: response.data.TotalRecord,
					});

					user.value = users.map((user) => ({
						id: user.CustomerId,
						username: user.UserName,
						fullName: user.FullName,
						time: dayjs(user.CreatedDate).format("DD/MM/YYYY HH:mm"),
						phone: user.PhoneNumber,
						email: user.Email,
						role: RoleLabels[user.Role] || "-",
					}));
				} else {
					user.value = [];
					notification.error({
						message: "Không tìm được người dùng ứng với từ khóa đang tìm",
					});
				}
			} else {
				user.value = [];
				notification.error({
					message: "Có lỗi xảy ra, vui lòng thử lại",
				});
			}
		} catch (error) {
			user.value = [];
			notification.error({
				message: "Có lỗi xảy ra, vui lòng thử lại",
			});
		} finally {
			isLoading.value = false;
		}
	};

	const deleteUser = async (id) => {
		try {
			const response = await customer.deleteUser(id);

			if (response.status > 199 && response.status < 300) {
				notification.success({ message: "Xoá người dùng thành công" });
				getUserList();
			}
		} catch (error) {
			console.log("Errror: ", error);
			notification.error({ message: "Có lỗi xảy ra, vui lòng thử lại" });
		}
	};

	watch(
		() => route.query,
		() => {
			getUserList();
		},
		{
			immediate: true,
		},
	);
</script>
