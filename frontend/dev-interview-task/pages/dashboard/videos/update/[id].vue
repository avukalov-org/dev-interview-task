<script lang="ts" setup>
import "@mux/mux-uploader";

import type { Video } from "types";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});

const runtimeConfig = useRuntimeConfig();
const { user, session } = useUserSession();
const route = useRoute();

const metadata = reactive<Video>({
  id: "",
  title: "",
  isPremium: false,
  price: 0,
  currency: "",
  status: "",
  userId: user.value.id,
});

const { data: video, status } = await useFetch<Video>(
  `${runtimeConfig.public.API_BASE_URL}/api/videos/${route.params.id}`,
  {
    lazy: true,
    onRequest({ options }) {
      options.headers.set(
        "Authorization",
        `Bearer ${session.value.session.accessToken}`,
      );
    },
  },
);

watch(video, (newVideo) => {
  if (newVideo) {
    Object.assign(metadata, newVideo);
  }
});

async function update() {
  await $fetch(`${runtimeConfig.public.API_BASE_URL}/api/videos`, {
    method: "PUT",
    headers: {
      Authorization: `Bearer ${session.value.session.accessToken}`,
    },
    body: {
      ...metadata,
    },
  });

  alert("Video updated successfully! You can go back to your videos.");
}
</script>

<template>
  <div class="flex flex-col h-full w-full p-4 gap-4">
    <div class="flex flex-row">
      <NuxtLink to="/dashboard/videos" class="btn btn-primary">
        <Icon
          name="tabler:arrow-big-left-filled"
          size="24"
          class="mr-2"
        />
        Back
      </NuxtLink>
    </div>

    <span
      v-if="status === 'pending'"
      class="loading loading-spinner loading-xl"
    />
    <div v-else class="flex flex-col md:flex-row gap-4">
      <div class="flex flex-col md:w-1/2 gap-4">
        <form @submit.prevent="update">
          <fieldset class="fieldset">
            <legend class="fieldset-legend">
              What is the title of the video?
            </legend>
            <input
              v-model="metadata.title"
              type="text"
              class="input"
              placeholder="Type your title here"
            >
            <legend class="fieldset-legend">
              Is the video premium?
            </legend>
            <label class="label">
              <input
                v-model="metadata.isPremium"
                type="checkbox"
                :checked="metadata.isPremium"
                class="checkbox checkbox-sm"
              >
              Premium
            </label>
            <div v-if="metadata.isPremium" class="flex flex-col">
              <legend class="fieldset-legend">
                What is the cost of the video?
              </legend>
              <input
                v-model="metadata.price"
                type="text"
                class="input"
                placeholder="Type your price here"
              >
              <legend class="fieldset-legend">
                What currency you prefer?
              </legend>
              <select v-model="metadata.currency" class="select">
                <option value="eur" selected>Eur</option>
                <option value="usd">Usd</option>
              </select>
            </div>
            <button
              :disabled="status !== 'success'"
              class="btn btn-primary self-start w-24 mt-2"
            >
              Update
            </button>
          </fieldset>
        </form>
      </div>
    </div>
  </div>
</template>
