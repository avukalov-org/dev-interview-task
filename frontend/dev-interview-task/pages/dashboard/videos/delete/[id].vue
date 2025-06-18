<script lang="ts" setup>
import "@mux/mux-uploader";

definePageMeta({
  layout: "dashboard",
  middleware: ["authenticated"],
});

const route = useRoute();

const deleteVideo = async () => {
  await $fetch(`/api/videos/${route.params.id}`, {
    method: "DELETE",
  });

  alert("Video deleted successfully! You can go back to your videos.");
};
</script>

<template>
  <div class="flex flex-col h-full w-full p-4 gap-4">
    <div class="flex flex-row">
      <NuxtLink to="/dashboard/videos" class="btn btn-primary">
        <Icon name="tabler:arrow-big-left-filled" size="24" class="mr-2" />
        Back
      </NuxtLink>
    </div>

    <div class="flex flex-col gap-4">
      <h1 class="text-xl">Are you sure you want to delete this video?</h1>
      <p class="text-sm">
        After pressing the Delete button, the video will be deleted shortly.
      </p>
      <button class="btn btn-error self-start" @click="deleteVideo()">
        Delete
      </button>
    </div>
  </div>
</template>
