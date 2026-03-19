import { getStoredAccessToken } from '@/services/authStorage';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5168';

export class ApiError extends Error {
  status: number;
  payload: unknown;

  constructor(message: string, status: number, payload: unknown) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
    this.payload = payload;
  }
}

function normalizePath(path: string): string {
  return path.startsWith('/') ? path : `/${path}`;
}

function makeUrl(baseUrl: string, path: string): string {
  const normalizedPath = normalizePath(path);
  if (!baseUrl) {
    return normalizedPath;
  }

  const trimmedBase = baseUrl.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
  return `${trimmedBase}${normalizedPath}`;
}

function getRequestUrlCandidates(path: string): string[] {
  const normalizedPath = path.startsWith('/') ? path : `/${path}`;
  // Proev backend-URL foerst, derefter relativ sti til proxy/native miljoeer.
  const candidates = [
    makeUrl(API_BASE_URL, normalizedPath),
    normalizedPath,
  ];

  return [...new Set(candidates)];
}

function normalizeDanishText(value: string): string {
  return value.replace(/ae|oe|aa/gi, (match) => {
    const lower = match.toLowerCase();
    const mapped = lower === 'ae' ? 'æ' : lower === 'oe' ? 'ø' : 'å';

    if (match === match.toUpperCase()) {
      return mapped.toUpperCase();
    }

    if (match[0] === match[0].toUpperCase()) {
      return mapped.toUpperCase();
    }

    return mapped;
  });
}

function shouldNormalizeString(value: string, key?: string): boolean {
  // Undgaa at aendre tekniske felter, hvor ae/oe/aa kan have betydning.
  const technicalKeyPattern = /(id|email|token|url|uri|path|code|identifier)/i;
  if (key && technicalKeyPattern.test(key)) {
    return false;
  }

  if (value.includes('@')) {
    return false;
  }

  if (value.includes('://')) {
    return false;
  }

  return true;
}

function normalizeDanishPayload<T>(value: T, key?: string): T {
  if (typeof value === 'string') {
    return (shouldNormalizeString(value, key) ? normalizeDanishText(value) : value) as T;
  }

  if (Array.isArray(value)) {
    return value.map((item) => normalizeDanishPayload(item)) as T;
  }

  if (value && typeof value === 'object') {
    const normalizedEntries = Object.entries(value as Record<string, unknown>).map(([entryKey, nestedValue]) => [
      entryKey,
      normalizeDanishPayload(nestedValue, entryKey),
    ]);

    return Object.fromEntries(normalizedEntries) as T;
  }

  return value;
}

async function parseResponsePayload(response: Response): Promise<unknown> {
  const contentType = response.headers.get('content-type') ?? '';
  if (contentType.includes('application/json')) {
    const payload = await response.json();
    return normalizeDanishPayload(payload);
  }

  const text = await response.text();
  return text.length > 0 ? normalizeDanishText(text) : null;
}

async function request<T>(path: string, init: RequestInit = {}): Promise<T> {
  const headers = new Headers(init.headers ?? {});
  const token = getStoredAccessToken();

  if (token && !headers.has('Authorization')) {
    headers.set('Authorization', `Bearer ${token}`);
  }

  if (!headers.has('Accept')) {
    headers.set('Accept', 'application/json');
  }

  if (init.body && !headers.has('Content-Type')) {
    headers.set('Content-Type', 'application/json');
  }

  const urlCandidates = getRequestUrlCandidates(path);
  let lastError: unknown = null;

  // Proev alle URL-kandidater, saa samme klient virker i flere setups.
  for (const url of urlCandidates) {
    try {
      const response = await fetch(url, {
        ...init,
        headers,
      });

      const payload = await parseResponsePayload(response);

      if (!response.ok) {
        throw new ApiError(`Request failed with status ${response.status}`, response.status, payload);
      }

      return payload as T;
    } catch (error) {
      lastError = error;
    }
  }

  if (lastError instanceof ApiError) {
    throw lastError;
  }

  throw new Error('Network request failed for all API base URL candidates.');
}

export const apiClient = {
  get<T>(path: string, init: RequestInit = {}) {
    return request<T>(path, { ...init, method: 'GET' });
  },
  post<T>(path: string, body?: unknown, init: RequestInit = {}) {
    return request<T>(path, {
      ...init,
      method: 'POST',
      body: body === undefined ? undefined : JSON.stringify(body),
    });
  },
  put<T>(path: string, body?: unknown, init: RequestInit = {}) {
    return request<T>(path, {
      ...init,
      method: 'PUT',
      body: body === undefined ? undefined : JSON.stringify(body),
    });
  },
  delete<T>(path: string, init: RequestInit = {}) {
    return request<T>(path, { ...init, method: 'DELETE' });
  },
};
