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
  ],
  css: ["~/assets/css/main.css"],
  runtimeConfig: {
    // API_BASE_URL: process.env.API_BASE_URL,
    MUX_TOKEN_ID: process.env.MUX_TOKEN_ID,
    MUX_TOKEN_SECRET: process.env.MUX_TOKEN_SECRET,
    STRIPE_SECRET_KEY: process.env.STRIPE_SECRET_KEY,
    public: {
      API_BASE_URL: process.env.API_BASE_URL,
      STRIPE_PUBLIC_KEY: process.env.STRIPE_PUBLIC_KEY,
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
      isCustomElement: (tag) => ["mux-uploader", "mux-player"].includes(tag),
    },
  },
});
