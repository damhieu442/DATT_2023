import { createStore } from "vuex";
import cart from "./cart";

export const store = createStore({
	modules: { cart },
});
