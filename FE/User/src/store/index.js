import { createStore } from "vuex";
import cart from "./cart";
import category from "./category";
import user from "./user";

export const store = createStore({
	modules: { cart, category, user },
});
