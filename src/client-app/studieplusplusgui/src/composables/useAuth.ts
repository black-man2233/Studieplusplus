import { ref } from 'vue';
import {
  AUTH_STATE_KEY,
  clearStoredAccessToken,
  clearStoredLoginContext,
  setStoredLoginContext,
} from '@/services/authStorage';

const isLoggedIn = ref<boolean>(!!localStorage.getItem(AUTH_STATE_KEY));

export function useAuth() {
  function login(email: string, password: string, method: string = 'direct'): boolean {
    // Placeholder — swap out for a real API call later
    if (email.trim() && password.trim()) {
      localStorage.setItem(AUTH_STATE_KEY, '1');
      setStoredLoginContext(method, email.trim());
      isLoggedIn.value = true;
      return true;
    }
    return false;
  }

  function logout() {
    localStorage.removeItem(AUTH_STATE_KEY);
    clearStoredAccessToken();
    clearStoredLoginContext();
    isLoggedIn.value = false;
  }

  return { isLoggedIn, login, logout };
}
