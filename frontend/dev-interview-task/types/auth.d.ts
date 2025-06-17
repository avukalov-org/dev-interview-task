// auth.d.ts
declare module "#auth-utils" {
  interface User {
    // Add your own fields
    id: string;
    name: string;
    firstName: string;
    lastName: string;
    email: string;
  }

  interface UserSession {
    // Add your own fields
    userId: string;
    accessToken: string;
    expiresAt: number;
  }

  interface SecureSessionData {
    // Add your own fields
  }
}

export {};
