<template>
	<div class="loader"></div>
</template>

<style lang="scss" scoped>
	.loader {
		width: 40px;
		height: calc(80px * 0.866);
		clip-path: polygon(0 0, 100% 100%, 0 100%, 100% 0);
		position: relative;
		animation: loading-animation 2s infinite linear;
		overflow: hidden;
	}
	.loader:before {
		content: "";
		position: absolute;
		inset: -150% -150%;
		background: repeating-conic-gradient(
			from 30deg,
			#ffabab 0 60deg,
			#abe4ff 0 120deg,
			#ff7373 0 180deg
		);
		animation: inherit;
		animation-direction: reverse;
	}
	@keyframes loading-animation {
		100% {
			transform: rotate(360deg);
		}
	}
</style>
