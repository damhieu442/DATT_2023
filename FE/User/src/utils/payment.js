import { loadStripe } from "@stripe/stripe-js";

const stripePublichKey = process.env.VUE_APP_STRIPE_KEY;
// Export loadStripe to avoid stripe being loaded multiple times
export default loadStripe(stripePublichKey);
