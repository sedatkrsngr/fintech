import { postJson } from "@/shared/http/api-client";

type IssueTokenResponse = {
  accessToken: string;
  refreshToken: string;
};

type RegisterResponse = {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
};

type AcceptedResponse = {
  accepted: boolean;
};

function assertOk<T>(result: { ok: boolean; data?: T; error?: string }, fallbackMessage: string): T {
  if (!result.ok || !result.data) {
    throw new Error(result.error ?? fallbackMessage);
  }

  return result.data;
}

export async function issueToken(input: { email: string; password: string }) {
  const result = await postJson<IssueTokenResponse>("/api/identity/api/auth/token", input);
  return assertOk(result, "Login failed.");
}

export async function registerUser(input: {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}) {
  const result = await postJson<RegisterResponse>("/api/identity/api/users", input);
  return assertOk(result, "Registration failed.");
}

export async function requestEmailVerification(email: string) {
  const result = await postJson<AcceptedResponse>(
    "/api/identity/api/auth/email-verification/request",
    { email }
  );

  return assertOk(result, "Email verification request failed.");
}

export async function requestPasswordReset(email: string) {
  const result = await postJson<AcceptedResponse>(
    "/api/identity/api/auth/password-reset/request",
    { email }
  );

  return assertOk(result, "Password reset request failed.");
}

export async function confirmPasswordReset(input: { token: string; newPassword: string }) {
  const result = await postJson<AcceptedResponse>(
    "/api/identity/api/auth/password-reset/confirm",
    input
  );

  return assertOk(result, "Password reset confirmation failed.");
}

export async function confirmEmailVerification(token: string) {
  const result = await postJson<AcceptedResponse>(
    "/api/identity/api/auth/email-verification/confirm",
    { token }
  );

  return assertOk(result, "Email verification failed.");
}
