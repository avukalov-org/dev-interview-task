// https://nuxt.com/docs/api/configuration/nuxt-config
import tailwindcss from "@tailwindcss/vite";

export default defineNuxtConfig({
  compatibilityDate: "2025-05-15",
  devtools: { enabled: true },
  modules: [
    "@nuxt/eslint",
    "@nuxt/icon",
    "@nuxtjs/color-mode",
    "nuxt-auth-utils",
    "@unlok-co/nuxt-stripe",
  ],
  css: ["~/assets/css/main.css"],
  runtimeConfig: {
    // API_BASE_URL: process.env.API_BASE_URL,
    MUX_TOKEN_ID: process.env.MUX_TOKEN_ID,
    MUX_TOKEN_SECRET: process.env.MUX_TOKEN_SECRET,
    public: {
      NUXT_BASE_URL: process.env.NUXT_BASE_URL,
      API_BASE_URL: process.env.API_BASE_URL,
    },
  },
  stripe: {
    // Server
    server: {
      key: process.env.STRIPE_SECRET_KEY,
      options: {
        // your api options override for stripe server side
        // https://github.com/stripe/stripe-node?tab=readme-ov-file#configuration
      },
      // CLIENT
    },
    client: {
      key: process.env.STRIPE_PUBLIC_KEY,
      // manualClientLoad: true, // if you want to have control where you are going to load the client
      // your api options override for stripe client side https://stripe.com/docs/js/initializing#init_stripe_js-options
      options: {},
    },
  },
  vite: {
    plugins: [tailwindcss()],
  },
  eslint: {
    config: {
      standalone: false,
    },
  },
  colorMode: {
    dataValue: "theme",
  },
  vue: {
    compilerOptions: {
      isCustomElement: tag => ["mux-uploader", "mux-player"].includes(tag),
    },
  },
});
