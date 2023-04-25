import { loadStripe } from "@stripe/stripe-js";

const stripePublichKey = process.env.VUE_APP_STRIPE_KEY;
export default loadStripe(stripePublichKey);
