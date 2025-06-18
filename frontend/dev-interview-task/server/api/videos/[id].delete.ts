import { getMux } from "./utils";

export default defineEventHandler(async (event) => {
  const { user } = await requireUserSession(event);

  const assetId = event.context.params?.id;

  const mux = getMux();
  mux.video.assets.delete(assetId!);

  return {};
});
