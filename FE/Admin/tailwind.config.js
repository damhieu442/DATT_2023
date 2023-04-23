/** @type {import('tailwindcss').Config} */
module.exports = {
	content: ["./index.html", "./src/**/*.{vue,js,ts,jsx,tsx}"],
	darkMode: "class",
	theme: {
		extend: {},
	},
	plugins: [],
	corePlugins: {
		preflight: false, // Resolve CSS conflict between tailwind and Antd
	},
};
