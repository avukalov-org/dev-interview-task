import { useServerStripe } from "#stripe/server";

export default defineEventHandler(async (event) => {
  const { user } = await requireUserSession(event);

  const { price, currency } = await readBody(event);
  const stripe = await useServerStripe(event);

  try {
    const paymentIntent = await stripe.paymentIntents.create({
      currency: currency,
      amount: Number(price * 100), // Amount in cents
      automatic_payment_methods: { enabled: true },
    });

    return {
      clientSecret: paymentIntent.client_secret,
      error: null,
    };
  } catch (error) {
    return {
      clientSecret: null,
      error,
    };
  }
});
