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
          name: 'students',
          path: 'alunos',
          component: StudentsView,
          children: []
        },
        {
          name: 'create student',
          path: 'alunos/cadastrar',
          component: () => import('@/views/students/CreateStudentView.vue')
        },
        {
          name: 'edit student',
          path: 'alunos/editar/:id',
          component: () => import('@/views/students/EditStudentView.vue'),
          props: (route) => ({ id: new String(route.params.id) })
        }
      ]
    }
  ]
})

export default router
