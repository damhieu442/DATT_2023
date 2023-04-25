import { cart as cartAPI } from "@/api";

export const EMutationTypes = {
	UPDATE_PRODUCT_AMOUNT: "UPDATE_PRODUCT_AMOUNT",
	REMOVE_PRODUCT: "REMOVE_PRODUCT",
};

/**
 * @typedef {Object} TProduct
 * @property {string} id
 * @property {string} image
 * @property {string} name
 * @property {number} price
 * @property {number} quantity
 *
 * @typedef {Object} TCart
 * @property {TProduct[]} productList - Product list
 * @property {boolean} isLoadingData - Is getting data
 */

const cart = {
	namespaced: true,
	/**
	 *
	 * @returns {TCart}
	 */
	state: () => ({
		productList: [],
		isLoadingData: false,
	}),
	getters: {
		totalProduct(state) {
			return state.productList.reduce((count, product) => count + product.quantity, 0);
		},

		totalPrice(state) {
			return state.productList.reduce(
				(count, product) => count + product.quantity * product.price,
				0,
			);
		},
	},
	mutations: {
		/**
		 *
		 * @param {TCart} state
		 * @param {Object} payload
		 * @param {string} payload.productId
		 * @param {string} payload.size
		 * @param {number} payload.amount
		 */
		[EMutationTypes.UPDATE_PRODUCT_AMOUNT](state, payload) {
			const product = state.productList.find(
				(product) => product.id === payload.productId && product.size === payload.size,
			);

			if (product) {
				product.quantity = payload.amount;
			}
		},
		/**
		 *
		 * @param {TCart} state
		 * @param {string} productID
		 */
		[EMutationTypes.REMOVE_PRODUCT](state, productID) {
			const productIndex = state.productList.findIndex((product) => product.id === productID);

			if (productIndex > -1) {
				state.productList.splice(productIndex, 1);
			}
		},
	},
	actions: {
		async addProductToCart({ state, rootState }, product) {
			const { id, image, name, price, size, amount } = product;
			const uid = rootState.user.uid;

			const sendForm = {
				ShoeId: id,
				ShoeName: name,
				Price: price,
				Amount: amount,
				Size: size,
				CreatedBy: rootState.user.username,
				ModifiedBy: rootState.user.username,
			};
			try {
				const response = await cartAPI.addProductToCart(sendForm, uid); //new Promise((resolve) => setTimeout(resolve, 1500));

				if (response.status < 300 && response.status > 199) {
					const prod = state.productList.find(
						(product) => product.id === id && product.size === size,
					);

					if (prod) {
						prod.quantity += amount;
					} else {
						state.productList.push({ id, image, name, price, size, quantity: amount });
					}
				} else {
					return false;
				}

				return true;
			} catch (error) {
				return false;
			}
		},

		async removeProductFromCart({ state }, id) {
			try {
				await new Promise((resolve) => setTimeout(resolve, 1500));
				state.productList.splice(
					state.productList.findIndex((product) => product.id === id),
					1,
				);

				return true;
			} catch (error) {
				return false;
			}
		},

		async getUserCart({ state, rootState }) {
			state.isLoadingData = true;
			const uid = rootState.user.uid;
			try {
				const response = await cartAPI.getUserCart(uid);
				console.log("Response: ", response);
			} catch (error) {
			} finally {
				state.isLoadingData = false;
			}
		},
	},
};

export default cart;
