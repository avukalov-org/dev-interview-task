import { z } from "zod";

import { getMux } from "./utils";

export default defineEventHandler(async (event) => {
  const { user } = await requireUserSession(event);

  const { title, creatorId, externalId } = await readBody(event);

  const mux = getMux();
  return await mux.video.uploads.create({
    cors_origin: "*",
    new_asset_settings: {
      playback_policy: ["public"],
      video_quality: "basic",
      meta: {
        title,
        creator_id: creatorId,
        external_id: externalId,
      },
    },
  });
});
