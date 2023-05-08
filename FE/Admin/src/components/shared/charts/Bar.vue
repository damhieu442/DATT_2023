<template>
	<div>
		<Bar :options="options" :data="chartdata" v-bind="$attrs" />
	</div>
</template>

<script>
	import { Bar } from "vue-chartjs";
	import {
		CategoryScale,
		Chart as ChartJS,
		Legend,
		BarElement,
		LinearScale,
		Title,
		Tooltip,
	} from "chart.js";

	ChartJS.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale);

	export default {
		name: "DonutChart",
		components: { Bar },
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
