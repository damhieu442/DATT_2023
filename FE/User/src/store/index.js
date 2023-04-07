import { createStore } from "vuex";
import cart from "./cart";
import category from "./category";

export const store = createStore({
	modules: { cart, category },
});
