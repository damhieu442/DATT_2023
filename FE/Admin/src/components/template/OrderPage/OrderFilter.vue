<template>
	<a-form layout="vertical" :form="form" @submit.prevent="formSubmit">
		<a-row :gutter="24">
			<a-col :span="9">
				<a-form-item label="Mã đơn hàng" name="id">
					<a-input v-model:value="form.id" placeholder="Nhập mã đơn hàng" />
				</a-form-item>
			</a-col>
			<a-col :span="15">
				<a-form-item label="Người nhận" name="recipient">
					<a-input v-model:value="form.recipient" placeholder="Nhập tên người nhận" />
				</a-form-item>
			</a-col>
		</a-row>
		<a-row type="flex" :gutter="24" justify="space-between" align="middle">
			<a-col :span="8">
				<a-form-item label="Số điện thoại" name="phone_number">
					<a-input
						v-model:value="form.phone_number"
						placeholder="Nhập số điện thoại người nhận"
					/>
				</a-form-item>
			</a-col>
			<a-col :span="8">
				<a-form-item label="Trạng thái" name="status">
					<a-select v-model:value="form.status" placeholder="Chọn trạng thái đơn hàng">
						<a-select-option
							v-for="(stateLabel, stateKey) in ORDER_STATE"
							:key="stateKey"
							:value="stateKey"
						>
							{{ stateLabel }}
						</a-select-option>
					</a-select>
				</a-form-item>
			</a-col>
			<a-col :span="8">
				<a-button type="primary" class="w-full submit-btn" html-type="submit">
					Tìm kiếm
				</a-button>
			</a-col>
		</a-row>
	</a-form>
</template>

<script>
	import _assign from "lodash/assign";
	import _forOwn from "lodash/forOwn";
	import ORDER_STATE from "@/constants/order";

	const defaultFilterForm = {
		id: "",
		phone_number: "",
		recipient: "",
		status: undefined,
	};
	export default {
		name: "OrderFilter",
		data() {
			return {
				form: _assign({}, defaultFilterForm),
				ORDER_STATE,
			};
		},

		methods: {
			formSubmit() {
				const query = _assign({}, this.$route.query);

				_forOwn(this.form, (filterVal, filterKey) => {
					if (filterVal) {
						query[filterKey] = filterVal;
					} else {
						delete query[filterKey];
					}
				});

				this.$router.push({
					query,
				});
			},
		},
	};
</script>

<style lang="scss" scoped>
	.submit-btn {
		@apply bg-blue-500  active:bg-blue-600 hover:bg-blue-400 border-none #{!important};
	}
</style>
