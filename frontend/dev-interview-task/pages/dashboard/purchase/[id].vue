<script lang="ts" setup>
import type { Stripe, StripeElements } from "@stripe/stripe-js";

import type { Video } from "types";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});

const runtimeConfig = useRuntimeConfig();
const { user, session } = useUserSession();

const route = useRoute();
const router = useRouter();

const { data: video } = await useFetch<Video>(
  `${runtimeConfig.public.API_BASE_URL}/api/videos/${route.params.id}`,
  {
    onRequest({ options }) {
      options.headers.set(
        "Authorization",
        `Bearer ${session.value.session.accessToken}`,
      );
    },
  },
);

const { loadStripe } = useClientStripe();
const stripe = ref<Stripe | null>(null);
let elements: StripeElements;

onMounted(async () => {
  stripe.value = await loadStripe();
});

watch(
  stripe,
  async () => {
    if (stripe.value) {
      const {
        clientSecret,
        error,
      }: { clientSecret: string | null; error: string | null } = await $fetch(
        "/api/stripe/create-payment-intent",
        {
          method: "POST",
          body: {
            currency: video.value?.currency,
            price: video.value?.price,
          },
        },
      );
      if (error) {
        console.error(error);
        return;
      }

      elements = stripe.value.elements({
        clientSecret: clientSecret as string,
      });
      const paymentElement = elements.create("payment");
      paymentElement.mount("#paymentElement");
    }
  },
  {
    immediate: true,
  },
);

const isProcessing = ref(false);
const errorMessage = ref<string | null>(null);

async function pay() {
  isProcessing.value = true;

  try {
    const { error } = await stripe.value!.confirmPayment({
      elements,
      redirect: "if_required",
      confirmParams: {
        payment_method_data: {
          billing_details: {
            name: user.value.fullName,
            email: user.value.email,
          },
        },
      },
    });

    if (error) {
      console.error("Payment error:", error);
      errorMessage.value = error.message || null;
      isProcessing.value = false;
    }
    else {
      savePayment();
      isProcessing.value = false;

      // Handle post-payment success actions, like showing a success message
      // TODO: Notify server about purchase
    }
  }
  catch (error) {
    console.error("Payment processing error:", error);
    errorMessage.value = "An error occurred. Please try again.";
    isProcessing.value = false;
  }
}

async function savePayment() {
  $fetch(`${runtimeConfig.public.API_BASE_URL}/api/payments`, {
    method: "POST",
    body: {
      userId: user.value.id,
      videoId: video.value?.id,
    },
  }).then(() => {
    alert("Payment succeeded");
    router.push("/dashboard");
  });
}
</script>

<template>
  <div class="flex flex-col w-full h-full">
    <div class="flex flex-col w-full md:w-1/2 lg:w-1/3 self-start gap-4 px-4">
      <h1 class="text-2xl font-bold">
        Purchase Video: {{ video?.title }}
      </h1>
      <form class="flex flex-col" @submit.prevent="pay">
        <div id="paymentElement" />
        <button class="btn btn-primary mt-4 self-end">Pay Now</button>
        <p
          v-if="errorMessage !== null"
          id="payment-error"
          role="alert"
          class="text-red-700 text-center font-semibold"
        >
          {{ errorMessage }}
        </p>
      </form>
    </div>
  </div>
</template>
