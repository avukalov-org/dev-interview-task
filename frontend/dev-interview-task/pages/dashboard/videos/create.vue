<script lang="ts" setup>
import "@mux/mux-uploader";
import { v4 as uuidv4 } from "uuid";

import type { CreateUploadMuxResponse } from "types";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});

const runtimeConfig = useRuntimeConfig();
const { user, session } = useUserSession();

const uploadMeta = reactive({
  isUploadReady: false,
  uploadId: "",
  uploadUrl: "",
});

const metadata = reactive({
  id: uuidv4(),
  title: "",
  isPremium: false,
  price: 0.0,
  currency: "eur",
  status: "Processing",
});

async function getUploadContext() {
  const res: CreateUploadMuxResponse = await $fetch(
    "/api/videos/create-upload",
    {
      method: "POST",
      body: {
        title: metadata.title,
        creatorId: user.value.id,
        externalId: metadata.id,
      },
    }
  );

  uploadMeta.uploadId = res.id;
  uploadMeta.uploadUrl = res.url;

  await $fetch(`${runtimeConfig.public.API_BASE_URL}/api/videos`, {
    method: "POST",
    headers: {
      Authorization: `Bearer ${session.value.session.accessToken}`,
    },
    body: metadata,
  });

  uploadMeta.isUploadReady = true;
}

function onUploadSuccess() {
  alert("Video uploaded successfully! You can go back to your videos.");
}
</script>

<template>
  <div class="flex flex-col h-full w-full p-4 gap-4">
    <div class="flex flex-row">
      <NuxtLink to="/dashboard/videos" class="btn btn-primary">
        <Icon name="tabler:arrow-big-left-filled" size="24" class="mr-2" />
        Back
      </NuxtLink>
    </div>

    <div class="flex flex-col md:flex-row gap-4">
      <div class="flex flex-col md:w-1/2 gap-4">
        <fieldset class="fieldset">
          <legend class="fieldset-legend">
            What is the title of the video?
          </legend>
          <input
            v-model="metadata.title"
            type="text"
            class="input"
            placeholder="Type your title here"
          />
          <legend class="fieldset-legend">Is the video premium?</legend>
          <label class="label">
            <input
              v-model="metadata.isPremium"
              type="checkbox"
              :checked="metadata.isPremium"
              class="checkbox checkbox-sm"
            />
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
            />
            <legend class="fieldset-legend">What currency you prefer?</legend>
            <select v-model="metadata.currency" class="select">
              <option value="eur" selected>Eur</option>
              <option value="usd">Usd</option>
            </select>
          </div>
        </fieldset>
        <button
          :disabled="!metadata.title || uploadMeta.isUploadReady"
          class="btn btn-primary self-start"
          @click="getUploadContext()"
        >
          Next
          <Icon
            name="tabler:arrow-big-right-filled"
            size="24"
            class="w-24 ml-2"
          />
        </button>
      </div>
      <div class="flex flex-col md:w-1/2">
        <mux-uploader
          v-if="uploadMeta.isUploadReady"
          :endpoint="uploadMeta.uploadUrl"
          class="w-full"
          @success="onUploadSuccess()"
        />
      </div>
    </div>
  </div>
</template>
