<template>
	<div>
		<a-row :gutter="24" type="flex" align="middle">
			<a-col :span="3">
				<span>Tìm kiếm</span>
			</a-col>
			<a-col :span="18">
				<a-input v-model:value="search" :placeholder="placeholder" />
			</a-col>
			<a-col :span="3">
				<a-button type="primary" class="w-full" @click="searchKeyword"> Tìm kiếm </a-button>
			</a-col>
		</a-row>
	</div>
</template>

<script>
	import _assign from "lodash/assign";

	export default {
		name: "SearchInput",
		props: {
			placeholder: {
				type: String,
				default: "Nhập tên muốn tìm kiếm",
			},
			query: {
				type: String,
				default: "search",
			},
		},
		data() {
			return {
				search: this.$route.query[this.query] || "",
			};
		},
		methods: {
			searchKeyword() {
				const queries = _assign({}, this.$route.query, { [this.query]: this.search });

				this.$router.push({
					query: queries,
				});
			},
		},
	};
</script>
