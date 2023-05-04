<template>
	<div>
		<div class="flex relative pt-5">
			<p class="text-sm absolute top-0 left-0">Số đơn</p>
			<LineChart
				:chartdata="chartdata"
				:options="options"
				style="width: 100%; height: 650px"
			/>
		</div>
	</div>
</template>

<script>
	import LineChart from "@/components/shared/charts/Line.vue";
	import _cloneDeep from "lodash/cloneDeep";

	const monthLabels = {
		January: 1,
		February: 2,
		March: 3,
		April: 4,
		May: 5,
		June: 6,
		July: 7,
		August: 8,
		September: 9,
		October: 10,
		November: 11,
		December: 12,
	};

	export default {
		name: "CompleteOrderStatisticLineChart",
		components: { LineChart },
		props: {
			growthStatistic: {
				type: Object,
				required: true,
			},
		},
		data() {
			const lineColor = "#22d3ee";
			const bgColor = "#f00";

			const labels = [];
			const datasets = [
				{
					data: [],
					backgroundColor: bgColor,
					borderColor: lineColor,
					pointBackgroundColor: "#fff",
					pointBorderColor: lineColor,
					pointRadius: 5,
					pointHoverRadius: 7,
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

					scales: {
						y: {
							beginAtZero: true,
							padding: 10,
							ticks: {},
						},
					},
					plugins: {
						legend: {
							display: false,
						},
						tooltip: {
							displayColors: false,
							callbacks: {
								title: (tooltipItem) => {
									const { label: month } = tooltipItem[0];
									return `Tháng ${month}`;
								},
								label: (tooltipItem) => {
									return `${tooltipItem.parsed.y} đơn`;
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
