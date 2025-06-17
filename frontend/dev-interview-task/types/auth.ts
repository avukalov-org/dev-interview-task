export type AuthResponse = {
  token: string;
  message: string;
};

export type TokenPayload = {
  sub: string;
  name: string;
  given_name: string;
  family_name: string;
  email: string;
  exp: number;
  iss: string;
  aud: string;
};

export type GoogleUserPayload = {
  sub: string;
  name: string;
  given_name: string;
  family_name: string;
  picture: string;
  email: string;
  email_verified: boolean;
};

export type GoogleTokenPayload = {
  access_token: string;
  expires_in: number;
  scope: string;
  token_type: string;
  id_token: string;
};
