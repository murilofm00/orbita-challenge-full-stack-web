import { createRouter, createWebHistory } from 'vue-router'
import BaseView from '@/views/BaseView.vue'
import StudentsView from '@/views/students/StudentsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: BaseView,
      children: [
        {
          name:'students',
          path: 'alunos',
          component: StudentsView
        }
      ]
    },
  ]
})

export default router
