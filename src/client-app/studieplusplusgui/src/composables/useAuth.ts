import { ref } from 'vue';

const AUTH_KEY = 'spp_auth';

const isLoggedIn = ref<boolean>(!!localStorage.getItem(AUTH_KEY));

export function useAuth() {
  function login(email: string, password: string): boolean {
    // Placeholder — swap out for a real API call later
    if (email.trim() && password.trim()) {
      localStorage.setItem(AUTH_KEY, '1');
      isLoggedIn.value = true;
      return true;
    }
    return false;
  }

  function logout() {
    localStorage.removeItem(AUTH_KEY);
    isLoggedIn.value = false;
  }

  return { isLoggedIn, login, logout };
}
