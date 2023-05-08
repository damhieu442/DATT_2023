<template>
	<div>
		<Pie :options="options" :data="chartdata" v-bind="$attrs" />
	</div>
</template>

<script>
	import { Pie } from "vue-chartjs";
	import {
		CategoryScale,
		Chart as ChartJS,
		Legend,
		LinearScale,
		Title,
		Tooltip,
		ArcElement,
	} from "chart.js";

	ChartJS.register(Title, Tooltip, Legend, CategoryScale, LinearScale, ArcElement);

	export default {
		name: "DonutChart",
		components: { Pie },
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
