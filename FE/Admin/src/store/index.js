import { createStore } from "vuex";
import user from "./user";
import category from "./category";

export const store = createStore({
	modules: { user, category },
});
