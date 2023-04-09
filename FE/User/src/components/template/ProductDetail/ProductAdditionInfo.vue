<template>
	<table class="additional-info">
		<tbody>
			<tr>
				<th>SKU</th>
				<td>
					<p>{{ sku }}</p>
				</td>
			</tr>
			<tr>
				<th>Chất liệu</th>
				<td>
					<p>{{ material }}</p>
				</td>
			</tr>
			<tr>
				<th>Giới tính</th>
				<td>
					<p>{{ target }}</p>
				</td>
			</tr>
		</tbody>
	</table>
</template>

<script setup>
	const props = defineProps({
		sku: String,
		material: String,
		target: String,
	});
</script>

<style lang="scss" scoped>
	.additional-info {
		width: 100%;
		/* min-height: 150px; */

		th,
		td {
			padding: 0.5rem;
			text-align: left;
			border-bottom: 1px solid #ececec;
			line-height: 1.3;
			font-size: 0.9rem;
		}

		th {
			line-height: 1.05;
			letter-spacing: 0.05rem;
			text-transform: uppercase;
			padding-left: 0;
		}

		td {
			color: #666;

			> p {
				margin: 0.5rem 0;
			}
		}
	}
</style>
