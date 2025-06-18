<script lang="ts" setup>
import type { Video } from "types";

const { video } = defineProps<{
  video: Video;
}>();
</script>

<template>
  <div
    class="card bg-base-100 w-72 md:w-96 shadow-2xl card-sm md:card-md"
  >
    <mux-player
      class="h-64 md:h-72"
      :playback-id="video.playbackId"
      :metadata-viewer-user-id="video.userId"
      :metadata-video-title="video.title"
    />
    <div class="card-body">
      <h2 class="card-title">
        {{ video.title }}
      </h2>
    </div>
  </div>
</template>
