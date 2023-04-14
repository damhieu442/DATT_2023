<template>
	<div class="billing-form">
		<h3>Thông tin thanh toán</h3>
		<a-form name="billing-form" layout="vertical" class="billing-form__form">
			<a-row :gutter="32">
				<a-col :span="12">
					<a-form-item label="Họ tên" v-bind="form.validateInfos.fullName">
						<a-input v-model:value="formState.fullName" />
					</a-form-item>
				</a-col>
				<a-col :span="12">
					<a-form-item label="Tuổi" v-bind="form.validateInfos.age">
						<a-input v-model:value="formState.age" />
					</a-form-item>
				</a-col>
			</a-row>
			<a-form-item label="Quốc gia" v-bind="form.validateInfos.country"
				><a-select
					v-model:value="formState.country"
					show-search
					placeholder="Chọn quốc gia"
					:options="countries"
					:filterOption="countryFilterHandler"
				>
				</a-select>
			</a-form-item>
			<a-form-item label="Địa chỉ" v-bind="form.validateInfos.address">
				<a-input v-model:value="formState.address" />
			</a-form-item>
			<a-form-item label="Tỉnh / Thành phố" v-bind="form.validateInfos.city">
				<a-input v-model:value="formState.city" />
			</a-form-item>
			<a-form-item label="Số điện thoại" v-bind="form.validateInfos.phone">
				<a-input v-model:value="formState.phone" />
			</a-form-item>
			<a-form-item label="Địa chỉ email" v-bind="form.validateInfos.email">
				<a-input v-model:value="formState.email" />
			</a-form-item>
			<a-form-item label="Ghi chú đơn hàng (tùy chọn)" v-bind="form.validateInfos.note">
				<a-textarea
					v-model:value="formState.note"
					:auto-size="{ minRows: 5, maxRows: 7 }"
					placeholder="Ghi chú về đơn hàng, ví dụ: thời gian hay chỉ dẫn địa điểm giao hàng chi tiết hơn."
				/>
			</a-form-item>
		</a-form>
	</div>
</template>

<script setup>
	import { reactive, toRaw } from "vue";
	import { countriesList } from "@/constants/countries";
	import { Form } from "ant-design-vue";

	const formState = reactive({
		fullName: "",
		age: "",
		country: "",
		address: "",
		city: "",
		phone: "",
		email: "",
		note: "",
	});

	const formRules = reactive({
		fullName: [{ required: true, message: "Vui lòng nhập họ tên người nhận!" }],
		age: [{ required: true, message: "Vui lòng nhập tuổi người nhận!" }],
		country: [{ required: true, message: "Vui lòng nhập quốc gia nhận!" }],
		address: [{ required: true, message: "Vui lòng nhập địa chỉ người nhận!" }],
		city: [{ required: true, message: "Vui lòng nhập thành phố nhận!" }],
		phone: [{ required: true, message: "Vui lòng nhập số điện thoại người nhận!" }],
		email: [
			{ required: true, message: "Vui lòng nhập địa chỉ email người nhận!" },
			{ type: "email", message: "Vui lòng nhập địa chỉ email hợp lệ" },
		],
		note: [],
	});

	const form = Form.useForm(formState, formRules);

	const formSubmitHandler = async () => {
		try {
			await form.validate();

			return { ...toRaw(formState) };
		} catch (error) {
			return undefined;
		}
	};

	const countries = countriesList;

	const countryFilterHandler = (input, option) => {
		return option.label.toLocaleLowerCase().indexOf(input.toLocaleLowerCase()) >= 0;
	};

	defineExpose({
		submit: formSubmitHandler,
	});
</script>

<style lang="scss">
	.billing-form {
		> h3 {
			font-size: 1.1em;
			overflow: hidden;
			padding-top: 10px;
			font-weight: bolder;
			text-transform: uppercase;
			color: #1c1c1c;
		}

		&__form {
			.ant-form-item-label {
				> label {
					font-weight: 700;
					font-size: 0.9rem;
					color: #222;

					&.ant-form-item-required {
						&::before {
							display: none !important;
						}

						&::after {
							display: inline-block !important;
							margin-left: 4px;
							color: #222;
							font-size: 14px;
							font-family: SimSun, sans-serif;
							line-height: 1;
							content: "*";
						}
					}
				}
			}
		}
	}
</style>
