import { createRouter, createWebHistory } from '@ionic/vue-router';
import { RouteRecordRaw } from 'vue-router';
import TabsPage from '../views/TabsPage.vue'
import { useAuth } from '@/composables/useAuth';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/login'
  },
  {
    path: '/login',
    component: () => import('@/views/LoginPage.vue'),
    meta: { public: true }
  },
  {
    path: '/login/unilogin',
    component: () => import('@/views/UniLoginPage.vue'),
    meta: { public: true }
  },
  {
    path: '/login/direkte',
    component: () => import('@/views/DirectLoginPage.vue'),
    meta: { public: true }
  },
  {
    path: '/login/mitid',
    component: () => import('@/views/MitIdPage.vue'),
    meta: { public: true }
  },
  {
    path: '/tabs/',
    component: TabsPage,
    children: [
      {
        path: '',
        redirect: '/tabs/home'
      },
      {
        path: 'messages',
        component: () => import('@/views/MessagePage.vue')
      },
      {
        path: 'schedule',
        component: () => import('@/views/SchedulePage.vue')
      },
      {
        path: 'home',
        component: () => import('@/views/HomePage.vue')
      },
      {
        path: 'profile',
        component: () => import('@/views/ProfilePage.vue')
      },
      {
        path: 'settings',
        component: () => import('@/views/SettingsPage.vue')
      }
    ]
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

router.beforeEach((to) => {
  const { isLoggedIn } = useAuth();
  if (!to.meta.public && !isLoggedIn.value) {
    return { path: '/login' };
  }
  if (to.path.startsWith('/login') && isLoggedIn.value) {
    return { path: '/tabs/home' };
  }
});

export default router
