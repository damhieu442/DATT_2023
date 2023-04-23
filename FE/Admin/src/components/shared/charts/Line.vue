<template>
	<div>
		<Line :options="options" :data="chartdata" v-bind="$attrs" />
	</div>
</template>

<script>
	import { Line } from "vue-chartjs";
	import {
		CategoryScale,
		Chart as ChartJS,
		Legend,
		LineElement,
		LinearScale,
		PointElement,
		Title,
		Tooltip,
	} from "chart.js";
	ChartJS.register(Title, Tooltip, Legend, LineElement, CategoryScale, LinearScale, PointElement);
	export default {
		name: "DonutChart",
		components: { Line },
		props: {
			chartdata: {
				type: Object,
				default: null,
			},
			options: {
				type: Object,
				default: null,
			},
		},
	};
</script>
