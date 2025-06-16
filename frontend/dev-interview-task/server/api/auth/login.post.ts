import { z } from "zod";
import { jwtDecode } from "jwt-decode";
import { User } from "~/models/user";

const bodySchema = z.object({
  email: z.string().email(),
  password: z.string(),
  rememberMe: z.boolean(),
});

interface AuthResponse {
  token: string;
}

interface TokenPayload {
  sub: string;
  given_name: string;
  family_name: string;
  email: string;
  exp: number;
  iss: string;
  aud: string;
}

export default defineEventHandler(async (event) => {
  const { email, password, rememberMe } = await readValidatedBody(
    event,
    bodySchema.parse
  );

  const res: AuthResponse = await $fetch(
    process.env.AUTH_BASE_URL + "/api/auth/login",
    {
      method: "POST",
      body: {
        email,
        password,
        rememberMe,
      },
    }
  );

  if (res.token === undefined) {
    throw createError({
      statusCode: 400,
      message: "Bad credentials",
    });
  }

  var payload = jwtDecode<TokenPayload>(res.token);
  console.log(payload);

  const user: User = {
    id: payload.sub,
    firstName: payload.given_name,
    lastName: payload.family_name,
    email: payload.email,
    exp: payload.exp,
    token: res.token,
  };

  await setUserSession(event, {
    user,
  });

  return {};
});
