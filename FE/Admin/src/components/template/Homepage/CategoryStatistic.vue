<template>
	<div>
		<div class="flex relative pt-5">
			<PieChart
				:chartdata="chartdata"
				:options="options"
				style="width: 100%; height: 650px"
			/>
		</div>
	</div>
</template>

<script>
	import PieChart from "@/components/shared/charts/Pie.vue";
	import { monthLabels } from "@/constants/months";
	import _cloneDeep from "lodash/cloneDeep";

	export default {
		name: "RevenueStatistic",
		components: { PieChart },
		props: {
			growthStatistic: {
				type: Object,
				required: true,
			},
		},
		data() {
			const lineColor = "#22d3ee";

			const pieArcColors = [
				"#ff990088",
				"#cbff0088",
				"#33ff0088",
				"#00ff6688",
				"#00ffff88",
				"#0066ff88",
				"#3200ff88",
				"#cc00ff88",
				"#ff009888",
				"#ff000088",
			];

			const labels = [];
			const datasets = [
				{
					data: [],
					backgroundColor: pieArcColors,
					borderColor: lineColor,
					lineTension: 0,
				},
			];

			return {
				chartdata: {
					labels,
					datasets,
				},
				options: {
					responsive: true,
					maintainAspectRatio: false,
					plugins: {
						legend: {
							display: true,
							position: "right",
						},
						tooltip: {
							displayColors: true,
							callbacks: {
								title: (tooltipItem) => {
									const { label: category } = tooltipItem[0];
									return `Danh mục ${category}`;
								},
								label: (tooltipItem) => {
									return `Đã bán ${tooltipItem.formattedValue} c`;
								},
							},
						},
					},
					layout: {
						padding: {
							left: 30,
							right: 0,
							top: 20,
							bottom: 0,
						},
					},
				},
			};
		},

		watch: {
			growthStatistic(value) {
				const chartdata = _cloneDeep(this.chartdata);

				for (const month in value) {
					const statistic = value[month];
					chartdata.datasets[0].data.push(statistic);
					chartdata.labels.push(monthLabels[month] || month);
				}

				this.chartdata = chartdata;
			},
		},
	};
</script>
