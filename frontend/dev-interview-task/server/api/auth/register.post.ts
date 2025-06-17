import { z } from "zod";
import { jwtDecode } from "jwt-decode";
import { AuthResponse, TokenPayload } from "~/types";

const bodySchema = z.object({
  firstName: z.string(),
  lastName: z.string(),
  email: z.string().email(),
  password: z.string(),
  confirmPassword: z.string(),
});

export default defineEventHandler(async (event) => {
  const { firstName, lastName, email, password, confirmPassword } =
    await readValidatedBody(event, bodySchema.parse);

  const res: AuthResponse = await $fetch(
    process.env.AUTH_BASE_URL + "/api/auth/register",
    {
      method: "POST",
      body: {
        firstName,
        lastName,
        email,
        password,
        confirmPassword,
      },
    }
  );

  // TODO: Sredi response na serveru
  if (res.token === undefined) {
    throw createError({
      statusCode: 400,
      message: res.message,
    });
  }

  var payload = jwtDecode<TokenPayload>(res.token);

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
