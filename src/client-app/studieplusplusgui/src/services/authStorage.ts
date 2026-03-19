export const AUTH_STATE_KEY = 'spp_auth';
export const AUTH_TOKEN_KEY = 'spp_access_token';
export const AUTH_LOGIN_METHOD_KEY = 'spp_login_method';
export const AUTH_LOGIN_IDENTIFIER_KEY = 'spp_login_identifier';

const DEFAULT_DEV_BEARER_TOKEN =
  'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRldi11c2VyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiZGV2QHN0dWRpZXBsdXNwbHVzLmRrIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE3NzM5OTA1OTMsImlzcyI6IlN0dWRpZVBsdXNQbHVzIiwiYXVkIjoiU3R1ZGllUGx1c1BsdXMifQ.Tea5W6-mJIZ90gsZ9EzQ7I9suULMu9dlxQZq_A7hN-c';

export function getStoredAccessToken(): string | null {
  return localStorage.getItem(AUTH_TOKEN_KEY) || import.meta.env.VITE_API_BEARER_TOKEN || DEFAULT_DEV_BEARER_TOKEN;
}

export function setStoredAccessToken(token: string) {
  localStorage.setItem(AUTH_TOKEN_KEY, token);
}

export function clearStoredAccessToken() {
  localStorage.removeItem(AUTH_TOKEN_KEY);
}

export function setStoredLoginContext(method: string, identifier: string) {
  localStorage.setItem(AUTH_LOGIN_METHOD_KEY, method);
  localStorage.setItem(AUTH_LOGIN_IDENTIFIER_KEY, identifier);
}

export function getStoredLoginMethod(): string | null {
  return localStorage.getItem(AUTH_LOGIN_METHOD_KEY);
}

export function getStoredLoginIdentifier(): string | null {
  return localStorage.getItem(AUTH_LOGIN_IDENTIFIER_KEY);
}

export function clearStoredLoginContext() {
  localStorage.removeItem(AUTH_LOGIN_METHOD_KEY);
  localStorage.removeItem(AUTH_LOGIN_IDENTIFIER_KEY);
}
