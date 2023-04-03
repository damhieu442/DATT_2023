const cart = {
	namespaced: true,
	state: () => ({
		productList: [],
	}),
	getters: {},
	mutations: {},
	actions: {
		async addProductToCart({ state }, product) {
			await new Promise((resolve) => setTimeout(resolve, 1500));
			state.productList.push(product);
		},
	},
};

export default cart;
