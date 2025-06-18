import { jwtDecode } from "jwt-decode";
import { z } from "zod";

import type { AuthResponse, TokenPayload } from "~/types";

const runtimeConfig = useRuntimeConfig();

const bodySchema = z.object({
  email: z.string().email(),
  password: z.string(),
  rememberMe: z.boolean(),
});

export default defineEventHandler(async (event) => {
  const { email, password, rememberMe } = await readValidatedBody(
    event,
    bodySchema.parse,
  );

  const res: AuthResponse = await $fetch(
    `${runtimeConfig.public.API_BASE_URL}/api/auth/login`,
    {
      method: "POST",
      body: {
        email,
        password,
        rememberMe,
      },
    },
  );

  // TODO: Sredi response na serveru
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

  return {};
});
