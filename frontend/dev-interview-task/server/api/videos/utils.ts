import Mux from "@mux/mux-node";

const runtimeConfig = useRuntimeConfig();

export function getMux() {
  return new Mux({
    tokenId: runtimeConfig.MUX_TOKEN_ID,
    tokenSecret: runtimeConfig.MUX_TOKEN_SECRET,
  });
}
