<script lang="ts" setup>
import type { Video } from "types";
import "@mux/mux-player";

const { video } = defineProps<{
  video: Video;
}>();
</script>

<template>
  <div
    class="card bg-base-100 image-full w-72 md:w-96 shadow-sm card-sm md:card-md"
  >
    <mux-player
      class="h-64 md:h-72"
      :playback-id="video.playbackId"
      :videoTitle="video.title"
      :metadata-viewer-user-id="video.userId"
      :metadata-video-title="video.title"
    />
    <div class="card-body h-full justify-end bg-black/70">
      <h2 class="card-title">
        {{ video.title }}
      </h2>

      <div class="card-actions justify-between items-center">
        <h3>Price: {{ video.price }} {{ video.currency }}</h3>
        <button class="btn btn-primary">Buy Now</button>
      </div>
    </div>
  </div>
</template>

<!-- 
ako je kupljeno stavi ako nije nemoj staviti
:playback-id="video.playbackId" 

-->
