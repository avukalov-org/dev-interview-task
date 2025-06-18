import { jwtDecode } from "jwt-decode";

import type {
  AuthResponse,
  GoogleTokenPayload,
  GoogleUserPayload,
  TokenPayload,
} from "~/types";

const runtimeConfig = useRuntimeConfig();

export default defineOAuthGoogleEventHandler({
  config: {},
  async onSuccess(event, { user, tokens }) {
    const googleUser: GoogleUserPayload = user;
    const googleTokens: GoogleTokenPayload = tokens;

    const res: AuthResponse = await $fetch(
      `${runtimeConfig.public.API_BASE_URL}/api/auth/google`,
      {
        method: "POST",
        body: {
          firstName: googleUser.given_name,
          lastName: googleUser.family_name,
          email: googleUser.email,
          isExternal: true,
          externalProvider: "Google",
        },
      },
    );

    if (res.token === undefined) {
      throw createError({
        statusCode: 400,
        message: res.message,
      });
    }

    const payload = jwtDecode<TokenPayload>(res.token);

    await setUserSession(event, {
      user: {
        id: payload.sub,
        fullName: payload.name,
        firstName: payload.given_name,
        lastName: payload.family_name,
        email: payload.email,
      },
      session: {
        userId: payload.sub,
        accessToken: res.token,
        expiresAt: payload.exp,
      },
    });
    return sendRedirect(event, "/dashboard");
  },
  // Optional, will return a json error and 401 status code by default
  onError(event, error) {
    console.error("Google OAuth error:", error);
    return sendRedirect(event, "/");
  },
});
