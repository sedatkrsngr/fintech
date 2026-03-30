const accessTokenKey = "fintech.accessToken";
const refreshTokenKey = "fintech.refreshToken";

export type StoredSession = {
  accessToken: string | null;
  refreshToken: string | null;
};

export function saveSessionTokens(accessToken: string, refreshToken: string) {
  if (typeof window === "undefined") {
    return;
  }

  window.localStorage.setItem(accessTokenKey, accessToken);
  window.localStorage.setItem(refreshTokenKey, refreshToken);
}

export function clearSessionTokens() {
  if (typeof window === "undefined") {
    return;
  }

  window.localStorage.removeItem(accessTokenKey);
  window.localStorage.removeItem(refreshTokenKey);
}

export function getStoredSession(): StoredSession {
  if (typeof window === "undefined") {
    return {
      accessToken: null,
      refreshToken: null
    };
  }

  return {
    accessToken: window.localStorage.getItem(accessTokenKey),
    refreshToken: window.localStorage.getItem(refreshTokenKey)
  };
}

export function hasStoredSession() {
  const session = getStoredSession();
  return Boolean(session.accessToken && session.refreshToken);
}
