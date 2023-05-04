import { cart as cartAPI, product as productAPI } from "@/api";
import { EKeys } from "@/constants/config";
import _cloneDeep from "lodash/cloneDeep";

const IMAGE_BASE_URL = process.env.VUE_APP_API_URL;

export const EMutationTypes = {
	UPDATE_PRODUCT_AMOUNT: "UPDATE_PRODUCT_AMOUNT",
	REMOVE_PRODUCT: "REMOVE_PRODUCT",
};

/**
 * @typedef {Object} TProduct
 * @property {string} cartId
 * @property {string} id
 * @property {string} image
 * @property {string} name
 * @property {string} size
 * @property {number} price
 * @property {number} discount
 * @property {number} quantity
 *
 * @typedef {Object} TCart
 * @property {TProduct[]} productList - Product list
 * @property {boolean} isLoadingData - Is getting data
 * @property {TProduct=} buyNowProduct - Buy now product
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
		buyNowProduct: undefined,
	}),
	getters: {
		totalProduct(state) {
			return state.productList.reduce((count, product) => count + product.quantity, 0);
		},

		totalPrice(state) {
			return state.productList.reduce(
				(total, product) =>
					total +
					Math.trunc(product.quantity * product.price * (1 - product.discount / 100)),
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
		async addProductToCart({ state, rootState, dispatch }, payload) {
			const { product, sizes } = payload;
			const { id, image, name, price, size, amount, discount } = product;
			const uid = rootState.user.uid;

			if (!uid) {
				return;
			}

			const sendForm = {
				ShoeId: id,
				ShoeName: name,
				Price: price,
				Amount: amount,
				Size: size,
				CreatedBy: rootState.user.username,
				ModifiedBy: rootState.user.username,
			};

			const productSizes = _cloneDeep(sizes);
			const productSize = productSizes.find((size) => size.code === product.size);
			productSize.amount -= amount;
			productSize.sold += amount;

			const updatedSizes = productSizes.map((size) => ({
				SizeId: size.id,
				SizeName: size.code,
				Amount: Number(size.amount),
				SoldNumber: Number(size.sold),
			}));

			try {
				const response = await cartAPI.addProductToCart(sendForm, uid);

				if (response.status < 300 && response.status > 199) {
					const prod = state.productList.find(
						(product) => product.id === id && product.size === size,
					);

					if (prod) {
						prod.quantity += amount;
					} else {
						state.productList.push({
							id,
							image,
							name,
							discount,
							price,
							size,
							quantity: amount,
						});
					}

					dispatch("getUserCart");
				} else {
					return false;
				}

				return true;
			} catch (error) {
				console.log("Error :", error);
				return false;
			}
		},

		async createBuyNowSession({ state, rootState, dispatch }, payload) {
			const { product } = payload;
			const { id, image, name, price, size, amount, discount } = product;
			const uid = rootState.user.uid;

			if (!uid) {
				return;
			}

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
				const response = await cartAPI.addProductToCart(sendForm, uid);

				if (response.status < 300 && response.status > 199) {
					state.buyNowProduct = {
						id,
						image,
						name,
						discount,
						price,
						size,
						quantity: amount,
					};

					localStorage.setItem(EKeys.buyNow, JSON.stringify(state.buyNowProduct));

					dispatch("getUserCart");
				} else {
					return false;
				}

				return true;
			} catch (error) {
				console.log("Error :", error);
				return false;
			}
		},

		async removeProductFromCart({ state }, id) {
			try {
				const prodIndex = state.productList.findIndex((product) => product.id === id);

				if (prodIndex !== -1) {
					await cartAPI.deleteCart(state.productList[prodIndex].cartId);

					state.productList.splice(prodIndex, 1);

					return true;
				} else {
					throw new Error();
				}
			} catch (error) {
				return false;
			}
		},

		async removeBuyNowProduct({ state }) {
			try {
				if (state.buyNowProduct) {
					await cartAPI.deleteCart(state.buyNowProduct.cartId);

					state.buyNowProduct = undefined;
					localStorage.removeItem(EKeys.buyNow);

					return true;
				} else {
					throw new Error();
				}
			} catch (error) {
				return false;
			}
		},

		async synchronizeCart({ state }) {
			try {
				const responses = await Promise.all(
					state.productList.map((product) =>
						cartAPI.updateCart(product.cartId, {
							CartDetailId: product.cartId,
							Amount: product.quantity,
							TotalPrice: Math.trunc(
								product.price * product.quantity * (1 - product.discount / 100),
							),
						}),
					),
				);

				return responses;
			} catch (error) {
				console.log("Error: ", error);
			}
		},

		async getUserCart({ state, rootState }) {
			state.isLoadingData = true;
			const uid = rootState.user.uid;
			try {
				const response = await cartAPI.getUserCart(uid);

				if (response.status > 199 && response.status < 300) {
					state.productList = response.data.map((item) => ({
						cartId: item.CartDetailId,
						id: item.ShoeId,
						image: item.ImgName
							? IMAGE_BASE_URL.concat(
									"/api/Shoes/imgName/",
									item.ImgName.split(",")[0],
							  )
							: "",
						name: item.ShoeName,
						price: item.Price,
						quantity: item.Amount,
						size: item.Size,
						discount: item.Discount,
					}));

					if (state.buyNowProduct) {
						const buyNowProductIndex = state.productList.findIndex(
							(product) =>
								product.id === buyNowProduct.id &&
								product.size === buyNowProduct.size,
						);

						if (buyNowProductIndex > -1) {
							state.buyNowProduct.cartId =
								state.productList[buyNowProductIndex].cartId;
							state.productList.splice(buyNowProductIndex, 1);
						}
					}
				} else {
					throw new Error();
				}
			} catch (error) {
				console.log("Error: ", error);
			} finally {
				state.isLoadingData = false;
			}
		},

		reset({ state }) {
			Object.assign(state, {
				productList: [],
				isLoadingData: false,
				buyNowProduct: undefined,
			});
			localStorage.removeItem(EKeys.buyNow);
		},

		initCart(state) {
			const buyNowProduct = localStorage.getItem(
				EKeys.buyNow,
				JSON.stringify(state.buyNowProduct),
			);

			if (buyNowProduct) {
				state.buyNowProduct = JSON.parse(buyNowProduct);
			}
		},
	},
};

export default cart;
