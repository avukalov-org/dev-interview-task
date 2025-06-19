// auth.d.ts
declare module "#auth-utils" {
  type User = {
    // Add your own fields
    id: string;
    fullName: string;
    firstName: string;
    lastName: string;
    email: string;
  };

  type UserSession = {
    // Add your own fields
    userId: string;
    accessToken: string;
    expiresAt: number;
  };

  type SecureSessionData = {
    // Add your own fields
  };
}

export {};
