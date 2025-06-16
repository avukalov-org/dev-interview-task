import { z } from "zod";
import { jwtDecode } from "jwt-decode";
import { User } from "~/models/user";

const bodySchema = z.object({
  firstName: z.string(),
  lastName: z.string(),
  email: z.string().email(),
  password: z.string(),
  confirmPassword: z.string(),
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

  if (res.token === undefined) {
    throw createError({
      statusCode: 400,
      message: "Bad Request",
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
